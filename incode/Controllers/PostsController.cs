using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using incode.Models;
using incode.Data;
using incode.ViewModels; // ★ 1. 必須引用這裡才能抓到 ViewModel

namespace incode.Controllers
{
    public class PostsController : Controller
    {
        private readonly incodedatabaseContext _context;

        public PostsController(incodedatabaseContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var posts = _context.Posts.Include(p => p.PostsUser);
            return View(await posts.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            // 1. 查詢資料庫 (包含作者、留言、按讚紀錄)
            var post = await _context.Posts
                .Include(p => p.PostsUser)                 // 關聯載入：文章作者
                .Include(p => p.PostsReplies)              // 關聯載入：文章留言
                    .ThenInclude(r => r.RepliesUser)       // 關聯載入：留言的作者
                .Include(p => p.PostsLikes)                // 關聯載入：按讚紀錄
                .FirstOrDefaultAsync(m => m.PostsId == id);

            if (post == null) return NotFound();

            // 2. 假設當前登入者 ID (之後請改成 User.Identity 取得)
            int currentUserId = 1;

            // 3. ★ 關鍵步驟：將資料轉換為 PostDetailsViewModel
            var viewModel = new PostDetailsViewModel
            {
                Post = post, // 將整包文章資料放入

                // 計算按讚數
                LikeCount = post.PostsLikes.Count,

                // 判斷當前使用者是否按過讚 (用 Any 檢查是否存在該 User ID)
                IsLikedByCurrentUser = post.PostsLikes.Any(l => l.LikesUserId == currentUserId),

                // 留言輸入框預設為空
                NewReplyContent = ""
            };

            // 4. 將 ViewModel 傳給 View
            return View(viewModel);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            // 因為 View 改用了 PostCreateViewModel，這裡不需要再傳 Users 下拉選單了
            // 除非您的 ViewModel 裡有 dropdown 欄位
            return View();
        }

        // POST: Posts/Create
        // ★ 2. 這裡改成接收 ViewModel，而不是 Post 資料庫模型
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateViewModel viewModel)
        {
            // 使用 ViewModel 的好處：ModelState 只會驗證 Title 和 Content，不會因為 UserId 是 null 而報錯
            if (ModelState.IsValid)
            {
                // 手動將 ViewModel 的資料「搬」到資料庫模型 (Entity)
                var post = new Post
                {
                    // 從 ViewModel 來的資料
                    Title = viewModel.Title,
                    PostsContent = viewModel.PostsContent,
                    IsCommission = viewModel.IsCommission,

                    // 系統自動填入的資料 (使用者不用填)
                    PostsUserId = 1,               // 之後請改成 User.FindFirstValue(ClaimTypes.NameIdentifier)
                    PostsCreatedAt = DateTime.Now, // 自動押上發文時間
                    PostsViewCount = 0,            // 預設瀏覽數
                    PostsUpdatDate = null
                };

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // 驗證失敗，將 viewModel 傳回 View 以顯示錯誤訊息
            return View(viewModel);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            ViewData["PostsUserId"] = new SelectList(_context.Users, "UserId", "Nickname", post.PostsUserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // 這裡維持使用 Post，因為您還沒有 PostEditViewModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostsId,PostsUserId,Title,PostsContent,IsCommission")] Post post)
        {
            if (id != post.PostsId) return NotFound();

            // 移除關聯驗證 (因為 Edit 沒有用 ViewModel，所以這裡還是要移除)
            ModelState.Remove("PostsUser");
            ModelState.Remove("PostsReplies");
            ModelState.Remove("CinemaReviews");
            ModelState.Remove("MovieReviews");
            ModelState.Remove("PostsLikes");
            ModelState.Remove("SavePosts");

            if (ModelState.IsValid)
            {
                try
                {
                    // 為了不覆蓋掉「發文時間」和「瀏覽數」，我們先抓出舊資料
                    var existingPost = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.PostsId == id);

                    if (existingPost != null)
                    {
                        post.PostsCreatedAt = existingPost.PostsCreatedAt; // 保持原發文時間
                        post.PostsViewCount = existingPost.PostsViewCount; // 保持原瀏覽數
                        post.PostsUpdatDate = DateTime.Now;                // 更新修改時間

                        _context.Update(post);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostsId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostsUserId"] = new SelectList(_context.Users, "UserId", "Nickname", post.PostsUserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts
                .Include(p => p.PostsUser)
                .FirstOrDefaultAsync(m => m.PostsId == id);
            if (post == null) return NotFound();

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null) _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostsId == id);
        }
    }
}
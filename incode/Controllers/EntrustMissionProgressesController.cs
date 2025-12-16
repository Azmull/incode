using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using incode.Models;

namespace incode.Controllers
{
    public class EntrustMissionProgressesController : Controller
    {
        private readonly incodedatabaseContext _context;

        public EntrustMissionProgressesController(incodedatabaseContext context)
        {
            _context = context;
        }

        // GET: EntrustMissionProgresses
        public async Task<IActionResult> Index(int? EntrustMissionId, int? UserId)
        {

            var query = _context.EntrustMissionProgresses
                .Include(p => p.EntrustMission)   // 載入所屬委託任務
                    .ThenInclude(m => m.User)      // 載入發布者資訊（可顯示發布者暱稱）
                .Include(p => p.User)              // 載入接取者資訊
                .AsNoTracking();                   // 純查看，不追蹤實體

            // 篩選特定委託任務
            if (EntrustMissionId.HasValue)
            {
                query = query.Where(p => p.EntrustMissionId == EntrustMissionId.Value);
            }

            // 篩選特定會員
            if (UserId.HasValue)
            {
                query = query.Where(p => p.UserId == UserId.Value);
            }

            // 排序：先按任務ID，再按會員ID
            query = query.OrderBy(p => p.EntrustMissionId)
                         .ThenBy(p => p.UserId);

            var progresses = await query.ToListAsync();

            // 設定頁面標題，讓管理員一看就知道現在在看什麼
            if (EntrustMissionId.HasValue)
            {
                var mission = await _context.EntrustMissions
                    .FirstOrDefaultAsync(m => m.EntrustMissionId == EntrustMissionId.Value);
                ViewBag.PageTitle = $"委託任務進度 - {mission?.Title ?? "未知任務"}";
            }
            else if (UserId.HasValue)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == UserId.Value);
                ViewBag.PageTitle = $"會員委託進度 - {user?.Nickname ?? "未知會員"}";
            }
            else
            {
                ViewBag.PageTitle = "所有委託任務進度記錄";
            }

            return View(progresses);
            //var incodedatabaseContext = _context.entrust_mission_progress.Include(e => e.entrust_mission).Include(e => e.user);

            //return View(await incodedatabaseContext.ToListAsync());

        }

        // GET: EntrustMissionProgresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrustMissionProgress = await _context.EntrustMissionProgresses
                .Include(e => e.EntrustMission)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EntrustMissionProgressId == id);
            if (entrustMissionProgress == null)
            {
                return NotFound();
            }

            ViewBag.PageTitle = $"進度詳情 - {entrustMissionProgress.EntrustMission?.Title ?? "未知任務"}";

            return View(entrustMissionProgress);
        }

        // GET: EntrustMissionProgresses/Create
        public IActionResult Create()
        {
            ViewData["EntrustMissionId"] = new SelectList(_context.EntrustMissions, "EntrustMissionId", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname");
            return View();
        }

        // POST: EntrustMissionProgresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntrustMissionProgressId,UserId,EntrustMissionId,Status,IsCompleted,CreatedAt")] EntrustMissionProgress entrustMissionProgress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entrustMissionProgress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntrustMissionId"] = new SelectList(_context.EntrustMissions, "EntrustMissionId", "Title", entrustMissionProgress.EntrustMissionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", entrustMissionProgress.UserId);
            return View(entrustMissionProgress);
        }

        // GET: EntrustMissionProgresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrustMissionProgress = await _context.EntrustMissionProgresses.FindAsync(id);
            if (entrustMissionProgress == null)
            {
                return NotFound();
            }
            ViewData["EntrustMissionId"] = new SelectList(_context.EntrustMissions, "EntrustMissionId", "Title", entrustMissionProgress.EntrustMissionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", entrustMissionProgress.UserId);
            return View(entrustMissionProgress);
        }

        // POST: EntrustMissionProgresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntrustMissionProgressId,UserId,EntrustMissionId,Status,IsCompleted,CreatedAt")] EntrustMissionProgress entrustMissionProgress)
        {
            if (id != entrustMissionProgress.EntrustMissionProgressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrustMissionProgress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntrustMissionProgressExists(entrustMissionProgress.EntrustMissionProgressId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntrustMissionId"] = new SelectList(_context.EntrustMissions, "EntrustMissionId", "Titlel", entrustMissionProgress.EntrustMissionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", entrustMissionProgress.UserId);
            return View(entrustMissionProgress);
        }

        // GET: EntrustMissionProgresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrustMissionProgress = await _context.EntrustMissionProgresses
                .Include(e => e.EntrustMission)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EntrustMissionProgressId == id);
            if (entrustMissionProgress == null)
            {
                return NotFound();
            }

            return View(entrustMissionProgress);
        }

        // POST: EntrustMissionProgresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrustMissionProgress = await _context.EntrustMissionProgresses.FindAsync(id);
            if (entrustMissionProgress != null)
            {
                _context.EntrustMissionProgresses.Remove(entrustMissionProgress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntrustMissionProgressExists(int id)
        {
            return _context.EntrustMissionProgresses.Any(e => e.EntrustMissionProgressId == id);
        }
    }
}



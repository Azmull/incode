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
    public class MissionProgressesController : Controller
    {
        private readonly incodedatabaseContext _context;

        public MissionProgressesController(incodedatabaseContext context)
        {
            _context = context;
        }

        // GET: MissionProgresses
        public async Task<IActionResult> Index(int? MissionId, int? UserId)
        {

            var query = _context.MissionProgresses
                .Include(m => m.Mission)
                .Include(m => m.User)
                .AsNoTracking();  // 純查看，不追蹤實體，提高效能

            // 篩選特定任務
            if (MissionId.HasValue)
            {
                query = query.Where(m => m.MissionId == MissionId.Value);
            }

            // 篩選特定用戶
            if (UserId.HasValue)
            {
                query = query.Where(m => m.UserId == UserId.Value);
            }

            // 排序：先按任務，再按用戶
            query = query.OrderBy(m => m.MissionId)
                         .ThenBy(m => m.UserId);

            var progresses = await query.ToListAsync();

            // 設定頁面標題
            if (MissionId.HasValue)
            {
                var mission = await _context.Missions
                    .FirstOrDefaultAsync(m => m.MissionId == MissionId.Value);
                ViewBag.PageTitle = $"任務進度 - {mission?.MissionName ?? "未知任務"}";
            }
            else if (UserId.HasValue)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == UserId.Value);
                ViewBag.PageTitle = $"用戶任務進度 - {user?.Nickname ?? "未知用戶"}";
            }
            else
            {
                ViewBag.PageTitle = "所有任務進度記錄";
            }

            return View(progresses);
        }

        // GET: MissionProgresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missionProgress = await _context.MissionProgresses
                .Include(m => m.Mission)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MissionProgressId == id);
            if (missionProgress == null)
            {
                return NotFound();
            }

            ViewBag.PageTitle = $"進度詳情 - {missionProgress.Mission?.MissionName ?? "未知任務"}";


            return View(missionProgress);
        }

        // GET: MissionProgresses/Create
        public IActionResult Create()
        {
            ViewData["MissionId"] = new SelectList(_context.Missions, "MissionId", "MissionDetail");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname");
            return View();
        }

        // POST: MissionProgresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MissionProgressId,UserId,MissionId,CurrentCount,IsCompleted,AcceptedAt,CompletedAt")] MissionProgress missionProgress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(missionProgress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MissionId"] = new SelectList(_context.Missions, "MissionId", "MissionDetail", missionProgress.MissionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", missionProgress.UserId);
            return View(missionProgress);
        }

        // GET: MissionProgresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missionProgress = await _context.MissionProgresses.FindAsync(id);
            if (missionProgress == null)
            {
                return NotFound();
            }
            ViewData["MissionId"] = new SelectList(_context.Missions, "MissionId", "MissionDetail", missionProgress.MissionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", missionProgress.UserId);
            return View(missionProgress);
        }

        // POST: MissionProgresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MissionProgressId,UserId,MissionId,CurrentCount,IsCompleted,AcceptedAt,CompletedAt")] MissionProgress missionProgress)
        {
            if (id != missionProgress.MissionProgressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(missionProgress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MissionProgressExists(missionProgress.MissionProgressId))
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
            ViewData["MissionId"] = new SelectList(_context.Missions, "MissionId", "MissionDetail", missionProgress.MissionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", missionProgress.UserId);
            return View(missionProgress);
        }

        // GET: MissionProgresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missionProgress = await _context.MissionProgresses
                .Include(m => m.Mission)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MissionProgressId == id);
            if (missionProgress == null)
            {
                return NotFound();
            }

            return View(missionProgress);
        }

        // POST: MissionProgresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var missionProgress = await _context.MissionProgresses.FindAsync(id);
            if (missionProgress != null)
            {
                _context.MissionProgresses.Remove(missionProgress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MissionProgressExists(int id)
        {
            return _context.MissionProgresses.Any(e => e.MissionProgressId == id);
        }
    }
}



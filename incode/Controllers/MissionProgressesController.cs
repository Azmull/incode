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
        public async Task<IActionResult> Index()
        {
            var incodedatabaseContext = _context.MissionProgresses.Include(m => m.Mission).Include(m => m.User);
            return View(await incodedatabaseContext.ToListAsync());
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

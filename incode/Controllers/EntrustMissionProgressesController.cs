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
        public async Task<IActionResult> Index()
        {
            var incodedatabaseContext = _context.EntrustMissionProgresses.Include(e => e.EntrustMission).Include(e => e.User);
            return View(await incodedatabaseContext.ToListAsync());
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

            return View(entrustMissionProgress);
        }

        // GET: EntrustMissionProgresses/Create
        public IActionResult Create()
        {
            ViewData["EntrustMissionId"] = new SelectList(_context.EntrustMissions, "EntrustMissionId", "Detail");
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
            ViewData["EntrustMissionId"] = new SelectList(_context.EntrustMissions, "EntrustMissionId", "Detail", entrustMissionProgress.EntrustMissionId);
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
            ViewData["EntrustMissionId"] = new SelectList(_context.EntrustMissions, "EntrustMissionId", "Detail", entrustMissionProgress.EntrustMissionId);
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
            ViewData["EntrustMissionId"] = new SelectList(_context.EntrustMissions, "EntrustMissionId", "Detail", entrustMissionProgress.EntrustMissionId);
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

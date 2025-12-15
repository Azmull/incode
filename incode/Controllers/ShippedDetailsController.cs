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
    public class ShippedDetailsController : Controller
    {
        private readonly incodedatabaseContext _context;

        public ShippedDetailsController(incodedatabaseContext context)
        {
            _context = context;
        }

        // GET: ShippedDetails
        public async Task<IActionResult> Index()
        {
            var incodedatabaseContext = _context.ShippedDetails.Include(s => s.OrderDetail);
            return View(await incodedatabaseContext.ToListAsync());
        }

        // GET: ShippedDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippedDetail = await _context.ShippedDetails
                .Include(s => s.OrderDetail)
                .FirstOrDefaultAsync(m => m.ShippedDetailId == id);
            if (shippedDetail == null)
            {
                return NotFound();
            }

            return View(shippedDetail);
        }

        // GET: ShippedDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "OrderDetailId", "OrderDetailId");
            return View();
        }

        // POST: ShippedDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShippedDetailId,OrderDetailId,RecipientName,RecipientAddress,RecipientPhone,ShippedDate,ArrivalDate,IsCompleted")] ShippedDetail shippedDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippedDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "OrderDetailId", "OrderDetailId", shippedDetail.OrderDetailId);
            return View(shippedDetail);
        }

        // GET: ShippedDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippedDetail = await _context.ShippedDetails.FindAsync(id);
            if (shippedDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "OrderDetailId", "OrderDetailId", shippedDetail.OrderDetailId);
            return View(shippedDetail);
        }

        // POST: ShippedDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShippedDetailId,OrderDetailId,RecipientName,RecipientAddress,RecipientPhone,ShippedDate,ArrivalDate,IsCompleted")] ShippedDetail shippedDetail)
        {
            if (id != shippedDetail.ShippedDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippedDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippedDetailExists(shippedDetail.ShippedDetailId))
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
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "OrderDetailId", "OrderDetailId", shippedDetail.OrderDetailId);
            return View(shippedDetail);
        }

        // GET: ShippedDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippedDetail = await _context.ShippedDetails
                .Include(s => s.OrderDetail)
                .FirstOrDefaultAsync(m => m.ShippedDetailId == id);
            if (shippedDetail == null)
            {
                return NotFound();
            }

            return View(shippedDetail);
        }

        // POST: ShippedDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippedDetail = await _context.ShippedDetails.FindAsync(id);
            if (shippedDetail != null)
            {
                _context.ShippedDetails.Remove(shippedDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippedDetailExists(int id)
        {
            return _context.ShippedDetails.Any(e => e.ShippedDetailId == id);
        }
    }
}

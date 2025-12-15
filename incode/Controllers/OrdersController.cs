using incode.Models;
using incode.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incode.Controllers
{
    public class OrdersController : Controller
    {
        private readonly incodedatabaseContext _context;

        public OrdersController(incodedatabaseContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var incodedatabaseContext = _context.Orders.Include(o => o.User);
            return View(await incodedatabaseContext.ToListAsync());
        }
        
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/OrderDetail/5

        public async Task<IActionResult> OrderDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = _context.Products;
            var order = _context.Orders;
            var orderdetail = _context.OrderDetails.Select(p => new OrderDetailViewModel
            {
                OrderId = p.OrderId,
                OrderDetailId = p.OrderDetailId,
                Quantity = p.Quantity,
                UnitPriceAtPurchase = p.UnitPriceAtPurchase,
                IsShipped = p.IsShipped,
                UserId = p.OrderId,
                name = products.Where(s => s.ProductId == p.ProductId).Select(s => s.Name).Single(),
                Notes = order.Where(s => s.OrderId == p.OrderId).Select(s => s.Notes).Single()
            });
            var result = await orderdetail.Where(a => a.OrderId == id).ToListAsync();
            
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Orders/OrderDetail/5/Delete
        [HttpPost, ActionName("DeleteOrderDetail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var order = await _context.OrderDetails.FindAsync(id);
            if (order != null)
            {
                _context.OrderDetails.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(OrderDetail));
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,UserId,TotalPrice,OrderDate,RequiresShipping,Notes")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,UserId,TotalPrice,OrderDate,RequiresShipping,Notes")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Nickname", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}

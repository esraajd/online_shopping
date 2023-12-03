using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class OrdersAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrdersAdmin
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrdersAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrdersId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: OrdersAdmin/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: OrdersAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdersId,CustomerId,Order_Date,Order_Status,Shipping_adress,Billing_address,Paymnet_Method")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.User, "Id", "Id", orders.CustomerId);
            return View(orders);
        }

        // GET: OrdersAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.User, "Id", "Id", orders.CustomerId);
            return View(orders);
        }

        // POST: OrdersAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdersId,CustomerId,Order_Date,Order_Status,Shipping_adress,Billing_address,Paymnet_Method")] Orders orders)
        {
            if (id != orders.OrdersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrdersId))
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
            ViewData["CustomerId"] = new SelectList(_context.User, "Id", "Id", orders.CustomerId);
            return View(orders);
        }

        // GET: OrdersAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrdersId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: OrdersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrdersId == id);
        }
    }
}

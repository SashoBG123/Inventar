using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspBiznes.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace aspBiznes.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Client> _userManager;

        public CartsController(ApplicationDbContext context, UserManager<Client> userManager)
        {
            
            _context = context;
            _userManager = userManager;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Carts.Include(c => c.Clients).Include(c => c.Items).AsQueryable();

            if (!User.IsInRole("Admin"))
            {
                var userId = _userManager.GetUserId(User);
                applicationDbContext = applicationDbContext.Where(c => c.ClientId == userId);
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Clients)
                .Include(c => c.Items)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") && cart.ClientId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create(int? itemId)
        {
            //ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", itemId);
            return View(new Cart { ItemId = itemId ?? 0, Quantity = 1 });
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Quantity")] Cart cart)
        {
            cart.ClientId = _userManager.GetUserId(User);
            cart.DateRegister = DateTimeOffset.Now;
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", cart.ItemId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            if (!CanManageCart(cart))
            {
                return Forbid();
            }

            //ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", cart.ClientId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", cart.ItemId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemId,Quantity")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            var existingCart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCart == null)
            {
                return NotFound();
            }

            if (!CanManageCart(existingCart))
            {
                return Forbid();
            }

            existingCart.ItemId = cart.ItemId;
            existingCart.Quantity = cart.Quantity;
            existingCart.DateRegister = DateTimeOffset.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(existingCart.Id))
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
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", cart.ItemId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Clients)
                .Include(c => c.Items)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            if (!CanManageCart(cart))
            {
                return Forbid();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                if (!CanManageCart(cart))
                {
                    return Forbid();
                }

                _context.Carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }

        private bool CanManageCart(Cart cart)
        {
            return User.IsInRole("Admin") || cart.ClientId == _userManager.GetUserId(User);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BotByePOC.Models;

namespace BotByePOC.Controllers.MVC
{
    public class OutagesController : Controller
    {
        private readonly BotByePOCContext _context;

        public OutagesController(BotByePOCContext context)
        {
            _context = context;    
        }

        // GET: OutagesMVC
        public async Task<IActionResult> Index()
        {
            return View(await _context.Outage.ToListAsync());
        }

        // GET: OutagesMVC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outage = await _context.Outage
                .SingleOrDefaultAsync(m => m.OutageID == id);
            if (outage == null)
            {
                return NotFound();
            }

            return View(outage);
        }

        // GET: OutagesMVC/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OutagesMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OutageID,StartDate,EndDate,Application,Description")] Outage outage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(outage);
        }

        // GET: OutagesMVC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outage = await _context.Outage.SingleOrDefaultAsync(m => m.OutageID == id);
            if (outage == null)
            {
                return NotFound();
            }
            return View(outage);
        }

        // POST: OutagesMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OutageID,StartDate,EndDate,Application,Description")] Outage outage)
        {
            if (id != outage.OutageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutageExists(outage.OutageID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(outage);
        }

        // GET: OutagesMVC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outage = await _context.Outage
                .SingleOrDefaultAsync(m => m.OutageID == id);
            if (outage == null)
            {
                return NotFound();
            }

            return View(outage);
        }

        // POST: OutagesMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outage = await _context.Outage.SingleOrDefaultAsync(m => m.OutageID == id);
            _context.Outage.Remove(outage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OutageExists(int id)
        {
            return _context.Outage.Any(e => e.OutageID == id);
        }
    }
}

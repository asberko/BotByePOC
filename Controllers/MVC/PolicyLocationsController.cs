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
    public class PolicyLocationsController : Controller
    {
        private readonly BotByePOCContext _context;

        public PolicyLocationsController(BotByePOCContext context)
        {
            _context = context;    
        }

        // GET: PolicyLocations
        public async Task<IActionResult> Index()
        {
            return View(await _context.PolicyLocation.ToListAsync());
        }

        // GET: PolicyLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyLocation = await _context.PolicyLocation
                .SingleOrDefaultAsync(m => m.PolicyLocationID == id);
            if (policyLocation == null)
            {
                return NotFound();
            }

            return View(policyLocation);
        }

        // GET: PolicyLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PolicyLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicyID,PolicyLocationID,LocationNumber,LocationDescription,LocationAddress,LocationCity,LocationState,LocationZip")] PolicyLocation policyLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(policyLocation);
        }

        // GET: PolicyLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyLocation = await _context.PolicyLocation.SingleOrDefaultAsync(m => m.PolicyLocationID == id);
            if (policyLocation == null)
            {
                return NotFound();
            }
            return View(policyLocation);
        }

        // POST: PolicyLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolicyID,PolicyLocationID,LocationNumber,LocationDescription,LocationAddress,LocationCity,LocationState,LocationZip")] PolicyLocation policyLocation)
        {
            if (id != policyLocation.PolicyLocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyLocationExists(policyLocation.PolicyLocationID))
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
            return View(policyLocation);
        }

        // GET: PolicyLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyLocation = await _context.PolicyLocation
                .SingleOrDefaultAsync(m => m.PolicyLocationID == id);
            if (policyLocation == null)
            {
                return NotFound();
            }

            return View(policyLocation);
        }

        // POST: PolicyLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policyLocation = await _context.PolicyLocation.SingleOrDefaultAsync(m => m.PolicyLocationID == id);
            _context.PolicyLocation.Remove(policyLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PolicyLocationExists(int id)
        {
            return _context.PolicyLocation.Any(e => e.PolicyLocationID == id);
        }
    }
}

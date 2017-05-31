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
    public class PoliciesController : Controller
    {
        private readonly BotByePOCContext _context;

        public PoliciesController(BotByePOCContext context)
        {
            _context = context;    
        }

        // GET: PoliciesMVC
        public async Task<IActionResult> Index()
        {
            return View(await _context.Policy.ToListAsync());
        }

        // GET: PoliciesMVC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policy = await _context.Policy
                .SingleOrDefaultAsync(m => m.PolicyID == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        // GET: PoliciesMVC/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PoliciesMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicyID,PolicyNumber,EffectiveDate,ExpirationDate,PremiumAmount,InsuredName")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(policy);
        }

        // GET: PoliciesMVC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policy = await _context.Policy.SingleOrDefaultAsync(m => m.PolicyID == id);
            if (policy == null)
            {
                return NotFound();
            }
            return View(policy);
        }

        // POST: PoliciesMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolicyID,PolicyNumber,EffectiveDate,ExpirationDate,PremiumAmount,InsuredName")] Policy policy)
        {
            if (id != policy.PolicyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyExists(policy.PolicyID))
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
            return View(policy);
        }

        // GET: PoliciesMVC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policy = await _context.Policy
                .SingleOrDefaultAsync(m => m.PolicyID == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        // POST: PoliciesMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policy = await _context.Policy.SingleOrDefaultAsync(m => m.PolicyID == id);
            _context.Policy.Remove(policy);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PolicyExists(int id)
        {
            return _context.Policy.Any(e => e.PolicyID == id);
        }
    }
}

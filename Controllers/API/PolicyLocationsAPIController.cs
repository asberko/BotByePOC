using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BotByePOC.Models;

namespace BotByePOC.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Policies/{policyID}/Locations")]
    public class PolicyLocationsAPIController : Controller
    {
        private readonly BotByePOCContext _context;

        public PolicyLocationsAPIController(BotByePOCContext context)
        {
            _context = context;
        }

        // GET: api/Policy/{policyID}/Locations
        [HttpGet]
        public IEnumerable<PolicyLocation> GetPolicyLocation(int policyID)
        {
            return _context.PolicyLocation.Where(l => l.PolicyID == policyID);
        }

        // GET: api/PolicyLocationsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyLocation([FromRoute] int policyID, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var policyLocation = await _context.PolicyLocation.SingleOrDefaultAsync(m => m.PolicyLocationID == id && m.PolicyID == policyID);

            if (policyLocation == null)
            {
                return NotFound();
            }

            return Ok(policyLocation);
        }

        // PUT: api/PolicyLocationsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicyLocation([FromRoute] int id, [FromBody] PolicyLocation policyLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policyLocation.PolicyLocationID)
            {
                return BadRequest();
            }

            _context.Entry(policyLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyLocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PolicyLocationsAPI
        [HttpPost]
        public async Task<IActionResult> PostPolicyLocation([FromBody] PolicyLocation policyLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PolicyLocation.Add(policyLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPolicyLocation", new { id = policyLocation.PolicyLocationID }, policyLocation);
        }

        // DELETE: api/PolicyLocationsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicyLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var policyLocation = await _context.PolicyLocation.SingleOrDefaultAsync(m => m.PolicyLocationID == id);
            if (policyLocation == null)
            {
                return NotFound();
            }

            _context.PolicyLocation.Remove(policyLocation);
            await _context.SaveChangesAsync();

            return Ok(policyLocation);
        }

        private bool PolicyLocationExists(int id)
        {
            return _context.PolicyLocation.Any(e => e.PolicyLocationID == id);
        }
    }
}
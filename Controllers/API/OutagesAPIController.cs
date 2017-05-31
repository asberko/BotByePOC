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
    [Route("api/Outages")]
    public class OutagesAPIController : Controller
    {
        private readonly BotByePOCContext _context;

        public OutagesAPIController(BotByePOCContext context)
        {
            _context = context;
        }

        // GET: api/Outages
        [HttpGet]
        public IEnumerable<Outage> GetOutage()
        {
            return _context.Outage;
        }

        // GET: api/OutagesAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOutage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var outage = await _context.Outage.SingleOrDefaultAsync(m => m.OutageID == id);

            if (outage == null)
            {
                return NotFound();
            }

            return Ok(outage);
        }

        // PUT: api/Outages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOutage([FromRoute] int id, [FromBody] Outage outage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != outage.OutageID)
            {
                return BadRequest();
            }

            _context.Entry(outage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OutageExists(id))
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

        // POST: api/Outages
        [HttpPost]
        public async Task<IActionResult> PostOutage([FromBody] Outage outage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Outage.Add(outage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOutage", new { id = outage.OutageID }, outage);
        }

        // DELETE: api/Outages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOutage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var outage = await _context.Outage.SingleOrDefaultAsync(m => m.OutageID == id);
            if (outage == null)
            {
                return NotFound();
            }

            _context.Outage.Remove(outage);
            await _context.SaveChangesAsync();

            return Ok(outage);
        }

        private bool OutageExists(int id)
        {
            return _context.Outage.Any(e => e.OutageID == id);
        }
    }
}
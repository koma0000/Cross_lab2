﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cross_Kamil.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cross_Kamil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessmenController : ControllerBase
    {
        private readonly BusinessContext _context;

        public BusinessmenController(BusinessContext context)
        {
            _context = context;
        }

        // GET: api/Businessmen
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Businessmen>>> GetBusinessmens()
        {
            return await _context.Businessmens.ToListAsync();
        }

        // GET: api/Businessmen/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Businessmen>> GetBusinessmen(long id)
        {
            var businessmen = await _context.Businessmens.FindAsync(id);

            if (businessmen == null)
            {
                return NotFound();
            }

            return businessmen;
        }

        // PUT: api/Businessmen/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessmen(long id, Businessmen businessmen)
        {
            if (id != businessmen.Id)
            {
                return BadRequest();
            }

            _context.Entry(businessmen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessmenExists(id))
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

        // POST: api/Businessmen
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Businessmen>> PostBusinessmen(Businessmen businessmen)
        {
            _context.Businessmens.Add(businessmen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessmen", new { id = businessmen.Id }, businessmen);
        }

        // DELETE: api/Businessmen/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Businessmen>> DeleteBusinessmen(long id)
        {
            var businessmen = await _context.Businessmens.FindAsync(id);
            if (businessmen == null)
            {
                return NotFound();
            }

            _context.Businessmens.Remove(businessmen);
            await _context.SaveChangesAsync();

            return businessmen;
        }

        private bool BusinessmenExists(long id)
        {
            return _context.Businessmens.Any(e => e.Id == id);
        }
    }
}

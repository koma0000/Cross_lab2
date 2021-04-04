using System;
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
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Businessmen>>> GetBusinessmens()
        {
            _context.BusinessmensInclude();
            return await _context.Businessmens.ToListAsync();
        }

        // GET: api/Businessmen/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Businessmen>> GetBusinessmen(long id)
        {
            var businessmen = await _context.Businessmens.FindAsync(id);

            if (businessmen == null)
            {
                return NotFound();
            }

            return businessmen;
        }


        [HttpGet("CompanyOfBusnesmen")]
        [Authorize]
        public Dictionary<string, List<string>> getCompanyOfBusnessmen()
        {
            return _context.GetCompanyOfBusinessmen();
        }


        [HttpGet("GetForebs")]
     //   [Authorize]
        public Dictionary<string, long> getForebs()
        {
            return _context.GetForebs();
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
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Businessmen>> PostBusinessmen(Businessmen businessmen)
        {
            _context.Businessmens.Add(businessmen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessmen", new { id = businessmen.Id }, businessmen);
        }



        [HttpPost("add/{bussnesmenId}/{companyId}")]
        [Authorize(Roles = "admin")]
        public string AddAppForUser(long bussnesmenId, long companyId)
        {
            return _context.SetBussinesmenOfComapany(bussnesmenId, companyId);
        }


        //[HttpPost("add")]
        //[Authorize(Roles = "admin")]
        //public string AddAppForUser([FromForm] long bussnesmenId, [FromForm] long companyId)
        //{
        //    return _context.SetBussinesmenOfComapany(bussnesmenId, companyId);
        //}


        // DELETE: api/Businessmen/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
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

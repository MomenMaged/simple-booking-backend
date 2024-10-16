using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Booking.Models;
using Booking.ViewModels;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ResContext _context;

        public ResourcesController(ResContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult< IEnumerable< ResponseResources>>> GetResources()
        {
            List<ResponseResources> response = new List<ResponseResources>();
            List<Resources> result = await _context.Resources.ToListAsync();
            for(int i = 0; i < result.Count; i++)
            {
                response.Add(new ResponseResources
                {
                   Id = result[i].Id,
                    Name=result[i].Name

                });

            }
          
            return response;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseResources>> GetResources(int id)
        {
            var resources = await _context.Resources.FindAsync(id);


            if (resources == null)
            {
                return NotFound();
            }

            ResponseResources response = new ResponseResources {
                Id = resources.Id,
                Name=resources.Name
            };

            return response;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutResources(int id, Resources resources)
        {
            if (id != resources.Id)
            {
                return BadRequest();
            }

            _context.Entry(resources).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourcesExists(id))
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

        [HttpPost]
        public async Task<ActionResult<ResponseResources>> PostResources(Resources resources)
        {
            _context.Resources.Add(resources);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResources", new { id = resources.Id }, resources);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResources(int id)
        {
            var resources = await _context.Resources.FindAsync(id);
            if (resources == null)
            {
                return NotFound();
            }

            _context.Resources.Remove(resources);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResourcesExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inforce.Server.DAL;
using Inforce.Server.Domain;

namespace Inforce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AboutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/About/description
        [HttpGet("description")]
        public async Task<IActionResult> GetDescription()
        {
            var about = await _context.About.FirstOrDefaultAsync();
            if (about == null)
            {
                return NotFound();
            }
            return Ok(new { description = about.Description });
        }

        // PUT: api/About/description
        [HttpPut("description")]
        public async Task<IActionResult> UpdateDescription([FromBody] About about)
        {
            var existingAbout = await _context.About.FirstOrDefaultAsync();
            if (existingAbout == null)
            {
                return NotFound();
            }

            existingAbout.Description = about.Description;
            _context.About.Update(existingAbout);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

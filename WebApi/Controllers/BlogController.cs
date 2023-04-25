using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab6.WebApi.Data;
using Lab6.WebApi.Models;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogEntry>>> GetEntries()
        {
            if (_context.Entries == null)
            {
                return NotFound();
            }
            return await _context.Entries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogEntry>> GetBlogEntry(uint id)
        {
            if (_context.Entries == null)
            {
                return NotFound();
            }
            var blogEntry = await _context.Entries.FindAsync(id);

            if (blogEntry == null)
            {
                return NotFound();
            }

            return blogEntry;
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogEntry(uint id, BlogEntry blogEntry)
        {
            if (id != blogEntry.BlogEntryId)
            {
                return BadRequest();
            }

            _context.Entry(blogEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogEntryExists(id))
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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogEntry>> PostBlogEntry(BlogEntry blogEntry)
        {
            if (_context.Entries == null)
            {
                return Problem("Entity set 'BlogContext.Entries'  is null.");
            }
            _context.Entries.Add(blogEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogEntry", new { id = blogEntry.BlogEntryId }, blogEntry);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogEntry(uint id)
        {
            if (_context.Entries == null)
            {
                return NotFound();
            }
            var blogEntry = await _context.Entries.FindAsync(id);
            if (blogEntry == null)
            {
                return NotFound();
            }

            _context.Entries.Remove(blogEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogEntryExists(uint id)
        {
            return (_context.Entries?.Any(e => e.BlogEntryId == id)).GetValueOrDefault();
        }
    }
}

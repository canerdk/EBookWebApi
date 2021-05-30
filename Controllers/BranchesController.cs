using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EBookWebApi.DAL.Context;
using EBookWebApi.DAL.Entities;

namespace EBookWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly EBookDbContext _context;

        public BranchesController(EBookDbContext context)
        {
            _context = context;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranches()
        {
            return await _context.Branches.ToListAsync();
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Branch>> GetBranch(Guid id)
        {
            var branch = await _context.Branches.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            return branch;
        }

        [HttpGet("getBranchWithGrades")]
        public async Task<ActionResult<List<Branch>>> GetGradesWithBranch()
        {
            List<Branch> branches = await _context.Branches.ToListAsync();
            foreach (var branch in branches)
            {
                branch.Grades = _context.Grades.Where(x => x.BranchId == branch.Id).OrderByDescending(x => x.Name).ToList();
            }
            return branches;
        }

        [HttpGet("getGradesWithBranchId")]
        public IQueryable<Grade> GetGradesWithBranch(Guid branchId)
        {
            var result = _context.Branches.Where(c => c.Id == branchId).SelectMany(m => m.Grades);
            return result;
        }

        // PUT: api/Branches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranch(Guid id, Branch branch)
        {
            if (id != branch.Id)
            {
                return BadRequest();
            }

            _context.Entry(branch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
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

        // POST: api/Branches
        [HttpPost]
        public async Task<ActionResult<Branch>> PostBranch(Branch branch)
        {
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBranch", new { id = branch.Id }, branch);
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Branch>> DeleteBranch(Guid id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return branch;
        }

        private bool BranchExists(Guid id)
        {
            return _context.Branches.Any(e => e.Id == id);
        }
    }
}

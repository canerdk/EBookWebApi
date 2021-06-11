using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EBookWebApi.DAL.Context;
using EBookWebApi.DAL.Entities;
using System.IO;
using Newtonsoft.Json;

namespace EBookWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly EBookDbContext _context;

        public DocumentsController(EBookDbContext context)
        {
            _context = context;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(Guid id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        [HttpGet("getimages")]
        public IActionResult GetPdf(string docName)
        {
            List<string> imageName = new List<string>();
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\pdfimages\\" + docName + "\\");
            try
            {
                string[] fileEntries = Directory.GetFiles(imagePath);
                foreach (var item in fileEntries)
                {
                    var ext = Path.GetExtension(item);
                    var name = Path.GetFileNameWithoutExtension(item);
                    string newName = "https://service.artizekaekutuphane.com/wwwroot/pdfimages/" + docName + "/" + name + ext;
                    imageName.Add(newName);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return new JsonResult(imageName);
        }

        // PUT: api/Documents/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(Guid id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            _context.Entry(document).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
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

        // POST: api/Documents
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocument", new { id = document.Id }, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Document>> DeleteDocument(Guid id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return document;
        }

        private bool DocumentExists(Guid id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}

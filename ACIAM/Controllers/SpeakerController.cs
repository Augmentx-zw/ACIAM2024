using Microsoft.AspNetCore.Mvc;
using ACIAM.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ACIAM.Data;

namespace ACIAM.Controllers
{
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SpeakerController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Speaker
        public async Task<IActionResult> Index()
        {
            return View(await _context.Speakers.ToListAsync());
        }

        // GET: Speaker/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speakers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // GET: Speaker/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Speaker/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Category,SubTitle,Description")] Speaker speaker, IFormFile imageFile)
        {
            if (imageFile == null)
            {
                ModelState.Remove("imageFile");
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string uniqueFileName = UploadedFile(imageFile);
                    speaker.Image = uniqueFileName;
                }

                _context.Add(speaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        // GET: Speaker/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speakers.FindAsync(id);
            if (speaker == null)
            {
                return NotFound();
            }
            return View(speaker);
        }

        // POST: Speaker/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Category,SubTitle,Description,Image")] Speaker speaker)
        {
            if (id != speaker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        // GET: Speaker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speakers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // POST: Speaker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speaker = await _context.Speakers.FindAsync(id);
            _context.Speakers.Remove(speaker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(int id)
        {
            return _context.Speakers.Any(e => e.Id == id);
        }

        private string UploadedFile(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}

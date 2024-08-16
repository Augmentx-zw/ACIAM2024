using ACIAM.Data;
using ACIAM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ACIAM.Controllers
{
    public class ConferenceController : Controller
    {
		private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ConferenceController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
            _webHostEnvironment = webHostEnvironment;
        }

		// GET: ParticipantController
		public async Task<IActionResult> Index()
		{
			return View(await _context.Conferences.ToListAsync());
		}

		// GET: ParticipantController/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var conference = await _context.Conferences
				.FirstOrDefaultAsync(m => m.Id == id);
			if (conference == null)
			{
				return NotFound();
			}

			return View(conference);
		}

		// GET: ParticipantController/Create
		public IActionResult Create()
        {
            return View();
        }

        // POST: ParticipantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Category,SubTitle,Description")] Conference conference, IFormFile imageFile)
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
                    conference.Image = uniqueFileName;
                }

                _context.Add(conference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
			return View(conference);
		}

		// GET: ParticipantController/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var conference = await _context.Conferences.FindAsync(id);
			if (conference == null)
			{
				return NotFound();
			}
			return View(conference);
		}

		// POST: ParticipantController/Edit/5
		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Category,SubTitle,Description,Image")] Conference conference)
		{
			if (id != conference.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(conference);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ConferenceExists(conference.Id))
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
			return View(conference);
		}

		// GET: ParticipantController/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var conference = await _context.Conferences
				.FirstOrDefaultAsync(m => m.Id == id);
			if (conference == null)
			{
				return NotFound();
			}

			return View(conference);
		}

		// POST: ParticipantController/Delete/5
		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var conference = await _context.Conferences.FindAsync(id);
			_context.Conferences.Remove(conference);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ConferenceExists(int id)
		{
			return _context.Conferences.Any(e => e.Id == id);
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

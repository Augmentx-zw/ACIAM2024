using ACIAM.Data;
using ACIAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ACIAM.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

        public async Task<IActionResult> OrganisingCommittee()
        {
            var organisingConferences = await _context.Conferences
                .Where(c => c.Category.ToLower() == "organising")
                .ToListAsync();

            return View(organisingConferences);
        }

        public async Task<IActionResult> LocalOrganisingCommittee()
		{
            var local = await _context.Conferences
                .Where(c => c.Category.ToLower() == "local")
                .ToListAsync();

            return View(local);
        }

		public async Task<IActionResult> ScientificCommittee()
		{
            var scientific = await _context.Conferences
                .Where(c => c.Category.ToLower() == "local")
                .ToListAsync();

            return View(scientific);
        }

		public async Task<IActionResult> HonoraryCommittee()
		{
            var honorary = await _context.Conferences
                .Where(c => c.Category.ToLower() == "local")
                .ToListAsync();

            return View(honorary);
        }

		public IActionResult Speakers()
		{
			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult Partners()
		{
			return View();
		}

		public IActionResult OnlineRegister()
		{
			return View();
		}

		public async Task<IActionResult> PlenarySpeakers()
		{
            var plenary = await _context.Speakers
                .Where(c => c.Category.ToLower() == "plenary")
                .ToListAsync();

            return View(plenary);
        }

		public async Task<IActionResult> InvitedSpeakers()
		{
            var plenary = await _context.Speakers
                .Where(c => c.Category.ToLower() == "invited")
                .ToListAsync();

            return View(plenary);
        }

		public async Task<IActionResult> SpecialSpeakers()
		{
            var special = await _context.Speakers
                .Where(c => c.Category.ToLower() == "special")
                .ToListAsync();

            return View(special);
        }

		public async Task<IActionResult> HonorarySpeakers()
		{
            var honorary = await _context.Speakers
                .Where(c => c.Category.ToLower() == "honorary")
                .ToListAsync();

            return View(honorary);
        }

		public async Task<IActionResult> SpeakerSingle(int id)
		{
			return View();
		}
		
		public async Task<IActionResult> CommitteeSingle(int id)
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

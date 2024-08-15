using ACIAM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ACIAM.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult OrganisingCommittee()
		{
			return View();
		}

		public IActionResult LocalOrganisingCommittee()
		{
			return View();
		}

		public IActionResult ScientificCommittee()
		{
			return View();
		}

		public IActionResult HonoraryCommittee()
		{
			return View();
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

		public IActionResult PlenarySpeakers()
		{
			return View(); // Returns the PlenarySpeakers.cshtml view
		}

		public IActionResult InvitedSpeakers()
		{
			return View(); // Returns the InvitedSpeakers.cshtml view
		}

		public IActionResult SpecialSpeakers()
		{
			return View(); // Returns the SpecialSpeakers.cshtml view
		}

		public IActionResult HonorarySpeakers()
		{
			return View(); // Returns the HonorarySpeakers.cshtml view
		}

		public IActionResult SpeakerSingle()
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

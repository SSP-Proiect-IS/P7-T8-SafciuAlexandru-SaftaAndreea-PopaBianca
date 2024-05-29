using CanvasHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CanvasHub.Controllers
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
        [Authorize]
        public IActionResult TestingFrontend()
        {
            return View();
        }
        //[Route("Home/Event")]
        //public IActionResult Event()
        //{
        //    return View();
        //}

        [Route("Home/UserProfile")]
        public IActionResult UserProfile()
        {
            return View();
        }

        [Route("Home/Resources")]
        public IActionResult Resources()
        {
            return View();
        }

        [Route("Home/Resource1")]
        public IActionResult Resource1()
        {
            return View();
        }

        [Route("Home/Resource2")]
        public IActionResult Resource2()
        {
            return View();
        }

        [Route("Home/Resource3")]
        public IActionResult Resource3()
        {
            return View();
        }

        [Route("Home/Comunity")]
        public IActionResult Comunity()
        {
            return View();
        }

        [Route("Home/MemberProfile")]
        public IActionResult MemberProfile()
        {
            return View();
        }

        [Route("Home/EventsPage")]
        public IActionResult EventsPage()
        {
            return View();
        }

        [Route("Home/SingleEvent")]
        public IActionResult SingleEvent()
        {
            return View();
        }

        [Route("Home/InvitationEvent")]
        public IActionResult InvitationEvent()
        {
            return View();
        }

        [Route("Home/AddResource")]
        public IActionResult AddResource()
        {
            return View();
        }

        [Route("Home/Calendar")]
        public IActionResult Calendar()
        {
            return View();
        }

        [Route("Home/CreateEvent")]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [Route("Home/PaymentPage")]
        public IActionResult PaymentPage()
        {
            return View();
        }

        [Route("Home/InboxUser")]
        public IActionResult InboxUser()
        {
            return View();
        }


        [Route("Home/InboxMember")]
        public IActionResult InboxMember()
        {
            return View();
        }

        [Route("Home/InvitationEventMember")]
        public IActionResult InvitationEventMember()
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

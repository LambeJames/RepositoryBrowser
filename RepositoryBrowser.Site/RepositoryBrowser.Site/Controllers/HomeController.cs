using RepositoryBrowser.Interfaces.Services;
using RepositoryBrowser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RepositoryBrowser.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserRequestViewModel request)
        {
            if (ModelState.IsValid)
                return RedirectToAction("UserDetails", request);
            else
                return View(request);
        }

        public async Task<ActionResult> UserDetails(UserRequestViewModel request)
        {
            if (!ModelState.IsValid)
                return View("NotFound");

            var user = await _userService.Get(request.Name);

            if (user == null)
                return View("NotFound");
            else
                return View(user);
        }
    }
}
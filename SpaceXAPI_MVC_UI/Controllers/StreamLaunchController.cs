using Microsoft.AspNetCore.Mvc;
using SpaceXAPI_MVC_UI.Models;
using SpaceXAPI_MVC_UI.Service;

namespace SpaceXAPI_MVC_UI.Controllers
{
    public class StreamLaunchController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var launches = new LaunchModel();
            StreamLaunchService getLaunchesViaStream = new StreamLaunchService();
            launches = await getLaunchesViaStream.GetLaunchesWithStream();
            return View(launches);
        }
    }
}

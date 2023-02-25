using Microsoft.AspNetCore.Mvc;
using SpaceXAPI_MVC_UI.Services;

namespace SpaceXAPI_MVC_UI.Controllers
{
    public class LaunchController : Controller
    {
        public async Task<IActionResult> Index()
        {
            CRUDService getLaunches = new CRUDService();
            var launches = await getLaunches.GetResourceThroughHttpRequestMessage();
            return View(launches);
        }
    }
}

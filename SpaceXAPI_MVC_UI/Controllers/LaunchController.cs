using Microsoft.AspNetCore.Mvc;
using SpaceXAPILaunchesTake2.Services;
using SpaceXAPILaunchesTake2.Models;

namespace SpaceXAPILaunchesTake2.Controllers
{
    public class LaunchController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var launches = new List<LaunchModel>();
            CRUDService getLaunches = new CRUDService();
            launches = (List<LaunchModel>)await getLaunches.GetResourceThroughHttpRequestMessage();
            return View(launches);
        }
    }
}

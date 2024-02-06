using Microsoft.AspNetCore.Mvc;

namespace RESTaurantAPI.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace RESTaurantAPI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

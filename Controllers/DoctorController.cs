using Microsoft.AspNetCore.Mvc;

namespace ClinicFrontend.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

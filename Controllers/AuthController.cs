using System.Text;
using ClinicFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClinicFrontend.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud");

                var json = JsonConvert.SerializeObject(new
                {
                    email = model.Email,
                    password = model.Password
                }
                     );
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/auth/login", content);

                var result = await response.Content.ReadAsStringAsync();

                dynamic data = JsonConvert.DeserializeObject(result);

                HttpContext.Session.SetString("token", (string)data.token);

                return RedirectToAction("Dashbord", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}

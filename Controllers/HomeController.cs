using System.Net.Http.Headers;
using System.Text;
using ClinicFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace ClinicFrontend.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Dashbord()
        {
            var token = HttpContext.Session.GetString("token");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("/admin/clinic");

                var result = await response.Content.ReadAsStringAsync();

                ViewBag.Result = result;

            }
            return View();
        }

        public async Task<IActionResult> Users()
        {
            var token = HttpContext.Session.GetString("token");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = await client.GetAsync("/admin/users");

                var json = await res.Content.ReadAsStringAsync();

                var users = JsonConvert.DeserializeObject<List<UserModel>>(json);

                return View(users);
            }
        }
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel user)
        {
            var token = HttpContext.Session.GetString("token");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud");

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await client.PostAsync("/admin/users", content);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Users");
                }

                var error = await res.Content.ReadAsStringAsync();
                ViewBag.Result = error;

                return View();
            }
        }
    }
}
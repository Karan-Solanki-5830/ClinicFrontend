using System.Net.Http.Headers;
using System.Text;
using ClinicFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClinicFrontend.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentModel a)
        {
            var token = HttpContext.Session.GetString("token");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud");

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var payload = new
                {
                    appointmentDate = a.appointmentDate.Substring(0, 10),
                    timeSlot = a.timeSlot.Trim()
                };

                var json = JsonConvert.SerializeObject(payload);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await client.PostAsync("/appointments", content);

                var result = await res.Content.ReadAsStringAsync();

                ViewBag.Result = result;

                return View();
            }
        }

        public async Task<IActionResult> List()
        {
            var token = HttpContext.Session.GetString("token");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = await client.GetAsync("/appointments/my");

                var json = await res.Content.ReadAsStringAsync();

                var appointments = JsonConvert.DeserializeObject<List<AppointmentModel>>(json);

                return View(appointments);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var token = HttpContext.Session.GetString("token");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = await client.GetAsync("/appointments/" + id);

                var json = await res.Content.ReadAsStringAsync();

                dynamic data = JsonConvert.DeserializeObject(json);

                return View(data);
            }
        }
    }
}


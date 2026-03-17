using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClinicFrontend.Controllers
{
    public class PrescriptionController : Controller
    {
        public async Task<IActionResult> List(int id)
        {
            var token = HttpContext.Session.GetString("token");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = await client.GetAsync("/prescriptions/my");

                var json = await res.Content.ReadAsStringAsync();

                dynamic data = JsonConvert.DeserializeObject(json);

                return View(data);
            }
        }
    }
}

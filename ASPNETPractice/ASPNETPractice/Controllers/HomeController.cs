using ASPNETPractice.Models;
using ASPNETPractice.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Numerics;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace ASPNETPractice.Controllers
{
    public class HomeController : BaseController
    {
        private readonly DatabaseService _databaseService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            using(HttpClient client = new HttpClient())
            {
                string url = "https://www.w3schools.com/xml/cd_catalog.xml";
                HttpResponseMessage response = await client.GetAsync(url);
                string data = await response.Content.ReadAsStringAsync();
                XmlSerializer serializer = new XmlSerializer(typeof(Catalog));
                using(StringReader reader = new StringReader(data))
                {
                    Catalog catalog = (Catalog)serializer.Deserialize(reader);
                    await _databaseService.InitDatabaseAsync(catalog.CDs);
                }
            }
            ViewBag.CDList = await _databaseService.GetAllCDAsync();
            return View();
        }
        public IActionResult RedirectToForm()
        {
            return RedirectToAction("Form");
        }
        public IActionResult Edit(int Id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Form(ReservationForm form)
        {
            if(ModelState.IsValid)
            {
                //if()
                string regexString = @"(\+420|\+421)[0-9]{9}";
                Regex r = new Regex(regexString);
                Match m = r.Match(form.TelNumber);
                if(m.Value != form.TelNumber)
                {
                    ModelState.AddModelError("TelNumber", "Spatne cislo");
                }
                if(form.TimeFrom > form.TimeTill)
                {
                    ModelState.AddModelError("TimeFrom", "Čas od musí být menší než čas do");
                }
                string jsonString = JsonSerializer.Serialize(form);
                using(FileStream fs = new FileStream("rezervace.json", FileMode.Append))
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(jsonString);
                }
                return RedirectToAction("Confirm");
            }
            return View();
        }
        public IActionResult Form()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Confirm()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

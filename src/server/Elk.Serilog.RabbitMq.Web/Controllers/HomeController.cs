using Elk.Serilog.RabbitMq.Model;
using Elk.Serilog.RabbitMq.Service;
using Elk.Serilog.RabbitMq.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Elk.Serilog.RabbitMq.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService _service;

        public HomeController(ILogger<HomeController> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("HomeController Index Page Viewed {date}", DateTime.Now);

            return View(_service.GetSampleData());
        }

        public IActionResult Privacy()
        {
            try
            {
                throw new Exception($"Sample Error");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error --> {exception.Message}");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Post(Customer model)
        {
            try
            {
                _service.CustomerProcess(model);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}

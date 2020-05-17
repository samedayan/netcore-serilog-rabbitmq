using Elk.Serilog.RabbitMq.Client.Model;
using Elk.Serilog.RabbitMq.Client.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Elk.Serilog.RabbitMq.Client.Controllers
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
            try
            {
                
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message} - {DateTime.Now:dd.MM.yyyy}");
            }

            return View(_service.GetSampleData());
        }

        public IActionResult Privacy()
        {
            _logger.LogError($"Sample Error");

            return StatusCode(500, new Exception($"Sample Error"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Post()
        {
            return Ok();
        }
    }
}

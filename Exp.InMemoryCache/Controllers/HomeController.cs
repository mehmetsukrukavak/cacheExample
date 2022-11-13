using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Exp.InMemoryCache.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Exp.InMemoryCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMemoryCache _memoryCache;
        public HomeController(ILogger<HomeController> logger,
                              IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            options.SlidingExpiration = TimeSpan.FromSeconds(10);

            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);
            return View();
        }

        public IActionResult Privacy()
        {
            _memoryCache.TryGetValue("zaman", out string zamancache);
            ViewBag.zaman = zamancache;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


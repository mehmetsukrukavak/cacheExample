using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Exp.IDistributedCacheRedisApp.Web.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Exp.IDistributedCacheRedisApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IDistributedCache _distributedCache;

    public HomeController(ILogger<HomeController> logger, IDistributedCache distributedCache)
    {
        _logger = logger;
        _distributedCache = distributedCache;
    }

    public IActionResult Index()
    {
        DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(1)
        };

        _distributedCache.SetString("sehir", "Mehmet", cacheEntryOptions);
        return View();
    }

    public IActionResult Privacy()
    {
        var value = _distributedCache.GetString("sehir");


        _distributedCache.Remove("sehir");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


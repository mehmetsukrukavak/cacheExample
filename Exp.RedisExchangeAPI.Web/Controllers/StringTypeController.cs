using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exp.RedisExchangeAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Exp.RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }

        public IActionResult Index()
        {
           
            db.StringSet("name", "MehmetNew");
            db.StringSet("ziyaretci", 101);
            return View();
        }

        public IActionResult Show()
        {
            
            var name = db.StringGet("name");
            
            ViewBag.ziyaretci = db.StringIncrementAsync("ziyaretci", 1).Result;
            if (name.HasValue)
            {
                ViewBag.value = name;
            }

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exp.RedisExchangeAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Exp.RedisExchangeAPI.Web.Controllers
{
    public class ListTypeController : Controller
    {

        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string listKey = "names";
        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(1);
        }
        
        public IActionResult Index()
        {
            List<string> nameList = new List<string>();

            if (db.KeyExists(listKey))
            {
                db.ListRange(listKey).ToList().ForEach(x =>
                {
                    nameList.Add(x.ToString());
                });
            }
            return View(nameList);
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            db.ListRightPush(listKey, name);
            return RedirectToAction("Index");
        }

        
        public IActionResult DeleteItem(string name)
        {
            db.ListRemoveAsync(listKey, name).Wait();
            return RedirectToAction("Index");
        }


    }
}


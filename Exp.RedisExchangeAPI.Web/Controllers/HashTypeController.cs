using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exp.RedisExchangeAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exp.RedisExchangeAPI.Web.Controllers
{
    public class HashTypeController : BaseController
    {
        private string hashKey = "hashtable";
        public HashTypeController(RedisService redisService) : base(redisService)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            if (db.KeyExists(hashKey))
            {
                db.HashGetAll(hashKey).ToList().ForEach(x =>
                {
                    keyValuePairs.Add(x.Name.ToString(),x.Value.ToString());
                });
            }
            return View(keyValuePairs);
        }

        [HttpPost]
        public IActionResult Add(string key, string value)
        {
            db.HashSet(hashKey, key, value);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            db.HashDeleteAsync(hashKey, name).Wait();
            return RedirectToAction("Index");
        }
    }
}


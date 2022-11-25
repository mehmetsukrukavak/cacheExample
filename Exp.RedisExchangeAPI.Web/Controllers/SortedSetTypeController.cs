using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exp.RedisExchangeAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exp.RedisExchangeAPI.Web.Controllers
{
    public class SortedSetTypeController : BaseController
    {
      
        private string listKey = "sortedsetnames";

        public SortedSetTypeController(RedisService redisService) : base(redisService)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();
            if (db.KeyExists(listKey))
            {
                //db.SortedSetScan(listKey).ToList().ForEach(x =>
                //{
                //    list.Add(x.ToString());
                //});

                db.SortedSetRangeByRank(listKey,order: Order.Descending).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, int score)
        {
            
            db.KeyExpire(listKey, DateTime.Now.AddMinutes(1));
            

            db.SortedSetAdd(listKey, name,score);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {

            //db.KeyExpire(listKey, DateTime.Now.AddMinutes(1));


            db.SortedSetRemove(listKey,name);
            return RedirectToAction("Index");
        }
    }
}


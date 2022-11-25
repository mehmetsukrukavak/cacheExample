using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exp.RedisExchangeAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;


namespace Exp.RedisExchangeAPI.Web.Controllers
{
    public class SetTypeController : BaseController
    {
       
        private string listKey = "setnames";

        public SetTypeController(RedisService redisService) : base(redisService)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            HashSet<string> hashList = new HashSet<string>();
            if (db.KeyExists(listKey))
            {
                db.SetMembers(listKey).ToList().ForEach(x => {
                    hashList.Add(x.ToString());
                });
            }

            return View(hashList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            //if (!db.KeyExists(listKey))
            // {
            db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));
            //}

            db.SetAdd(listKey, name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(string name)
        {
            await db.SetRemoveAsync(listKey, name);
            return RedirectToAction("Index");
        }
    }
}


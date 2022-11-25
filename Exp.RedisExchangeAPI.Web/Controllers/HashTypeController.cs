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
        public HashTypeController(RedisService redisService) : base(redisService)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}


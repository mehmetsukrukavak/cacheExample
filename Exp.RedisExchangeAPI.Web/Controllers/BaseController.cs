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
    public class BaseController : Controller
    {
        private readonly RedisService _redisService;
        protected readonly IDatabase db;

        public BaseController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }
       
        
    }
}


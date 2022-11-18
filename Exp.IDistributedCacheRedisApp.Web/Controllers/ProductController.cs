using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Exp.IDistributedCacheRedisApp.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace Exp.IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IDistributedCache _distributedCache;

        public ProductController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public IActionResult Index()
        {
           
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30)
            };
            for (int i = 1; i < 3; i++)
            {
                Product product = new Product
                {
                    Id = i,
                    Name = $"Defter {i}",
                    Price = 45 * i
                };
               
                string jsonProduct = JsonConvert.SerializeObject(product);
                Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProduct);
                //String
                _distributedCache.SetString($"product:{i}", jsonProduct, cacheEntryOptions);
                //Byte
                _distributedCache.Set($"byteproduct:{i}", byteProduct);
            }
            

            
            return View();
        }

      

        public IActionResult ShowString()
        {
            List<Product> productList = new List<Product>();
            for (int i = 1; i < 3; i++)
            {
                string jsonString = _distributedCache.GetString($"product:{i}");

                Product productString = JsonConvert.DeserializeObject<Product>(jsonString);
                productList.Add(productString);
            }

            return View(productList);
        }
        public IActionResult ShowByte()
        {
            List<Product> productList = new List<Product>();
            for (int i = 1; i < 3; i++)
            {
                Byte[] jsonByte = _distributedCache.Get($"product:{i}");


                string jsonString = Encoding.UTF8.GetString(jsonByte);
                Product productString = JsonConvert.DeserializeObject<Product>(jsonString);
                productList.Add(productString);
            }

            return View(productList);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Core.Entities.Concrete;
using WebApi.Service.Redis;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private static List<CurrencyRate> currencyRatelist = new List<CurrencyRate>();
        private ICacheService _cacheService;
            
        public RedisController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("RedisList")]
        [Authorize(Roles = "Admin")]
        public IActionResult deneme()
        {
            if (_cacheService.Any("money"))
            {
                var books = _cacheService.Get<List<CurrencyRate>>("money");
                return Ok(books);
            }

            return Ok("Redis Empty!");
        }


        [HttpPost("RedisDestroy")]
        [Authorize(Roles = "Admin")]
        public void DeleteAllRedis()
        {

            _cacheService.Remove("money");
            currencyRatelist = new List<CurrencyRate>();

        }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApi.Core.DataAccess.EntityFrameworkDal.Concrete;
using WebApi.Core.DataAccess.TcmbAccess.Concrete;
using WebApi.Core.Entities.Concrete;
using WebApi.Core.Entities.Dtos;
using WebApi.Service.Abstract;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {
        private ICurrencyRateService _currencyRateService;

        public CurrencyRateController(ICurrencyRateService currencyRateService)
        {
            _currencyRateService = currencyRateService;
        }

        [HttpGet("Search")]
        [Authorize()]
        public IActionResult AddCurrencyRate(string code)
        {
            
            var currencyRate = _currencyRateService.Add(code);

           if (currencyRate == null)
           {
               return BadRequest("Geçersiz veya kayıtlı olmayan bir para birimi girdiniz!");
           }

           return Ok(currencyRate);

        }


        [HttpGet("deneme")]
        public List<EmailAndUsernameDto> allemail()
        {

            EfUserDal deneme = new EfUserDal();
            List<EmailAndUsernameDto> a = new List<EmailAndUsernameDto>();
            a = deneme.GetAllEmailsAndUsernames();
            return a;

        }

    }
}

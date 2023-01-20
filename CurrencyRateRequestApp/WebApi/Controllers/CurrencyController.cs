using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.DataAccess.TcmbAccess.Concrete;
using WebApi.Core.Entities.Concrete;
using WebApi.Service.Abstract;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private ICurrencyService _currencyService;
       
        
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
         
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromForm] Currency currency)
        {
            var result = _currencyService.Add(currency);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);

        }

        [HttpPost("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult delete([FromForm] Currency currency)
        {
            var result = _currencyService.Delete(currency);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);

        }
        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public IActionResult update([FromForm] Currency currency)
        {
            var result = _currencyService.Update(currency);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);

        }


    }
}

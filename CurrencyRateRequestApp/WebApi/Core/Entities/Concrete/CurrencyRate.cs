using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Core.Entities.Abstract;

namespace WebApi.Core.Entities.Concrete
{
    public class CurrencyRate:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime CurrencyDate { get; set; }
        public decimal ForexBuying { get; set; }
        public decimal ForexSelling { get; set; }
        public decimal BanknoteBuying { get; set; }
        public decimal BanknoteSelling { get; set; }

    }

}

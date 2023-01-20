using System.ComponentModel.DataAnnotations;
using WebApi.Core.Entities.Abstract;

namespace WebApi.Core.Entities.Concrete
{
    public class Currency:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

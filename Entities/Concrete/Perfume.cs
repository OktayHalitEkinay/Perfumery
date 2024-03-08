using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Perfume : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PerfumeId { get; set; }
        public string PerfumeName { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public decimal Price { get; set; }
        public string PhotoPath { get; set; }
     
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}

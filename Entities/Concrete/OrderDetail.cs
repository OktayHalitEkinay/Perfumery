using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class OrderDetail : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        [ForeignKey("PerfumeId")]
        public int PerfumeId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public virtual Perfume Perfume { get; set; }
        public virtual Order Order { get; set; }
    }
}

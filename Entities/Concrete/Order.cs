using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Order:IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [ForeignKey("UserDetailId")]
        public int UserDetailId { get; set; }
        public string ShipAddress { get; set; }
        public string Phone { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual UserDetail UserDetail { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

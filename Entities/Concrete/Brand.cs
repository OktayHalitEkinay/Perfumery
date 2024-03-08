using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Brand : IEntity
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Perfume> Perfumes { get; set; }
    }
}

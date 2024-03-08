using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Perfume
{
    public class AddPerfumeDto
    {
        public string PerfumeName { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public string PhotoPath { get; set; }
    }
}

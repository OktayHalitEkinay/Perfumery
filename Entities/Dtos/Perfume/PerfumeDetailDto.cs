namespace Entities.Dtos.Perfume
{
    public class PerfumeDetailDto
    {
        public int PerfumeId { get; set; }
        public string PerfumeName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public string PhotoPath { get; set; }
    }
}

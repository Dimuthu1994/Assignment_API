using System.ComponentModel.DataAnnotations;

namespace Assignment_API.Models.Dto
{
    public class ProductCreateDto
    {
        [Required]
        public bool Active { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public double RetailPrice { get; set; }
        [Required]
        public double SalePrice { get; set; }
        [Required]
        public double LowestPrice { get; set; }
    }
}

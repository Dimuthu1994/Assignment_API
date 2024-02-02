using System.ComponentModel.DataAnnotations;

namespace Assignment_API.Models.Dto
{
    public class ProductUpdateDto
    {
        [Required]
        public int ProductId { get; set; }
        public bool Active { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public double RetailPrice { get; set; }
        public double SalePrice { get; set; }
        public double LowestPrice { get; set; }
    }
}

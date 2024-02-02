using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_API.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
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

using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        
        [Range(0, int.MaxValue)] 
        public int Stock { get; set; }
        
        public int CategoryId { get; set; } // foreign key to Category
        
        public Category? Category { get; set; } 
    }
}
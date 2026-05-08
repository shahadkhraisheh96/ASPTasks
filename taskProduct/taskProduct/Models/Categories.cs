
using System.ComponentModel.DataAnnotations;

namespace taskProduct.Models
{
    public class Categories
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a category name")] 
        [StringLength(100)]
        public string Name { get; set; } 

        public string? ImagePath { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(100)] //Range Validator
        public string Name { get; set; } = string.Empty;
        [Editable(false)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}

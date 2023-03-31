using System.ComponentModel.DataAnnotations;

namespace DunamicRoleAuth.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductType { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}

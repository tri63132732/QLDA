using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4BroShop.Models.EFModels
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        public string Icon { get; set; }

        public bool IsActive { get; set; }

        // Một Category chứa nhiều Product
        public ICollection<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4BroShop.Models.EFModels
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [AllowHtml]
        public string Detail { get; set; }

        public string Image { get; set; }

        //giá bán
        public decimal Price { get; set; }

        //public int ViewCount { get; set; }
        public bool IsHome { get; set; }
        public bool IsFeature { get; set; }
        public bool IsActive { get; set; }

        // Thêm trường tham chiếu đến Category
        [ForeignKey("ProductCategory")]
        public int CategoryId { get; set; }

        public virtual Category ProductCategory { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
namespace _4BroShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductImage")]
    public partial class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Image { get; set; }

        public bool IsDefault { get; set; }

        public virtual Product Product { get; set; }
    }
}

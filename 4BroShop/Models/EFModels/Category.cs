using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace _4BroShop.Models.EFModels
{
    [Table("Category")]
    public class Category : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên danh mục không để trống!")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; }
    }
}
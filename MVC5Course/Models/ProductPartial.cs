using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    [MetadataType(typeof(ProductPartial))]
    public partial class Product
    {
        
    }

    public class ProductPartial
    {
        [Required(ErrorMessage = "商品名稱必填")]
        [MinLength(3), MaxLength(30)]
        [RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤")]
        public string ProductName { get; set; }

        [Required]
        [Range(0, 9999, ErrorMessage = "請設定正確的商品價格範圍")]
        public Nullable<decimal> Price { get; set; }

        [Required]
        public Nullable<bool> Active { get; set; }

        [Required]
        public Nullable<decimal> Stock { get; set; }
    }
}
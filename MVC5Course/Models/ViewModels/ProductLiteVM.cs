using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    /// <summary>
    /// 精簡版的Product資料
    /// </summary>
    public class ProductLiteVM
    {
        public int ProductId { get; set; }

        [DisplayName("商品名稱")]
        [Required]
        [MinLength(5)]
        public string ProductName { get; set; }

        [DisplayName("商品價格")]
        [Required]
        public Nullable<decimal> Price { get; set; }

        [DisplayName("庫存")]
        [Required]
        public Nullable<decimal> Stock { get; set; }
    }
}
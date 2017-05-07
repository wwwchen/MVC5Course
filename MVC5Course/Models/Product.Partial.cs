using System.ComponentModel;

namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        [DisplayName("訂單數量")]
        public virtual int OrderCount //若沒延遲載入，會有問題(目前沒有是因為前面controller 的 all 是用 AsQueryable，這個有延遲載入)
        {
            get
            {
                return this.OrderLine.Count;
                //return this.OrderLine.Count;
                //return this.OrderLine.Where(p => p.Qty > 400).Count;
                //return this.OrderLine.Where(p => p.Qty > 400).ToList().Count;
                //return this.OrderLine.Count(p => p.Qty > 400);
            }
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [DisplayName("商品名稱")]
        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }

        [DisplayName("商品價格")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Price { get; set; }

        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }

        [DisplayName("商品庫存")]
        public Nullable<decimal> Stock { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}

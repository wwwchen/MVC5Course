using System.ComponentModel;
using MVC5Course.Models.ValidationAttributes;

namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price.HasValue && Price == 0)
            {
                yield return new ValidationResult("商品價格不能為 0"); //沒加參數，錯誤訊息就會顯示在最上面
            }

            if (Stock.HasValue && Stock == 0)
            {
                yield return new ValidationResult("商品庫存不能為 0", new string[] { "Stock" }); //後面的參數是指定錯誤訊息是顯示在那個label下
            }

            if (Stock.HasValue && Stock == 0 && Price.HasValue && Price == 0)
            {
                yield return new ValidationResult("商品價格與商品庫存不能為 1", new string[] { "Price", "Stock" }); //一個label只能有一個錯誤訊息
            }

            yield break;
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [DisplayName("商品名稱")]
        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        //[Include("Will")]
        //[MaxWordsAttribute(2)]
        //[MinLength(3), MaxLength(30)]
        //[RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤")]
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

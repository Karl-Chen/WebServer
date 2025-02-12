using System.ComponentModel.DataAnnotations;
using MyWebAPI.Models;

namespace MyWebAPI.ValidationAttributes
{
    public class MyValidator
    {
        //5.3.3 在ProductPostDTO.cs加入自訂驗證器(使用ValidationAttribute物件)，寫完變成一個標籤，可以放在上方Model的變數上，做成變數驗證條件
        public class ProductNameCheck : ValidationAttribute
        {
            //5.3.4 在需要使用此驗證器的屬性上加入標籤(這裡範例為ProductName屬性)
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                //為了修改而加的這個判斷，因為修改可以不改產品名稱
                if (value == null)
                    return ValidationResult.Success;
                //-------------------------------------------------------
                string productName = value.ToString();
                if (productName.Length < 3)
                    return new ValidationResult("商品名稱至少3個字");

                return ValidationResult.Success;
            }
        }
        public class CateNameCheck : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                GoodStoreContextCustom _context = validationContext.GetService<GoodStoreContextCustom>();
                var findName = _context.Category.Where(c => c.CateName == value.ToString());
                if (findName.Any())
                    return new ValidationResult("類別名稱重覆了");
                return ValidationResult.Success;
            }
        }
    }
}

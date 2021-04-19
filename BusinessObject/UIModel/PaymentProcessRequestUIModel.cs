using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessObject.UIModel
{
    public class PaymentProcessRequestUIModel
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^4[0-9]{12}(?:[0-9]{3})?$", ErrorMessage = "Credit card number is not valid.")]
        public string CreditCardNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string CardHolder { get; set; }

        [Required(AllowEmptyStrings = false)]
        public DateTime ExpirationDate { get; set; }

        [StringLength(3, ErrorMessage = "Only 3 digits are allwed.")]
        public string SecurityCode { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(0.1, int.MaxValue, ErrorMessage = "Amount value can not be less than 0.")]
        public double Amount { get; set; }
    }
}

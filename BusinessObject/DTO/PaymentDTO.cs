using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject.DTO
{
    public class PaymentDTO : BaseDTO
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public double Amount { get; set; }
        public bool IsCheapPayment
        {
            get
            {
                if (Amount <= 20)
                    return true;
                return false;
            }
        }
        public bool IsExpensivePayment
        {
            get
            {
                if (Amount > 20 && Amount <= 500)
                    return true;
                return false;
            }
        }
        public bool IsPremiumPayment
        {
            get
            {
                if (Amount > 500)
                    return true;
                return false;
            }
        }
        public string PaymentState { get; set; } = "Pending";
    }

    public class BaseDTO
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}

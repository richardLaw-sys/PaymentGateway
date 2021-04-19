using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class PaymentGateway : IPaymentGateway
    {
        public bool CheapPaymentService()
        {
            return true;
        }

        public bool ExpensivePaymentService()
        {
            return true;
        }

        public bool PremiumPaymentService()
        {
            return true;
        }
    }
}

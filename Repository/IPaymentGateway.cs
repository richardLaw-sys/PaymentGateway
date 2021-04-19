using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IPaymentGateway
    {
        bool ExpensivePaymentService();
        bool CheapPaymentService();
        bool PremiumPaymentService();
    }
}

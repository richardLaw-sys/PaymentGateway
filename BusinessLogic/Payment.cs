using BusinessObject.DTO;
using BusinessObject.UIModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Payment
    {
        public readonly PaymentDTO paymentDTO;
        private IPaymentGateway paymentGateway = null;

        public Payment(PaymentDTO paymentDTO, IPaymentGateway paymentGateway)
        {
            this.paymentDTO = paymentDTO;
            this.paymentGateway = paymentGateway;
        }

        public static async Task<Payment> SelectPaymentProcess(PaymentDTO paymentDTO, IPaymentGateway paymentGateway)
        {
            var retVal = new Payment(paymentDTO, paymentGateway);
            await retVal.PaymentProcess();
            return retVal;
        }

        public async Task PaymentProcess()
        {
            bool IsPaymentSuccess = false;

            if (paymentDTO.IsCheapPayment)
            {
                IsPaymentSuccess = paymentGateway.CheapPaymentService();
            }
            else if (paymentDTO.IsExpensivePayment)
            {
                IsPaymentSuccess = paymentGateway.ExpensivePaymentService(); // to check expensive payment service is available or not.
                if (!IsPaymentSuccess)
                    IsPaymentSuccess = paymentGateway.CheapPaymentService();
            }
            else if (paymentDTO.IsPremiumPayment)
            {
                IsPaymentSuccess = Retry.Do(paymentGateway.PremiumPaymentService, TimeSpan.FromSeconds(1));
            }

            if (IsPaymentSuccess)
                paymentDTO.PaymentState = "Processed";
            else
                paymentDTO.PaymentState = "Failed";

            paymentDTO.IsSuccess = true;
        }
    }
}

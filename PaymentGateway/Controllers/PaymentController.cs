using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using BusinessObject.DBModel;
using BusinessObject.DTO;
using BusinessObject.UIModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentGateway paymentGateway;
        private readonly IDataRepository<PaymentDBModel> _dataRepository;
        public PaymentController(IPaymentGateway paymentGateway, IDataRepository<PaymentDBModel> dataRepository)
        {
            this.paymentGateway = paymentGateway;
            this._dataRepository = dataRepository;
        }

        [Route("PaymentProcess")]
        [HttpPost]
        public async Task<IActionResult> PaymentProcess([FromBody]PaymentProcessRequestUIModel request)
        {
            var retVal = new PaymentProcessResponseUIModel();

            try
            {
                if (request.ExpirationDate.Date < DateTime.Now.Date)
                {
                    return BadRequest("Expiration date can not be past.");
                }
                else if (request.SecurityCode.Length != 3)
                {
                    return BadRequest("SecurityCode should be 3 characters.");
                }

                var paymentDTO = Mapper.Map<PaymentDTO>(request);
                var paymentProcess = await Payment.SelectPaymentProcess(paymentDTO, paymentGateway);

                if (paymentProcess.paymentDTO.IsSuccess)
                {
                    _dataRepository.Add(new PaymentDBModel
                    {
                        Amount = paymentProcess.paymentDTO.Amount,
                        CardHolder = paymentProcess.paymentDTO.CardHolder,
                        CCN = paymentProcess.paymentDTO.CreditCardNumber,
                        ExpirationDate = paymentProcess.paymentDTO.ExpirationDate,
                        PaymentState = paymentProcess.paymentDTO.PaymentState,
                        SecurityCode = paymentProcess.paymentDTO.SecurityCode
                    });
                }

                retVal = Mapper.Map<PaymentProcessResponseUIModel>(paymentProcess.paymentDTO);
            }
            catch (Exception ex)
            {
                retVal.IsSuccess = false;
                retVal.ErrorMessage = $"We have encountered an issue:- {ex.Message}";
                return StatusCode(StatusCodes.Status500InternalServerError, retVal);
            }

            return Ok(retVal);
        }
    }
}

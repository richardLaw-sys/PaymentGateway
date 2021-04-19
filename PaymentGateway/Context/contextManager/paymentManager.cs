using BusinessObject.DBModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentGateway.Context.contextManager
{
    public class PaymentManager : IDataRepository<PaymentDBModel>
    {
        readonly PaymentContext _paymentContext;
        public PaymentManager(PaymentContext context)
        {
            _paymentContext = context;
        }
        public IEnumerable<PaymentDBModel> GetAll()
        {
            return _paymentContext.Payment.ToList();
        }
        public PaymentDBModel Get(long id)
        {
            throw new NotImplementedException();
        }
        public void Add(PaymentDBModel entity)
        {
            _paymentContext.Payment.Add(entity);
            _paymentContext.SaveChanges();
        }

        public void Update(PaymentDBModel dbEntity, PaymentDBModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(PaymentDBModel entity)
        {
            throw new NotImplementedException();
        }
    }
}

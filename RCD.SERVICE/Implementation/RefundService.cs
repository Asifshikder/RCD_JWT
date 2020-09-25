using RCD.DATA.Entity;
using RCD.REPO;
using RCD.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Implementation
{
   public class RefundService : IRefundService
    {
        private readonly IRepository<Refund> RefundRepository;
        public RefundService(IRepository<Refund> RefundRepository)
        {
            this.RefundRepository = RefundRepository;
        }

        public void DeleteRefund(int id)
        {
            Refund Refund = RefundRepository.Get(id);
            RefundRepository.Remove(Refund);

        }

        public Refund GetRefund(int id)
        {
            return RefundRepository.Get(id);
        }

        public IEnumerable<Refund> GetRefunds()
        {
            return RefundRepository.GetAll();
        }

        public void InsertRefund(Refund Refund)
        {
            RefundRepository.Insert(Refund);
        }

        public void UpdateRefund(Refund Refund)
        {
            RefundRepository.Update(Refund);
        }
    }
}


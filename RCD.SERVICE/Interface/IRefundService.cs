using RCD.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Interface
{
    public interface IRefundService
    {
        IEnumerable<Refund> GetRefunds();
        Refund GetRefund(int id);
        void InsertRefund(Refund Refund);
        void UpdateRefund(Refund Refund);
        void DeleteRefund(int id);
    }
}

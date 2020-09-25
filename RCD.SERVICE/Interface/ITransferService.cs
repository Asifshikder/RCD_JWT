using RCD.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Interface
{
    public interface ITransferService
    {
        IEnumerable<Transfer> GetTransfers();
        Transfer GetTransfer(int id);
        void InsertTransfer(Transfer Transfer);
        void UpdateTransfer(Transfer Transfer);
        void DeleteTransfer(int id);
    }
}

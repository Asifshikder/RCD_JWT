using RCD.DATA.Entity;
using RCD.REPO;
using RCD.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Implementation
{
   public class TransferService : ITransferService
    {
        private readonly IRepository<Transfer> TransferRepository;
        public TransferService(IRepository<Transfer> TransferRepository)
        {
            this.TransferRepository = TransferRepository;
        }

        public void DeleteTransfer(int id)
        {
            Transfer Transfer = TransferRepository.Get(id);
            TransferRepository.Remove(Transfer);

        }

        public Transfer GetTransfer(int id)
        {
            return TransferRepository.Get(id);
        }

        public IEnumerable<Transfer> GetTransfers()
        {
            return TransferRepository.GetAll();
        }

        public void InsertTransfer(Transfer Transfer)
        {
            TransferRepository.Insert(Transfer);
        }

        public void UpdateTransfer(Transfer Transfer)
        {
            TransferRepository.Update(Transfer);
        }
    }
}


using RCD.DATA.Entity;
using RCD.REPO;
using RCD.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Implementation
{
   public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> TransactionRepository;
        public TransactionService(IRepository<Transaction> TransactionRepository)
        {
            this.TransactionRepository = TransactionRepository;
        }

        public void DeleteTransaction(int id)
        {
            Transaction Transaction = TransactionRepository.Get(id);
            TransactionRepository.Remove(Transaction);

        }

        public Transaction GetTransaction(int id)
        {
            return TransactionRepository.Get(id);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return TransactionRepository.GetAll();
        }

        public void InsertTransaction(Transaction Transaction)
        {
            TransactionRepository.Insert(Transaction);
        }

        public void UpdateTransaction(Transaction Transaction)
        {
            TransactionRepository.Update(Transaction);
        }
    }
}


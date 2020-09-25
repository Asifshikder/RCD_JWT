using RCD.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Interface
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactions();
        Transaction GetTransaction(int id);
        void InsertTransaction(Transaction Transaction);
        void UpdateTransaction(Transaction Transaction);
        void DeleteTransaction(int id);
    }
}

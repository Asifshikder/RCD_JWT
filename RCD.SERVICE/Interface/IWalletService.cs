using RCD.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Interface
{
    public interface IWalletService
    {
        IEnumerable<Wallet> GetWallets();
        Wallet GetWallet(int id);
        void InsertWallet(Wallet Wallet);
        void UpdateWallet(Wallet Wallet);
        void DeleteWallet(int id);
    }
}

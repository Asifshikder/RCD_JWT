using RCD.DATA.Entity;
using RCD.REPO;
using RCD.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Implementation
{
   public class WalletService : IWalletService
    {
        private readonly IRepository<Wallet> WalletRepository;
        public WalletService(IRepository<Wallet> WalletRepository)
        {
            this.WalletRepository = WalletRepository;
        }

        public void DeleteWallet(int id)
        {
            Wallet Wallet = WalletRepository.Get(id);
            WalletRepository.Remove(Wallet);

        }

        public Wallet GetWallet(int id)
        {
            return WalletRepository.Get(id);
        }

        public IEnumerable<Wallet> GetWallets()
        {
            return WalletRepository.GetAll();
        }

        public void InsertWallet(Wallet Wallet)
        {
            WalletRepository.Insert(Wallet);
        }

        public void UpdateWallet(Wallet Wallet)
        {
            WalletRepository.Update(Wallet);
        }
    }
}


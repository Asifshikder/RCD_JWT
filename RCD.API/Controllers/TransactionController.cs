using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCD.DATA.Entity;
using RCD.DATA.Models;
using RCD.SERVICE.Interface;

namespace RCD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private IWalletService walletService;
        private ITransactionService transactionService;
        private UserManager<ApplicationUser> userManager;
        private ITransferService transferService;
        private IReebuxService reebuxService;
        private IRefundService refundService;
        private IProductService productService;

        public TransactionController(IWalletService walletService,
            ITransactionService transactionService,
            UserManager<ApplicationUser> userManager,
            ITransferService transferService,
            IReebuxService reebuxService,
            IRefundService refundService,
            IProductService productService)
        {
            this.walletService = walletService;
            this.transactionService = transactionService;
            this.userManager = userManager;
            this.transferService = transferService;
            this.reebuxService = reebuxService;
            this.refundService = refundService;
            this.productService = productService;
        }

        [HttpPost("Transfer")]
        public IActionResult TransferBalance(TransferVM transfer)
        {
            var checkwallet = walletService.GetWallets().Where(s => s.Address == transfer.RecieverWallet).FirstOrDefault();
            if (checkwallet != null)
            {
                var senderwalletAddress = transfer.SenderWallet;
                var recieverwalletAddress = transfer.RecieverWallet;

                var senderID = userManager.GetUserAsync(HttpContext.User).Result.Id;
                var rcvrID = walletService.GetWallets().Where(s => s.Address == recieverwalletAddress).FirstOrDefault().UserID;

                var swlt = walletService.GetWallets().Where(s => s.Address == senderwalletAddress).FirstOrDefault();
                var rwlt = walletService.GetWallets().Where(s => s.Address == recieverwalletAddress).FirstOrDefault();

                swlt.Balance = swlt.Balance - transfer.Amount;
                walletService.UpdateWallet(swlt);
                rwlt.Balance = rwlt.Balance + transfer.Amount;
                walletService.UpdateWallet(rwlt);

                Transfer trs = new Transfer();
                trs.AddDate = DateTime.Now;
                trs.Amount = transfer.Amount;
                trs.SenderID = senderID;
                trs.RecieverID = rcvrID;
                trs.SenderWalletAddress = senderwalletAddress;
                trs.RecieverWalletAddress = recieverwalletAddress;
                trs.Status = 1;
                transferService.InsertTransfer(trs);
                return Ok(new TransactionResponse { Message = "Successfully send to" + recieverwalletAddress + "", IsSuccess = true });
            }
            else
            {
                return Ok(new TransactionResponse { Message = "Transfer failed", IsSuccess = false });
            }

        }


        [HttpPost("Purchase")]
        public IActionResult PurchaseReebux(TransactionVM transaction)
        {
            try
            {
                var user = userManager.GetUserAsync(HttpContext.User).Result;
                var userwallet = walletService.GetWallets().Where(s => s.UserID == user.Id).FirstOrDefault();
                var product = reebuxService.GetReebux(transaction.ReebuxID);

                Transaction tr = new Transaction();
                tr.Amount = transaction.Amount;
                tr.Details = transaction.Details;
                tr.IsValid = true;
                tr.Method = transaction.Method;
                tr.UserID = user.Id;
                tr.WalletID = userwallet.Id;
                tr.AddDate = DateTime.Now;
                tr.Status = 1;
                transactionService.InsertTransaction(tr);

                userwallet.Balance += product.Balance;
                walletService.UpdateWallet(userwallet);
                return Ok(new TransactionResponse { Message = "Successfully deposited", IsSuccess = true });
            }
            catch (Exception ex)
            {
                return Ok(new TransactionResponse { Message = "Failed to deposited", IsSuccess = false });
            }


        }
        [HttpPost("Refund")]
        public IActionResult RefundReebux(RefundVM refund)
        {
            try
            {
                var user = userManager.GetUserAsync(HttpContext.User).Result;
                var userwallet = walletService.GetWallets().Where(s => s.UserID == user.Id).FirstOrDefault();
                var product = reebuxService.GetReebux(refund.ReebuxID.Value);

                Refund rf = new Refund();
                rf.Amount = product.Amount;
                rf.Description = refund.Description;
                rf.UserID = user.Id;
                rf.WalletID = userwallet.Id;
                rf.AddDate = DateTime.Now;
                rf.Status = 1;
                rf.IsGranted = true;
                rf.ReebuxID = refund.ReebuxID;
                userwallet.Balance = product.Balance;

                refundService.InsertRefund(rf);
                userwallet.Balance -= rf.Amount;
                walletService.UpdateWallet(userwallet);
                return Ok(new TransactionResponse { Message = "Successfully recieved, We'll verify your request. We will notify you.", IsSuccess = true });
            }
            catch (Exception ex)
            {
                return Ok(new TransactionResponse { Message = "Something went wrong", IsSuccess = false });
            }


        }

        [HttpPost("ProductBuy")]
        public IActionResult BuyProduct(BuyProductVM buy)
        {
            try
            {
                var user = userManager.GetUserAsync(HttpContext.User).Result;
                var wallet = walletService.GetWallets().Where(s => s.UserID == user.Id).FirstOrDefault();
                var prod = productService.GetProduct(buy.ProductID);
                var price = prod.Price * buy.Amount;
                if (wallet.Balance < price)
                {
                    return Ok(new TransactionResponse { Message = "Infficient Balance", IsSuccess = false });
                }
                else
                {
                    wallet.Balance -= price;
                    walletService.UpdateWallet(wallet);
                    Transaction tr = new Transaction();
                    tr.AddDate = DateTime.Now;
                    tr.Amount = price;
                    tr.Details = "Buying product";
                    tr.IsValid = true;
                    tr.Method = "Reebux Payment";
                    tr.UserID = user.Id;
                    tr.WalletID = wallet.Id;
                    tr.Status = 1;
                    transactionService.InsertTransaction(tr);
                    return Ok(new TransactionResponse { Message = "Success", IsSuccess = true });
                }
            }
            catch (Exception ex)
            {
                return Ok(new TransactionResponse { Message = "Failed", IsSuccess = false });
            }

        }

    }
}

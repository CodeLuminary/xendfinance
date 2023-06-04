using Xend_Finance.Interface.Repository;
using System.Linq;
using Xend_Finance.Entities;
using Xend_Finance.Models;

namespace Xend_Finance.Repository
{
    public class TransactionRepository: ITransactionRepository
    {
        private readonly DataContext _dbContext;
        public TransactionRepository(DataContext context)
        {
            _dbContext = context;
        }
        public Dictionary<string, TransactionsModel> GetTransactionsInDictionaryFormat(TransactionQueryModel model)
        {
            return (from c in _dbContext.Set<Transactions>()
                                where String.Compare(c.WalletAddress, model.WalletAddress) == 0  && String.Compare(c.Currency, model.Currency)==0
                                select new TransactionsModel
                                {
                                    TransactionId = c.TransactionId,
                                    TransactionRef = c.TransactionRef,
                                    ClientId = c.ClientId,
                                    WalletAddress = c.WalletAddress,
                                    Amount = c.Amount,
                                    CreatedDate = c.CreatedDate
                                }).ToDictionary(x => x.TransactionRef);
        }

        public List<TransactionsModel> GetTransactionsInListFormat(TransactionQueryModel model)
        {
            return (from c in _dbContext.Set<Transactions>()
                    where String.Compare(c.WalletAddress, model.WalletAddress) == 0 && String.Compare(c.Currency, model.Currency) == 0
                    select new TransactionsModel
                    {
                        TransactionId = c.TransactionId,
                        TransactionRef = c.TransactionRef,
                        ClientId = c.ClientId,
                        WalletAddress = c.WalletAddress,
                        Amount = c.Amount,
                        CreatedDate = c.CreatedDate
                    }).ToList();
        }

        public bool AddClientTransaction(TransactionsModel model)
        {
            _dbContext.Set<Transactions>().Add(new Transactions
            {
                TransactionId = model.TransactionId,
                TransactionRef = model.TransactionRef,
                ClientId = model.ClientId,
                WalletAddress = model.WalletAddress,
                Currency = model.Currency,
                Amount = model.Amount,
                CreatedDate = model.CreatedDate
            });

            return _dbContext.SaveChanges() > 0;
        }
    }
}

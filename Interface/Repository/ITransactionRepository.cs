using Xend_Finance.Models;

namespace Xend_Finance.Interface.Repository
{
    public interface ITransactionRepository
    {
        Dictionary<string, TransactionsModel> GetTransactionsInDictionaryFormat(TransactionQueryModel model);
        List<TransactionsModel> GetTransactionsInListFormat(TransactionQueryModel model);
        bool AddClientTransaction(TransactionsModel model);
    }
}

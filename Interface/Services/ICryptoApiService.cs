using Xend_Finance.Models;

namespace Xend_Finance.Interface.Repository
{
    public interface ICryptoApiService
    {
        Task<List<TransactionsModel>> GetClientTransactions(TransactionQueryModel model);
    }
}

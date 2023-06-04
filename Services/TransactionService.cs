
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using Xend_Finance.Interface;
using Xend_Finance.Interface.Repository;
using Xend_Finance.Models;

namespace Xend_Finance.Services
{
    public class TransactionService : IConsumer<TransactionQueryModel>
    {
        private readonly ICryptoApiService _cryptoApiService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IEventBus _eventBus;

        public TransactionService(ICryptoApiService cryptoApiService, ITransactionRepository transactionRepository, IEventBus eventBus)
        {
            _cryptoApiService = cryptoApiService;
            _transactionRepository = transactionRepository;
            _eventBus = eventBus;
        }

        public async Task Consume(ConsumeContext<TransactionQueryModel> context)
        {
            var data = context.Message;
            var allWalletTransactions = await _cryptoApiService.GetClientTransactions(data);
            try
            {
                var allSavedTransactions = _transactionRepository.GetTransactionsInDictionaryFormat(data);
                var transactionCount = allWalletTransactions.Count();

                for (var i = 0; i < transactionCount; i++)
                {

                    if (!allSavedTransactions.ContainsKey(allWalletTransactions[i].TransactionRef))
                    {
                        _transactionRepository.AddClientTransaction(allWalletTransactions[i]);
                        _eventBus.publish(allWalletTransactions[i]);
                    }
                }
            }
            catch(Exception) { }
        }
    }
}

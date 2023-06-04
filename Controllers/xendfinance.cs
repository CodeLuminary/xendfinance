using Microsoft.AspNetCore.Mvc;
using Xend_Finance.Interface.Repository;
using Xend_Finance.Models;

namespace Xend_Finance.Controllers
{
    [ApiController]
    [Route("api/v1/transactions")]
    public class xendfinance : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        public xendfinance(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost("get-client-transactions")]
        public IActionResult GetClientTransactions(TransactionQueryModel model)
        {
            return Ok(_transactionRepository.GetTransactionsInListFormat(model));
        }
    }
}
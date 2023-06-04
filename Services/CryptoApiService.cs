using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Xend_Finance.Interface.Repository;
using Xend_Finance.Models;

namespace Xend_Finance.Services
{
    public class CryptoApiService : ICryptoApiService
    {
        private readonly IConfiguration _configuration;
        public CryptoApiService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public async Task<List<TransactionsModel>> GetClientTransactions(TransactionQueryModel model)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                try
                {
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    var result = await _httpClient.PostAsync(_configuration["CryptoApi:Url"].ToString(), httpContent);

                    var resultContent = await result.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<TransactionsModel>>(resultContent);
                }
                catch(Exception e)
                {
                    return new List<TransactionsModel>();
                }
            }
        }
    }
}

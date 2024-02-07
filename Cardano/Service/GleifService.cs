using Cardano.Models;
using System.Net.Http.Json;

namespace Cardano.Service
{
    internal class GleifService
    {
        private readonly string _url;
        private readonly HttpClient _httpClient;
        private readonly string _pageQueryParam = "&page[number]=";

        public GleifService(string url)
        {
            _url = url;
            _httpClient = new HttpClient();
        }

        public async Task<List<GleifResponseLeiRecord>> GetLeiRecords(IEnumerable<Transaction> transactions)
        {
            var distinctLeis = transactions.Select(x => x.Lei).Distinct();
            var leiString = string.Join(",", distinctLeis);
            GleifResponse response;
            var records = new List<GleifResponseLeiRecord>();
            try
            {
                int pageIndex = 1;
                do
                {
                    response = await _httpClient.GetFromJsonAsync<GleifResponse>(_url + leiString + _pageQueryParam + pageIndex++);
                    records.AddRange(response.data);
                } while (response.meta.pagination.currentPage < response.meta.pagination.lastPage);

            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error fetching LEI data", ex.Message);
                throw;
            }            
            
            return records;
        }
    }
}

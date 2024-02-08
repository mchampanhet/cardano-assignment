using Cardano.Models;
using System.Net.Http.Json;

namespace Cardano.Services
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
            // multiple transactions can have the same LEI, so let's remove duplicates
            var distinctLeis = transactions.Select(x => x.Lei).Distinct();
            var leiString = string.Join(",", distinctLeis);
            var records = new List<GleifResponseLeiRecord>();

            try
            {
                GleifResponse response;
                int pageIndex = 1;

                // fetch all pages for the given list of LEIs and combine them into one list to return
                do
                {
                    response = await _httpClient.GetFromJsonAsync<GleifResponse>(_url + leiString + _pageQueryParam + pageIndex++);
                    records.AddRange(response.data);
                } while (response.meta.pagination.currentPage < response.meta.pagination.lastPage);

            }
            catch (Exception ex)
            {
               LoggerService.LogError("There was an error fetching LEI data", ex);
               throw;
            }            
            
            return records;
        }
    }
}

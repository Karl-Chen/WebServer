using MyWebAPI.APIModels;
using Newtonsoft.Json;

namespace MyWebAPI.Services
{
    public class PetAdoptionService
    {
        private readonly HttpClient _httpClient;
        
        public PetAdoptionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PetAdoptionData>> GetAllData(int page = 1, int count = 50)
        {
            return await GetData(page, count);
        }

        public async Task<IEnumerable<PetAdoptionData>> GetData(int page = 1, int count = 50, string keyparam = "", string valueparam = "")
        {
            int skip = getItemSkip(page, count);
            string url = "https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL";
            url = GetHttpUrl(url, "$top", count.ToString());
            url = GetHttpUrl(url, "$skip", skip.ToString());
            url = GetHttpUrl(url, keyparam, valueparam);
            
            return await GetJsonResult(url);

        }


        private async Task<IEnumerable<PetAdoptionData>> GetJsonResult(string url)
        {
            string response = await _httpClient.GetStringAsync(url);
            IEnumerable<PetAdoptionData> collection = JsonConvert.DeserializeObject<IEnumerable<PetAdoptionData>>(response);

            return collection;
        }

        private string GetHttpUrl(string baseUrl, string keyFilter, string valueFilter)
        {
            if (keyFilter == "")
                return baseUrl;
            string url = baseUrl + "&" + keyFilter + "=" + valueFilter;
            return url;
        }

        private int getItemSkip(int page, int count)
        {
            return (page - 1) * count;
        }
    }
}

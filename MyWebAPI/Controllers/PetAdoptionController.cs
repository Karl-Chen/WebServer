using System.Collections.Generic;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.APIModels;
using MyWebAPI.Services;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace MyWebAPI.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class PetAdoptionController : ControllerBase
    {
        //private readonly HttpClient _httpClient;
        //public PetAdoptionController(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        private readonly PetAdoptionService _petAdoptionService;
        public PetAdoptionController(PetAdoptionService petAdoptionService)
        {
            _petAdoptionService = petAdoptionService;
        }

        [HttpGet]
        //IEnumerable 可被列舉的資料，相當於list
        public async Task<IEnumerable<PetAdoptionData>> GetAllData(int page = 1, int count = 50)
        {
            //每頁50筆
            //9.1.8 在Get()方法中加入分頁用參數
            //string skipcount = getItemSkip(page, count).ToString();
            //string url = "https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top=50&$skip=" + skipcount;
            //HttpClient client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync(url);
            //string response = await _httpClient.GetStringAsync(url);
            //IEnumerable<PetAdoptionData> collection = JsonConvert.DeserializeObject<IEnumerable<PetAdoptionData>>(response);

            //以上沒解偶，以下解偶
            var collection = _petAdoptionService.GetAllData(page, count);

            if (collection == null)
                return null;

            return await collection;

        }

        [HttpGet("GetByArea")]
        //IEnumerable 可被列舉的資料，相當於list
        public async Task<IEnumerable<PetAdoptionData>> GetByArea(int Area, int page = 1, int count = 50)
        {
            ////每頁50筆
            ////9.1.8 在Get()方法中加入分頁用參數
            //string skipcount = getItemSkip(page, count).ToString();
            //string url = $"https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top={count}&$skip={skipcount}&animal_area_pkid={Area}";

            ////HttpClient client = new HttpClient();
            ////HttpResponseMessage response = await client.GetAsync(url);
            //string response = await _httpClient.GetStringAsync(url);
            //IEnumerable<PetAdoptionData> collection = JsonConvert.DeserializeObject<IEnumerable<PetAdoptionData>>(response);

            var collection = _petAdoptionService.GetData(page, count, "animal_area_pkid", Area.ToString());
            if (collection == null)
                return null;

            return await collection;

        }

        [HttpGet("GetByBacterin")]
        //IEnumerable 可被列舉的資料，相當於list
        public async Task<IEnumerable<PetAdoptionData>> GetByBacterin(string animal_bacterin, int page = 1, int count = 50)
        {
            ////每頁50筆
            ////9.1.8 在Get()方法中加入分頁用參數
            //string skipcount = getItemSkip(page, count).ToString();
            //string url = $"https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top={count}&$skip={skipcount}&animal_bacterin={animal_bacterin}";
            //Console.WriteLine(url);
            ////HttpClient client = new HttpClient();
            ////HttpResponseMessage response = await client.GetAsync(url);
            //string response = await _httpClient.GetStringAsync(url);
            //IEnumerable<PetAdoptionData> collection = JsonConvert.DeserializeObject<IEnumerable<PetAdoptionData>>(response);

            var collection = _petAdoptionService.GetData(page, count, "animal_bacterin", animal_bacterin);
            if (collection == null)
                return null;

            return await collection;

        }

        private int getItemSkip(int page, int count)
        {
            return (page - 1) * count;
        }
    }
}

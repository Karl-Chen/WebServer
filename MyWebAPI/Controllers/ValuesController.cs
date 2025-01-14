using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebAPI.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        //IEnumerable回傳值是個集合/陣列 
        //Index
        //Url只能走get，不能走Post/Put/Delete
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("aaa")]
        public IEnumerable<string> Get2()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        //detail
        public string Get(int id)
        {
            //return "value";
            string[] myProducts = { "超級無敵海景佛跳牆", "清香白玉板紅嘴綠鸚鴿", "玉笛誰家聽落梅" };
            return myProducts[id % 3];
        }

        // POST api/<ValuesController>
        //[FromBody]前端送資料過來
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("ppp")]
        public void Delete(int id)
        {
        }
    }
}

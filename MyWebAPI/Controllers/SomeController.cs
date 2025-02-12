using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        //8.1.5 在SomeController裡注入SomeService服務(這裡就是DI的寫法，不使用 new 關鍵字)
        readonly SomeService _service;
        public SomeController(SomeService service)
        {
            _service = service;
        }

        //8.1.6 撰寫兩個Get Action
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _service.GetStudents();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            var students = _service.GetStudent(id);
            if (students == "")
                return NotFound();
            return students;
        }
    }
}

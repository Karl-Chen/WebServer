using MyModel_DBFirst.Models;

namespace MyModel_DBFirst.ViewModels
{
    public class VMtStudent
    {
        public List<tStudent>? Students { get; set; }
        public List<Department>? Departments { get; set; }
    }
}

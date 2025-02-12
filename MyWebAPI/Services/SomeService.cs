using System.Runtime.CompilerServices;

namespace MyWebAPI.Services
{
    public class SomeService
    {
        string[] Id = { "A01", "A02", "A03"};
        string[] Name = { "王小明", "王中明", "王大明"};
        int[] Age = { 39, 20, 33 };

        public string GetStudent(string id)
        {
            int index = Array.IndexOf(Id, id);
            string ret = $"{Id[index] + "-" + Name[index] + "-" + Age[index]}";
            return ret;
        
        }
        public string[] GetStudents()
        {
            string[] strings = new string[Id.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = $"{ Id[i] + "-" + Name[i] + "-" + Age[i]}";
            }
            return strings;
        }
    }
}

using System.Reflection;

namespace MyController.Models
{
    // 1. 撰寫資料模型(Model)
    public class Member
    {
        // class成員宣告時如果值可以為空的話，可以寫成public string? MemberID { get; set; }
        public string MemberID { get; set; } = string.Empty;
        public string MemberName { get; set; } = null!;
        public string? MemberAddres { get; set; }
        public bool Gender { get; set; }
        public string MemberPhone { get; set; } = string.Empty;
    }
}

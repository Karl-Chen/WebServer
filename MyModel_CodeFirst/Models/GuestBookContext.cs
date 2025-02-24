﻿using Microsoft.EntityFrameworkCore;

namespace MyModel_CodeFirst.Models
{
    //1.2.1 在Models資料夾上按右鍵→加入→類別，檔名取名為GuestBookContext.cs，按下「新增」鈕
    //1.2.2 撰寫GuestBookContext類別的內容
    //      (1)須繼承DbContext類別
    //      (2)撰寫依賴注入用的建構子
    //      (3)描述資料庫裡面的資料表
    public class GuestBookContext : DbContext
    {
        public GuestBookContext(DbContextOptions<GuestBookContext> options) : base(options)
        {
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Rebook> Rebook { get; set; }
        public virtual DbSet<Login> Login { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace MyModel_CodeFirst.Models
{
    //1.3.3 撰寫SeedData類別的內容
    //      (1)撰寫靜態方法 Initialize(IServiceProvider serviceProvider)
    //      (2)撰寫Book及ReBook資料表內的初始資料程式
    //      (3)撰寫getFileBytes，功能為將照片轉成二進位資料
    public class SeedData
    {
        //(1)撰寫靜態方法 Initialize(IServiceProvider serviceProvider)
       
        //private readonly GuestBookContext _context;
        //public SeedData(GuestBookContext context)
        //{
        //    _context = context;
        //}
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (GuestBookContext _context = new GuestBookContext(serviceProvider.GetRequiredService<DbContextOptions<GuestBookContext>>()))
            {
                //如果Book沒東西(Book.Any()是false)才加資料進去
                if (!_context.Book.Any())
                {

                    //      (2)撰寫Book及ReBook資料表內的初始資料程式
                    List<string> myGuidList = new List<string>();
                    for (int i = 0; i < 5; i++)
                    {
                        myGuidList.Add(Guid.NewGuid().ToString());
                    }
                    _context.Book.AddRange(
                        new Book
                        {
                            BookID = myGuidList[0],
                            Title = "櫻桃鴨",
                            Description = "感覺好像很好吃耶！！！！",
                            Author = "Jack",
                            Photo = myGuidList[0] + ".jpg",
                            PhotoType = "image/jpeg",//圖片檔案類型標準表達法
                            TimeStmp = DateTime.Now
                        },
                        new Book
                        {
                            BookID = myGuidList[1],
                            Title = "鴨油高麗菜",
                            Description = "好像有點油！！！！",
                            Author = "Mary",
                            Photo = myGuidList[1] + ".jpg",
                            PhotoType = "image/jpeg",//圖片檔案類型標準表達法
                            TimeStmp = DateTime.Now
                        },
                        new Book
                        {
                            BookID = myGuidList[2],
                            Title = "薑母鴨",
                            Description = "超暖耶！！！！",
                            Author = "Jckason",
                            Photo = myGuidList[2] + ".jpg",
                            PhotoType = "image/jpeg",//圖片檔案類型標準表達法
                            TimeStmp = DateTime.Now
                        },
                        new Book
                        {
                            BookID = myGuidList[3],
                            Title = "脆皮鴨",
                            Description = "香香脆脆又有水果味！！！！",
                            Author = "Jack",
                            Photo = myGuidList[3] + ".jpg",
                            PhotoType = "image/jpeg",//圖片檔案類型標準表達法
                            TimeStmp = DateTime.Now
                        },
                        new Book
                        {
                            BookID = myGuidList[4],
                            Title = "三杯鴨",
                            Description = "問就是三杯！！！！",
                            Author = "Peter",
                            Photo = myGuidList[4] + ".jpg",
                            PhotoType = "image/jpeg",//圖片檔案類型標準表達法
                            TimeStmp = DateTime.Now
                        });

                    _context.SaveChanges();

                    List<string> myRebookGuidList = new List<string>();
                    for (int i = 0; i < 10; i++)
                    {
                        myRebookGuidList.Add(Guid.NewGuid().ToString());
                    }
                    _context.Rebook.AddRange(
                        new Rebook
                        {
                            ReBookID = myRebookGuidList[0],
                            Description = "你確定？這道超雷耶",
                            Author = "Mike",
                            TimeStmp = DateTime.Now,
                            BookID = myGuidList[0]
                        },
                        new Rebook
                        {
                            ReBookID = myRebookGuidList[1],
                            Description = "樓下別亂說，這道菜超讚的，我都配三碗白飯了",
                            Author = "Su",
                            TimeStmp = DateTime.Now,
                            BookID = myGuidList[0]
                        },
                        new Rebook
                        {
                            ReBookID = myRebookGuidList[2],
                            Description = "樓上愛吃噴~",
                            Author = "Mike",
                            TimeStmp = DateTime.Now,
                            BookID = myGuidList[0]
                        },
                        new Rebook
                        {
                            ReBookID = myRebookGuidList[3],
                            Description = "你的舌頭壞掉了吧",
                            Author = "Mark",
                            TimeStmp = DateTime.Now,
                            BookID = myGuidList[1]
                        },
                        new Rebook
                        {
                            ReBookID = myRebookGuidList[4],
                            Description = "冬天吃這個超爽的啦~",
                            Author = "Ammy",
                            TimeStmp = DateTime.Now,
                            BookID = myGuidList[2]
                        },
                        new Rebook
                        {
                            ReBookID = myRebookGuidList[5],
                            Description = "什麼脆脆，根本超硬好嗎！！",
                            Author = "Frank",
                            TimeStmp = DateTime.Now,
                            BookID = myGuidList[3]
                        },
                        new Rebook
                        {
                            ReBookID = myRebookGuidList[6],
                            Description = "唯一支持三杯的啦",
                            Author = "Nike",
                            TimeStmp = DateTime.Now,
                            BookID = myGuidList[4]
                        },
                        new Rebook
                        {
                            ReBookID = myRebookGuidList[7],
                            Description = "人生三杯，幾何三杯，唯有三杯，酒駕成三杯",
                            Author = "Boss",
                            TimeStmp = DateTime.Now,
                            BookID = myGuidList[4]
                        },
                        new Rebook
                        {
                            ReBookID = myRebookGuidList[8],
                            Description = "不夠油我還不想吃咧",
                            Author = "Arkya",
                            TimeStmp = DateTime.Now,
                            BookID = myGuidList[1]
                        },
                        new Rebook
                        {
                            ReBookID = myRebookGuidList[9],
                            Description = "吃完開車上路就有理由了",
                            Author = "Mike",
                            TimeStmp = DateTime.Now,
                            BookID = myGuidList[2]
                        }
                        );

                    _context.SaveChanges();

                    //撰寫上傳圖片的程式
                    string SeedPhotosPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedPhotos");            //取得來源照片的路徑
                    string BookPhotosPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BookPhotos"); //取得目的路徑

                    string[] files = Directory.GetFiles(SeedPhotosPath);        //取得指定路徑中的所有檔案
                    for (int i = 0; i < files.Length; i++)
                    {
                        string destFile = Path.Combine(BookPhotosPath, myGuidList[i] + ".jpg");
                        File.Copy(files[i], destFile);
                    }
                }
            }

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CodeFirstModelExam.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using ExamBookContext _content = new ExamBookContext(serviceProvider.GetRequiredService<DbContextOptions<ExamBookContext>>());
            if (!_content.Books.Any()) {
                List<string> bookid = new List<string>();
                string[] Title = ["MGEX攻擊自由", "MGSD自由", "MGSD獵魔", "MGSD飛翼", "PG薩克II", "PG能天使", "PG攻擊", "PG空中霸者", "PG攻擊自由", "PG紅異端改"];
                string[] Description = ["最新最刷最強最華麗的攻擊自由耶，三種不同的金色電鍍來做到色差分色，無愧於最華麗的攻擊自由",
                    "以MG的骨架技術展現在SD的Size上，擁有迷人的身姿及高度可動展現出自由鋼彈的帥氣",
                    "以MG的骨架技術展現在SD的Size上，擁有迷人的身姿及高度可動展現出獵魔鋼彈帥氣骨架露出",
                    "以MG的骨架技術展現在SD的Size上，展露出俗稱飛翼掉毛的帥氣天使",
                    "以1：60比例呈現將夏亞專用薩克完美呈現",
                    "能天使以1：60比例呈現出來，並且可以裝上燈組做為發光的賣點",
                    "以1：60比例呈現將攻擊鋼彈完美呈現，以超高可動及外甲展開做為賣點",
                    "以1：60比例將攻擊鋼彈的翔翼背包搭載在空中霸者上，並且可以將翔翼背包與PG攻擊鋼彈相結合",
                    "以1：60比例將攻擊自由的金色骨架及超大超華麗翅膀呈現在玩家",
                    "以1：60比例將紅異端改的肌肉特色造型呈現，其中更以雙武士刀及戰術大劍做為賣點"];
                string[] Author = ["基拉大和", "基拉大和", "三日月", "希洛唯", "夏亞", "剎那F塞因", "基拉大和", "穆福拉卡", "基拉大和", "羅"];
                DateTime[] dateTimes = [new DateTime(2024, 12, 20, 13, 20, 50, 600),
                new DateTime(2025, 1, 1, 15, 26, 51, 120),
                new DateTime(2025, 1, 1, 17, 23, 29, 321),
                new DateTime(2025, 1, 2, 11, 12, 15, 756),
                new DateTime(2025, 1, 3, 3, 56, 8, 675),
                new DateTime(2025, 1, 4, 0, 37, 55, 220),
                new DateTime(2025, 1, 5, 1, 48, 44, 123),
                new DateTime(2025, 1, 6, 8, 25, 33, 369),
                new DateTime(2025, 1, 7, 9, 12, 22, 111),
                new DateTime(2025, 1, 8, 19, 11, 11, 22)];
                
                for (int i = 0; i < 10; i++) {
                    Book book = new Book();
                    bookid.Add(Guid.NewGuid().ToString());
                    book.BookID = bookid[i];
                    book.Title = Title[i];
                    book.Description = Description[i];
                    book.Author = Author[i];
                    book.TimeStmp = dateTimes[i];
                    book.Photo = bookid[i] + ".jpg";
                    book.PhotoType = "image/jpeg";
                    _content.Books.Add(book);
                }
                _content.SaveChanges();

                List<string> RebookID = new List<string>();
                String[] RebookDisciption = ["最帥最強當之無愧!!哪裡可以買得到？",      //22筆
                    "最帥最強？你在開什麼玩笑，在大學長面前施主你還是自盡吧",
                    "阿姆羅大學長沒來啊",    //1
                    "超讚耶，比SDSC還要來得精緻，現在都搶不到貨了",     //2
                    "啊最後還不是被牙籤插爆？",     
                    "樓上很嗆喔~",                   
                    "三明~不要停下來啊~",                             //3
                    "人家吃雞腿，我吃雞翅，兩隻大雞翅還會掉毛呢~",       //4
                    "哼哼~紅色有角三倍速吶~",
                    "夏亞你算計我!!",
                    "紅色有角三倍速又怎樣，還不是被大學長慘電!!",
                    "樓上你是不是沒被阿克西斯砸過？",
                    "來啊，怕你喔",                                   //5
                    "這哪來的瘋子？",
                    "我就是鋼彈!!",
                    "神經病",                              //6
                    "又是來騙錢的，直接空霸跟攻擊一起賣就好了，而且只有給翔裝備，還沒給其它的",     //8
                    "跟MGEX比真的超遜，而且還比較貴",
                    "樓上我還真的無法反駁，重點是MGEX買不到啊",
                    "阿斯蘭~~~~~~",                //9
                    "吃我雙刀啦~",
                    "樓上你的裝備多到跟攻擊一樣多了，有點太扯了喔"                //10
                ];
                string[] reAuthor = ["基拉大和", "克魯澤", "基拉大和", "真飛鳥", "夏亞", "希洛唯", "歐格", "希洛唯", "夏亞", "卡爾瑪",
                "剎那F塞因", "夏亞", "剎那F塞因", "里包滋", "剎那F塞因", "穆福拉卡", "穆福拉卡", "阿斯蘭", "基拉大和", "真飛鳥",
                "羅", "基拉大和"];
                string[] reBookBookID = [bookid[0], bookid[0], bookid[0], bookid[1],
                    bookid[2], bookid[2], bookid[2], bookid[3], bookid[4], bookid[4], bookid[4], bookid[4], bookid[4],
                    bookid[5], bookid[5], bookid[5], bookid[7], bookid[8], bookid[8], bookid[8], bookid[9], bookid[9]];
                DateTime[] reDateTimes = [new DateTime(2024, 12, 20, 13, 20, 50, 600),
                new DateTime(2025, 1, 1, 15, 26, 51, 120),
                new DateTime(2025, 1, 1, 17, 23, 29, 321),
                new DateTime(2025, 1, 2, 11, 12, 15, 756),
                new DateTime(2025, 1, 3, 3, 56, 8, 675),
                new DateTime(2025, 1, 4, 0, 37, 55, 220),
                new DateTime(2025, 1, 5, 1, 48, 44, 123),
                new DateTime(2025, 1, 6, 8, 25, 33, 369),
                new DateTime(2025, 1, 7, 9, 12, 22, 111),
                new DateTime(2025, 1, 8, 19, 11, 11, 22),
                new DateTime(2024, 12, 20, 13, 20, 50, 600),
                new DateTime(2025, 1, 1, 15, 26, 51, 120),
                new DateTime(2025, 1, 1, 17, 23, 29, 321),
                new DateTime(2025, 1, 2, 11, 12, 15, 756),
                new DateTime(2025, 1, 3, 3, 56, 8, 675),
                new DateTime(2025, 1, 4, 0, 37, 55, 220),
                new DateTime(2025, 1, 5, 1, 48, 44, 123),
                new DateTime(2025, 1, 6, 8, 25, 33, 369),
                new DateTime(2025, 1, 7, 9, 12, 22, 111),
                new DateTime(2025, 1, 8, 19, 11, 11, 22),
                new DateTime(2025, 1, 7, 9, 12, 22, 111),
                new DateTime(2025, 1, 8, 19, 11, 11, 22)];
                for (int i = 0; i < RebookDisciption.Length; i++)
                {
                    RebookID.Add(Guid.NewGuid().ToString());
                    Rebook rebook = new Rebook();
                    rebook.RebookID = RebookID[i];
                    rebook.Author = reAuthor[i];
                    rebook.Description = RebookDisciption[i];
                    rebook.TimeStmp = reDateTimes[i];
                    rebook.BookID = reBookBookID[i];
                    _content.Rebooks.Add(rebook);
                }
                _content.SaveChanges();

                string SeedPhotosPath = Path.Combine(Directory.GetCurrentDirectory(), "photo");            //取得來源照片的路徑
                string BookPhotosPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BookPhotos"); //取得目的路徑
                if (!Directory.Exists(BookPhotosPath))
                {
                    Directory.CreateDirectory(BookPhotosPath);
                }
                string[] files = Directory.GetFiles(SeedPhotosPath);        //取得指定路徑中的所有檔案

                for (int i = 0; i < files.Length; i++)
                {
                    string destFile = Path.Combine(BookPhotosPath, bookid[i] + ".jpg");
                    File.Copy(files[i], destFile);
                }
            }
        }
    }
}

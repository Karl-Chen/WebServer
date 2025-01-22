using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;
using System.Diagnostics;
using System.IO;

namespace MyModel_CodeFirst.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GuestBookContext _context;
        
        public HomeController(ILogger<HomeController> logger, GuestBookContext context)
        {
            _logger = logger;
            _context = context;
 
        }

        public async Task<IActionResult> Index()
        {
            //3.1.2  在HomeController中加入讀取Book資料表的程式
            var photos = await _context.Book.Where(b => b.Photo != null).OrderByDescending(b => b.TimeStmp).Take(5).ToListAsync();
            return View(photos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


//MyModel_CodeFirst專案進行步驟

//1. 使用Code First建立Model及資料庫

//1.1   在Models資料夾裡建立Book及ReBook兩個類別做為模型
//1.1.1 在Models資料夾上按右鍵→加入→類別，檔名取名為Book.cs，按下「新增」鈕
//1.1.2 設計Book類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(Display)
//1.1.3 在Models資料夾上按右鍵→加入→類別，檔名取名為ReBook.cs，按下「新增」鈕
//1.1.4 設計ReBook類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(Display)
//1.1.5 撰寫兩個類別間的關聯屬性做為未來資料表之間的關聯


//1.2   建立DbContext類別
//      ※安裝下列兩個套件※
//      (1)Microsoft.EntityFrameworkCore.SqlServer
//      (2)Microsoft.EntityFrameworkCore.Tools
//      ※與DB First安裝的套件一樣※
//1.2.1 在Models資料夾上按右鍵→加入→類別，檔名取名為GuestBookContext.cs，按下「新增」鈕
//1.2.2 撰寫GuestBookContext類別的內容
//      (1)須繼承DbContext類別
//      (2)撰寫依賴注入用的建構子
//      (3)描述資料庫裡面的資料表
//1.2.3 在appsettings.json中撰寫資料庫連線字串
//1.2.4 在Program.cs內以依賴注入的寫法註冊讀取連線字串的服務(food panda、Uber Eats)
//      ※注意程式的位置必須要在var builder = WebApplication.CreateBuilder(args);這句之後
//1.2.5 在套件管理器主控台(檢視 > 其他視窗 > 套件管理器主控台)下指令
//      ※※※注意注意※※※ 請先確定專案是否正確
//      ※※※注意注意※※※ 請先確定專案是否正確
//      ※※※注意注意※※※ 請先確定專案是否正確
//      (1)Add-Migration InitialCreate
//      (2)cc
//      ※第(1)項的「InitialCreate﹞是自訂的名稱，若執行成功會看到「Build succeeded.」※
//      ※另外會看到一個Migrations的資料夾及其檔案被建立在專案中，裡面紀錄著Migration的歷程※
//      ※第(1)項指令執行成功才能執行第(2)項指令※
//      (3)至SSMS中查看是否有成功建立資料庫及資料表(目前資料表內沒有資料)



//1.3   創建Initializer物件建立初始(種子)資料(Seed Data)
//      ※※※我們可以在創建資料庫時就創建幾筆初始的資料在裡面以供開發時測試之用※※※
//      ※※※請先將資料庫刪除，並將專案中Migrations資料夾及內含檔案整個刪除※※※

//1.3.1 準備種子照片(SeedPhotos資料夾)
//1.3.2 在Models資料夾上按右鍵→加入→類別，檔名取名為SeedData.cs，按下「新增」鈕
//1.3.3 撰寫SeedData類別的內容
//      (1)撰寫靜態方法 Initialize(IServiceProvider serviceProvider)
//      (2)撰寫Book及ReBook資料表內的初始資料程式
//      (3)撰寫上傳圖片的程式
//      (4)加上 using() 及 判斷資料庫是否有資料的程式
//1.3.4 在Program.cs撰寫啟用Initializer的程式(要寫在var app = builder.Build();之後)
//      ※這個Initializer的作用是建立一些初始資料在資料庫中以利測試，所以不一定要有Initializer※
//      ※注意:初始資料的照片放在BookPhotos資料夾中※
//1.3.5 建置專案，確定專案完全建置成功
//1.3.6 再次於套件管理器主控台(檢視 > 其他視窗 > 套件管理器主控台)下指令
//      (1)Add-Migration InitialCreate
//      (2)Update-database
//1.3.7 至SSMS中查看是否有成功建立資料庫及資料表(目前資料表內沒有資料)
//1.3.8 在瀏覽器上執行網站首頁以建立初始資料(若沒有執行過網站，初始資料不會被建立)
//1.3.9 再次至SSMS中查看資料表內是否有資料


//2.   製作留言板前台功能

//2.1   製作自動生成的Book資料表CRUD
//2.1.1 在Controllers資料夾上按右鍵→加入→控制器
//2.1.2 選擇「使用EntityFramework執行檢視的MVC控制器」→按下「加入」鈕
//2.1.3 在對話方塊中設定如下
//      模型類別: Book(MyModel_CodeFirst.Models)
//      資料內容類別: GuestBookContext(MyModel_CodeFirst.Models)
//      勾選 產生檢視
//      勾選 參考指令碼程式庫
//      勾選 使用版面配置頁
//      控制器名稱改為PostBooksController
//      按下「新增」鈕
//2.1.4 修改PostBooksController，移除Edit、Delete Action
//2.1.5 刪除Edit、Delete View檔案
//2.1.6 修改Index Action的寫法


//2.2   顯示功能
//2.2.1 修改適合前台呈現的Index View
//2.2.2 將PostBooksController中Details Action改名為Display(View也要改名字)
//2.2.3 在Index View中加入Display Action的超鏈結
//2.2.4 修改Display View 排版樣式，排版可以個人喜好呈現
//      ※排版可以個人喜好呈現※


//2.3   使用「ViewComponent」技巧實作「將回覆留言內容顯示於Display View」
//      ※此單元將要介紹ViewComponent的使用方式※
//2.3.1 在專案中新增ViewComponents資料夾(專案上按右鍵→加入→新增資料夾)以放置所有的ViewComponent元件檔
//2.3.2 在ViewComponents資料夾中建立VCReBooks ViewComponent(右鍵→加入→類別→輸入檔名→新增)
//2.3.3 VCReBooks class繼承ViewComponent(注意using Microsoft.AspNetCore.Mvc;)
//2.3.4 撰寫InvokeAsync()方法取得回覆留言資料
//2.3.5 在/Views/Shared裡建立Components資料夾，並在Components資料夾中建立VCReBooks資料夾
//2.3.6 在/Views/Shared/Components/VCReBooks裡建立檢視(右鍵→加入→檢視→選擇「Razor檢視」→按下「加入」鈕)
//2.3.7 在對話方塊中設定如下
//      檢視名稱: Default
//      範本:Empty(沒有模型)
//      不勾選 建立成局部檢視
//      不勾選 使用版面配置頁
//   ※注意：資料夾及View的名稱不是自訂的，而是有預設的名稱，規定如下：※
//   /Views/Shared/Components/{ComponentName}/Default.cshtml
//   /Views/{ControllerName}/Components/{ComponentName}/Default.cshtml
//2.3.8 在Default View上方加入@model IEnumerable<MyModel_CodeFirst.Models.ReBook>
//2.3.9 依喜好編輯Default View排版方式
//2.3.10 編寫Display View，加入VCReBooks ViewComponent
//2.3.11 測試


//2.4   留言功能
//2.4.1 修改Create View，修改上傳檔案的元件
//2.4.2 修改Create View，將<form>增加 enctype="multipart/form-data" 屬性
//2.4.3 加入前端效果，使照片可先預覽
//2.4.4 刪除ImageType欄位
//2.4.5 刪除TimeStamp欄位
//2.4.6 修改Post Create Action，加上處理上傳照片的功能
//2.4.7 測試留言功能
//2.4.8 在Index View中加入未上傳照片的留言之顯示方式
//2.4.9 在Display View中加入未上傳照片的留言之顯示方式
//2.4.10 在Index View中加入處理「有換行的留言」顯示方式
//2.4.11 在Display View中加入處理「有換行的留言」顯示方式
//2.4.12 在VCReBook View Component的Default View中加入沒有回覆留言即不顯示的判斷



//2.5   回覆留言功能
//2.5.1 在Controllers資料夾上按右鍵→加入→控制器
//2.5.2 選擇「使用EntityFramework執行檢視的MVC控制器」→按下「加入」鈕
//2.5.3 在對話方塊中設定如下
//      模型類別: ReBook(MyModel_CodeFirst.Models)
//      資料內容類別: GuestBookContext(MyModel_CodeFirst.Models)
//      勾選 產生檢視
//      勾選 參考指令碼程式庫
//      不勾選 使用版面配置頁
//      控制器名稱改為RePostBooksController
//      按下「新增」鈕
//2.5.4 修改RePostBooksController，僅保留Create Action，其它全部刪除
//2.5.5 僅保留Create View檔案，其它全部刪除
//2.5.6 修改 Create View
//      ※製作前後端分離的回覆留言功能※

//2.5.7 在PostBooks\Display View中將RePostBooks\Create View以Ajax方式讀入
//2.5.8 配合Boostrap Modal Component顯示出Create畫面
//2.5.9 傳遞BookID參數
//2.5.10 將ReBooks\Create View中<form>加上BookID的隱藏欄位
//2.5.11 測試效果
//2.5.12 修改ReBooksController中的Create Action，使其Return JSON資料
//2.5.13 在PostBooks\Display View中撰寫相關的JavaScript程式，以Ajax方式執行新增回覆留言
//2.5.14 將ReBooks\Create View的Form建立ID
//2.5.15 在ReBooksController中撰寫自VCRebook ViewComponent取得回覆留言資料的Action
//2.5.16 測試效果

//----------------------------------這條線以上是評量範圍-----------------------------------------------

//3     介面設及與佈局
//3.1   Bootstrap應用-利用Bootstrap裡的功能作首頁照片輪播
//3.1.1 在Home/Index View中使用Bootstrap的Carousel元件
//3.1.2 在HomeController中加入讀取Book資料表的程式
//3.1.3 編輯Home/Index View實現照片輪播效果 

//3.2   佈局設計
//3.2.1 在Shared資料夾中建立_UserLayout.cshtml佈局檔
//3.2.2 在Shared資料夾中建立_Layout.cshtml佈局檔
//3.2.3 設定Home/Index View的Layout為_UserLayout
//3.2.4 建立VCBooksTopThree.cs 的 ViewComponent
//3.2.5 撰寫VCBooksTopThree Class使其能讀出最新三筆留言
//3.2.6 在Shared/Components資料夾中建立VCBooksTopThree資料夾，並在其中建立Default.cshtml檔
//3.2.7 撰寫VCBooksTopThree的Default View，使其能顯示最新三筆留言
//3.2.8 在Home/Index View中加入VCBooksTopThree ViewComponent

//3.3   Layout的處理
//3.3.1 將Viewstart.cshtml中的Layout設定為_UserLayout
//3.3.2 修改_UserLayout的選單內容及呈現方式


//4.    製作留言板後台管理功能

//4.1   製作自動生成的Book資料表CRUD
//4.1.1 在Controllers資料夾上按右鍵→加入→控制器
//4.1.2 選擇「使用EntityFramework執行檢視的MVC控制器」→按下「加入」鈕
//4.1.3 在對話方塊中設定如下
//      模型類別: Book(MyModel_CodeFirst.Models)
//      資料內容類別: GuestBookContext(MyModel_CodeFirst.Models)
//      勾選 產生檢視
//      勾選 參考指令碼程式庫
//      勾選 使用版面配置頁
//      控制器名稱使用(BooksManageController)
//      按下「新增」鈕
//4.1.4 執行/BooksManage/Index 進行測試
//4.1.5 修改Index View將Photo及ImageType欄位、Create、Edit及Details超鏈結移除
//4.1.6 依喜好自行修改介面


//4.2   調整BooksManageController內容 
//4.2.1 改寫Index Action的內容，將留言依新到舊排序
//4.2.2 移除Details Action (亦可一併刪除 Details.cshtml)
//4.2.3 移除Create Action (亦可一併刪除 Create.cshtml)
//4.2.4 移除Edit Action (亦可一併刪除 Edit.cshtml)
//4.2.5 刪除Delete.cshtml


//4.3   在Index View呈現回覆留言資料
//4.3.1 在Index View中加入呈現回覆留言的ViewComponent
//4.3.2 新增一個VCRebooks/Delete.cshtml View
//4.3.3 將Delete View重新排版並加入刪除回覆留言的按鈕
//4.3.4 在VCRebooks ViewComponent中加入isDel參數判斷是否呈現Delete View
//4.3.5 在Index View中呈現回覆留言的ViewComponent增加isDel參數的傳遞
//4.3.6 使用Bootstrap的Collapse Component來呈現留言資料
//4.3.7 將呈現回覆留言的id改為動態產生
//4.3.8 測試畫面效果



//4.4   製作刪除留言功能
//4.4.1 在BooksManageController中的Delete Action加入刪除圖片的程式
//4.4.2 將BooksManage/Index View的刪除按鈕改寫成表單傳送
//4.4.3 測試刪除留言功能
//      ※這裡所製作的刪除會將有關聯的回覆留言資料一併刪除※
//4.4.4 在BooksManageController加人中的DeleteReBook Action
//4.4.5 將VCRebooks/Delete View的刪除鈕製作以Ajax方式刪除以保留頁面不整頁更新
//4.4.6 撰寫Ajax程式以呼叫DeleteReBook Action
//4.4.7 在BooksManageController中加入GetRebookByViewComponent Action
//4.4.8 利用GetRebookByViewComponent Action局部更新畫面以顯示刪除後的回覆留言資料畫面
//4.4.9 測試刪除留言功能
//      ※這裡所製作的刪除會將有關聯的回覆留言資料一併刪除※



//5   登入功能製作

//5.1   資料庫變更
//5.1.1 用Code-First方式在資料庫裡新增一個Login資料表存放管理者帳號密碼
//5.1.2 在Models資料夾裡建立Login類別做為模型
//5.1.3 Models資料夾上按右鍵→加入→類別，檔名取名為Login.cs，按下「新增」鈕
//5.1.4 設計Login類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(DisplayName)
//5.1.5 修改GuestBookContext類別的內容，加入描述資料庫裡Login的資料表
//5.1.6 在套件管理器主控台(檢視 > 其他視窗 > 套件管理器主控台)下指令
//      ※※※注意注意※※※ 請先確定專案是否正確
//      ※※※注意注意※※※ 請先確定專案是否正確
//      ※※※注意注意※※※ 請先確定專案是否正確
//      (1)Add-Migration AddLoginTable
//      (2)Update-database
//5.1.7 至SSMS中查看是否有成功建立Login資料表
//5.1.8 在Login資料表中建立一筆帳號密碼的資料(admin, 12345678)



//5.2   製作Login功能與畫面
//5.2.1 在Controllers資料夾上按右鍵→加入→控制器
//5.2.2 選擇「MVC控制器-空白」→按下「加入」鈕
//5.2.3 檔名取名為「LoginController」→按下「新增」鈕
//5.2.4 建立Get與Post的Login Action
//5.2.5 建立Login View(Login Action中按右鍵→新增檢視→Razor檢視→按下「加入」鈕)
//      在對話方塊中設定如下
//      檢視稱: Login (使用預設名稱)
//      範本: Create
//      模型類別: Login(MyModel_CodeFirst.Models)
//      資料內容類別: GuestBookContext(MyModel_CodeFirst.Models)
//      不勾選 建立成局部檢視
//      勾選 參考指令碼程式庫
//      勾選 使用版面配置頁
//5.2.6 將ViewData["Error"]加入Login View
//5.2.7 測試


//※※※注意注意※※※
//※此時的登入與非登入狀況根本沒有被紀錄，即使沒有登入，仍能直接以url進入後台管理頁面※
//※因此我們必須將有登入過的使用者紀錄下來，如果沒有登入紀錄的使用者即使透過url也進不去後台管理頁面※


//5.3   建立進入後台必須是登入狀態實作
//5.3.1 在_ViewStart.cshtml中加入前台及後台使用不同Layout的判斷式
//5.3.2 在_UserLayout.cshtml中加入登入後台的按鈕
//5.3.3 在Program.cs中註冊及啟用Session
//5.3.4 在LoginController的Post Login Action中加入Session紀錄登入狀態
//5.3.5 在_ViewStart.cshtml加入未登入則將網頁自動導往/Home/Index的判斷式
//5.3.6 測試


//5.4   登出功能
//5.4.1 在LoginController加入Logout Action
//5.4.2 在_Layout.cshtml及_UserLayout.cshtml加入登出的按鈕



//6     基本的錯誤處理(Error Handle)
//6.1   測試:故意讓程式發生例外看看結果
//6.1.1 在PostBooksController裡寫一個會發生例外的Action如下
//public IActionResult ExceptionTest()
//{
//    int a = 0;
//    int s = 100 / a;
//    return View();
//}
//6.1.2 利用原本預設的Home Controller Error Handler來處理例外
//6.1.3 修改 Program.cs中的程式碼，將是否在開發模式下的錯誤處理判斷註解掉
//6.1.4 測試:再次故意讓程式發生例外看看結果
//6.1.5 修改Error.cshtml內容
//6.2   測試:故意讓程式發生HttpNotFound(404)錯誤
//6.2.1 在Program.cs中的程式碼註冊處理HttpNotFound(404)錯誤的Error Handler
//6.2.2 測試:再次故意讓程式發生HttpNotFound(404)錯誤
//      ※最基本的錯誤處理，就是不讓使用者看到系統錯誤訊息※


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyModel_DBFirst.Models;
using MyModel_DBFirst.ViewModels;

namespace MyModel_DBFirst.Controllers
{
    public class MyStudentsController : Controller
    {
        //4.1.4 撰寫建立DbContext物件的程式
        //6.2   修正tStudentsController建立DbContext物件的寫法
        //6.2.1 將步驟2.2.1所寫的建立DbContext物件的程式註解
        //6.2.2 將步驟2.2.2所註解掉的程式取消註解(這裡的寫法是scaffold預設的依賴注入寫法)
        //6.2.3 參照tStudentsController，修改MyStudentsController中建立DbContext物件的程式為依賴注入寫法
        //dbStudentsContext db = new dbStudentsContext();
        private readonly dbStudentsContext db;
        public MyStudentsController(dbStudentsContext context)
        {
            db = context;
        }

        //4.2   建立同步執行的Index Action
        public IActionResult Index(string deptid = "01")
        {
            //4.2.1 撰寫Index Action程式碼
            //把資料庫tStudent資料表資料取出，回傳給View
            //db.tStudent = select * from tStudent(把資料庫tStudent資料表資料取出)
            //Linq
            //db.tStudent.ToList() ToList()是把db.tStudent做實體化，只在server上做
            //5.5.1 修改 Index Action
            //var student = db.tStudent.Include(s=>s.Department).ToList();

            //return View(student);                  //回傳給View

            //5.8.4 修改MyStudnetsController裡的Index Action
            
            VMtStudent students = new VMtStudent()
            {
                Students = db.tStudent.Where(s=>s.DeptID == deptid).ToList(),
                Departments = db.Department.ToList(),
            };
            if (students.Students.Count == 0)
                ViewData["ErrMsg"] = "該科系目前沒有學生";
            ViewData["DeptName"] = db.Department.Find(deptid).DeptName;
            ViewData["DeptID"] = deptid;
            return View(students);
        }


        //4.2.2 建立Index View
        //4.2.3 在Index Action內按右鍵→新增檢視→選擇「Razor檢視」→按下「加入」鈕
        //4.2.4 在對話方塊中設定如下
        //      檢視名稱: Index
        //      範本:List
        //      模型類別: tStudent(MyModel_DBFirst.Models)
        //      資料內容類別: dbStudentsContext(MyModel_DBFirst.Models)
        //      不勾選 建立成局部檢視
        //      不勾選 參考指令碼程式庫
        //      勾選 使用版面配置頁
        //4.2.5 執行Index View測試
        //4.2.6 修改介面上的文字，拿掉Details的超鏈結
        //      ※可依自己的喜好修改View的顯示※

        public IActionResult Create(string deptid="01")
        {
            ViewData["Department"] = new SelectList(db.Department, "DeptID", "DeptName");   //有三個參數，第一個是資料來源，第二個是要對應的資料(option的value)，第三個是要顯示的資料
            ViewData["DeptID"] = deptid;
            return View();
        }

        // 加入Token驗證機制
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(tStudent tStudent)
        {
            //4.3.7 加入主鍵是否重覆的檢查
            //Find只能放主鍵，檢查此主鍵是否已存在
            //select * from tStudent where fStuId = '113004'
            var stu = db.tStudent.Find(tStudent.fStuId);
            if (stu != null)
            {
                ViewData["ErrMsg"] = "該學號已被使用了";
                return View(tStudent);
            }
            //將tStudent表單資料寫入資料庫
            //新增資料-Insert into values(,,,)
            //ModelState.IsValid 判斷是否符合設定的規範
            if (ModelState.IsValid)
            {
                db.tStudent.Add(tStudent);
                db.SaveChanges();               //他會轉譯成Insert into values(,,,)
                //ViewData["DeptID"] = deptid;
                return RedirectToAction("Index", new { deptid=tStudent.DeptID});
            }
            ViewData["ErrMsg"] = "您輸入的欄位內容可能有誤，請重新輸入，或聯絡系統管理員";
            return View(tStudent);
        }

        //4.4   建立同步執行的Edit Action
        //4.4.1 撰寫Edit Action程式碼(需有兩個Edit Action)
        //4.4.2 建立Edit View
        public IActionResult Edit(string id, string deptid)
        {
            //select * from tstudent where fStuId=id
            //Linq
            //var result = from s in db.tStudent
            //             where s.fStuId = id
            //             select s;
            // Find只能用在主索引鍵
            var stu = db.tStudent.Find(id);
            if (stu == null)
            {
                ViewData["ErrMsg"] = "該學號不存在";
                return NotFound();
            }
            //5.5.5修改 Edit Action
            ViewData["Department"] = new SelectList(db.Department, "DeptID", "DeptName");   //有三個參數，第一個是資料來源，第二個是要對應的資料(option的value)，第三個是要顯示的資料
            ViewData["DeptID"] = deptid;
            return View(stu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(tStudent tStudent, string fStuId)
        {
            if (fStuId != tStudent.fStuId)
            {
                return View(tStudent);
            }
            if (ModelState.IsValid)
            {
                db.Update(tStudent);
                db.SaveChanges();       //轉成SQL的Update語法
                return RedirectToAction("Index", new { tStudent.DeptID});          //導回index
            }
            return View(tStudent);
        }

        //4.5.1 撰寫Delete Action程式碼
        //4.5.2 將Index的Delete改為Form，以Post方式送出
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id, string deptid)
        {
            // delete from tStudent where fStudId=@id
            var tStudent = db.tStudent.Find(id);
            if (tStudent != null)
            {
                db.tStudent.Remove(tStudent);
                db.SaveChanges();       //轉成SQL的Update語法
            }
            return RedirectToAction("Index", new { deptid});          //導回index
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MyModel_DBFirst.Models;
using System.Diagnostics;

namespace MyModel_DBFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

//MyModel_DBFirst�M�׶i��B�J

//1.�ϥ�DB First�إ� Model

//1.1 �bSSMS������dbStudents.sql�{���X�A�إ߽d�Ҹ�ƮwdbStudents�A���t�@��tStudent��ƪ�

//1.2 �إ߱M�׻P��Ʈw���s�u
//1.2.1 �ϥ�NuGet(�M�צW�٤W���k����޲zNuGet�M��)�w�ˤU�C�M��
//      (1) Microsoft.EntityFrameworkCore.SqlServer
//      (2) Microsoft.EntityFrameworkCore.Tools

//1.2.2 ��SSMS�]�w�n�JSQL Server���ϥΪ�(�������ճs�u���\)

//1.2.3 ��M��޲z���D���x(�˵� > ��L���� > �M��޲z���D���x)�U���O
//      Scaffold-DbContext "Data Source=���A����};Database=��Ʈw�W��;TrustServerCertificate=True;User ID=�b��;Password=�K�X" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force
//      �Y���\���ܡA�|�ݨ�Build succeeded.�r���A�æbModels��Ƨ��̬ݨ�dbStudentsContext.cs(�y�z��Ʈw)��tStudent.cs(�y�z��ƪ�)

//1.2.4 �bdbStudentsContext.cs�̼��g�s�u���Ʈw���{��
//      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//              => optionsBuilder.UseSqlServer("Data Source=���A����};Database=��Ʈw�W��;TrustServerCertificate=True;User ID=�b��;Password=�K�X");

//1.2.5 �bdbStudentsContext.cs�̼��g�@�ӪŪ��غc�l
//      public dbStudentsContext()
//      {
//      }
///////////////////////////////////////////////////////
///

//2.�s�@�۰ʥͦ���tStudent��ƪ���CRUD�\��

//2.1 �ϥ�Scaffold��k��Visual Studio�۰ʫإߥXtStudent��ƪ�����CRUD�\��
//    �]�ttStudentController��Index.cshtml�BCreate.cshtml�BEdit.cshtml�BDelete.cshtml�BDetails.cshtml������View�������{���X

//2.1.1 �bControllers��Ƨ��W���k����[�J�����
//2.1.2 ��ܡu�ϥ�EntityFramework�����˵���MVC����v�����U�u�[�J�v�s
//2.1.3 �b��ܤ�����]�w�p�U
//      �ҫ����O: tStudent(MyModel_DBFirst.Models)
//      ��Ƥ��e���O: dbStudentsContext(MyModel_DBFirst.Models)
//      �Ŀ� �����˵�
//      �Ŀ� �Ѧҫ��O�X�{���w
//      �Ŀ� �ϥΪ����t�m��
//      ����W�٨ϥιw�]�Y�i(tStudentsController)
//      ���U�u�s�W�v�s
//2.1.4 ����Visual Studio�|�i��Scaffolding�ʧ@�A�N���X�@��tStudentsController
//      (�|�]�t�Ҧ�������Action)�Τ���View(Index.cshtml�BCreate.cshtml�BEdit.cshtml�BDelete.cshtml�BDetails.cshtml)�������{���X

//      ���Ƶ�������
//      Index.cshtml(List�d��)
//      Create.cshtml(Create�d��)
//      Edit.cshtml(Edit�d��)
//      Delete.cshtml(Delete�d��)
//      Details.cshtml(Details�d��)


//2.2   �ק�tStudentsController���e
//2.2.1 ���g�إ�DbContext���󪺵{��
//      dbStudentsContext _context=new dbStudentsContext();
//2.2.2 �N����J�����{���X(�p�U)���ѱ�
//      private readonly dbStudentsContext _context;
//      public tStudentsController(dbStudentsContext context)
//      {
//          _context = context;
//      }
//���۰ʥͦ���Controller�g�k���̿�`�J(Dependency Injection, DI)�A�ثe�ڭ̩|���Ǩ�A�]�����Τ@��new���󪺼g�k��
//2.3   ����Index View�i��CRUD�\�����

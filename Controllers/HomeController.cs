using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleApplication.Models;
using System.Diagnostics;
using System.Web;
using SampleApplication.Data;
using SampleApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace SampleApplication.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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

        [HttpGet]
        public IActionResult GetAudits(string sidx, string sort, int page, int rows, bool _search, string searchField, string searchOper, string searchString)  
        {
            var audits = _context.Audits.ToList();
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            /* if(_search)
            {
                switch(searchField)
                {
                    case "Name":
                        StudentList=StudentList.Where(t => t.Name.Contains(searchString)).ToList();
                        break;
                    case "FatherName":
                        StudentList = StudentList.Where(t => t.FatherName.Contains(searchString)).ToList();
                        break;
                    case "Gender":
                        StudentList = StudentList.Where(t => t.Gender.Contains(searchString)).ToList();
                        break;
                    case "ClassName":
                        StudentList = StudentList.Where(t => t.ClassName.Contains(searchString)).ToList();
                        break;
                }
            }*/
            int totalRecords = audits.Count();
            var totalPages = 1;
            /*if (sort.ToUpper() == "DESC")
            {
                StudentList = StudentList.OrderByDescending(t => t.Name).ToList();
                StudentList = StudentList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                StudentList = StudentList.OrderBy(t => t.Name).ToList();
                StudentList = StudentList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }*/
            page =1 ;
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = audits
            };
            return Json(jsonData);
        }

        [HttpPost]
        public string Create([Bind] Audit Model)
        {
            ModelState.Remove("AuditId");
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Audits.Add(Model);
                    _context.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }
        public string Edit(Audit Model)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(Model).State = EntityState.Modified;
                    _context.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }
        public string Delete(int Id)
        {
            var audit = _context.Audits.Find(Id);
            _context.Audits.Remove(audit);
            _context.SaveChanges();
            return "Deleted successfully";
        }
    }
}
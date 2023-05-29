using Microsoft.AspNetCore.Mvc;
using MockStudentManager.ViewModels;
using StudentManager.DBModels;
using StudentManager.IRepository;
using System.Collections.Generic;

namespace MockStudentManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="studentRepository"></param>
        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            
            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            return View(students);
        }


        public IActionResult Detail(int? Id)
        { 

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                student = _studentRepository.GetStudent(Id??1),
                PageTitle = "学生详情"
            };
            //ViewData["PageTitle"] = "学生详情";
            //ViewData["Student"] = model;


            return View(homeDetailsViewModel);
        }
    }
}

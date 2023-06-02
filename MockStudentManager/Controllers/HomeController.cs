using Microsoft.AspNetCore.Mvc;
using MockStudentManager.ViewModels;
using StudentManager.DBModels;
using StudentManager.IRepository;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting.Internal;
using System.IO;
using System;

namespace MockStudentManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly HostingEnvironment hostingEnvironment;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="studentRepository"></param>
        public HomeController(IStudentRepository studentRepository, HostingEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            
            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            return View(students);
        }


        public IActionResult Detail(int? Id)
        {
            //return $"id={Id},并且名字为{name}";
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                student = _studentRepository.GetStudent(Id ?? 1),
                PageTitle = "学生详情"
            };

            //ViewData["PageTitle"] = "学生详情";
            //ViewData["Student"] = model;


            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        { 
            return View();

        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel student)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (student.Photos != null)
                {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath,"images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + student.Photos[0].FileName;
                    string filePath = Path.Combine(uploadFolder,uniqueFileName);
                    student.Photos[0].CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Student newstudent = new Student() {
                    Name = student.Name,
                    Email = student.Email,
                    ClassName = student.ClassName,
                    PhotoPath = uniqueFileName
                };

                _studentRepository.Add(newstudent);

                //return View(newstudent);

                return RedirectToAction("Detail", new { Id = newstudent.Id });
            }

            return View();


        }

        public string JsonString() {

            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            string strJson = JsonConvert.SerializeObject(students);
            List<Student> list = JsonConvert.DeserializeObject<List<Student>>(strJson);

            return strJson;
        }
    }
}

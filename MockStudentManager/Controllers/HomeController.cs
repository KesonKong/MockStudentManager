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
                string uniqueFileName = ProcessUploadFile(student);

                Student newstudent = new Student{
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


        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            Student student = _studentRepository.GetStudent(Id.Value);

            StudentEditViewModel studentEditView = new StudentEditViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                ClassName = student.ClassName,
                ExistingPhotoPath = student.PhotoPath
            };

            return View(studentEditView);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student student = _studentRepository.GetStudent(model.Id);

                if (student != null)
                {
                    student.Email = model.Email;
                    student.ClassName = model.ClassName;
                    student.Name = model.Name;

                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    student.PhotoPath = ProcessUploadFile(model);

                    Student updatestudeng = _studentRepository.Update(student);
                    return RedirectToAction("Index");
                }

            }
            return View();
        }

        public string JsonString() {

            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            string strJson = JsonConvert.SerializeObject(students);
            List<Student> list = JsonConvert.DeserializeObject<List<Student>>(strJson);

            return strJson;
        }

        /// <summary>
        /// 将照片保存到指订的路径，并返回唯一的文件名
        /// </summary>
        /// <returns></returns>
        private string ProcessUploadFile(StudentCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {
                foreach (var photo in model.Photos)
                {
                    //必须将图片路径上传到wwwroot文件夹中
                    //要获取wwwroot文件路径，需要使用APS.NET Core提供的 HostingEnvironment 服务
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photos[0].FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    //使用IFormFile接口提供的CopyTo()方法，将文件复制到wwwroot/images文件夹
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
                
            }

            return uniqueFileName;
        }
    }
}

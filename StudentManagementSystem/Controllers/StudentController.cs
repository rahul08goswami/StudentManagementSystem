using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagementSystem.Models;
using System.Drawing;
using System.Text;
using System.Text.Json.Serialization;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7009/api/StudentAPI/";
        private HttpClient client = new HttpClient();
        IWebHostEnvironment environment;
        public StudentController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Student>>(result);
                if (data != null)
                {
                    students = data;
                }
            }
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentVM student)
        {
            string fileName = "";
            if (student != null)
            {
                string ext = Path.GetExtension(student.Photo.FileName);
                long size = student.Photo.Length;
                List<string> extensions = new List<string>()
                {
                    ".png",
                    ".jpg",
                    ".jpeg"
                };
                if (extensions.Contains(ext))
                {
                    if (size <= 100000)
                    {
                        string folder = Path.Combine(environment.WebRootPath, "images");
                        fileName = Guid.NewGuid().ToString() + "_" + student.Photo.FileName;
                        string filePath = Path.Combine(folder, fileName);
                        student.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        Student s = new Student()
                        {
                            Name = student.Name,
                            Age = student.Age,
                            Gender = student.Gender,
                            Id = student.Id,
                            Image_Path = fileName
                        };
                        string std = JsonConvert.SerializeObject(s);
                        StringContent content = new StringContent(std, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync(url, content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            TempData["insert_message"] = " Student Added..";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["error_size"] = "Images must be less than 1MB..";
                    }
                }
                else
                {
                    TempData["error_extensions"] = "Only " + extensions[0] + " " + extensions[1] + " " + extensions[2] + " are allowed";
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            StudentVM student = new StudentVM();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<StudentVM>(result);
                if (data != null)
                {
                    student = data;
                }
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(StudentVM student)
        {
            string fileName = "";
            if (student != null)
            {
                string ext = Path.GetExtension(student.Photo.FileName);
                long size = student.Photo.Length;
                List<string> extensions = new List<string>()
                {
                    ".png",
                    ".jpg",
                    ".jpeg"
                };
                if (extensions.Contains(ext))
                {
                    if (size <= 100000)
                    {
                        string folder = Path.Combine(environment.WebRootPath, "images");
                        fileName = Guid.NewGuid().ToString() + "_" + student.Photo.FileName;
                        string filePath = Path.Combine(folder, fileName);
                        student.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        Student s = new Student()
                        {
                            Name = student.Name,
                            Age = student.Age,
                            Gender = student.Gender,
                            Id = student.Id,
                            Image_Path = fileName
                        };
                        Student s1 = new Student()
                        {
                            Name = student.Name,
                            Age = student.Age,
                            Gender = student.Gender,
                            Id = student.Id,
                            Image_Path = fileName
                        };
                        string std = JsonConvert.SerializeObject(s1);
                        StringContent content = new StringContent(std, Encoding.UTF8, "application/json");
                        string finalUrl = url + student.Id;
                        HttpResponseMessage response = client.PutAsync(url + student.Id, content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            TempData["update_message"] = " Student Updated..";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["error_size"] = "Images must be less than 1MB..";
                    }
                }
                else
                {
                    TempData["error_extensions"] = "Only " + extensions[0] + " " + extensions[1] + " " + extensions[2] + " are allowed";
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Student student = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    student = data;
                }
            }
            return View(student);
        }
        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            Student student = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    student = data;
                }
            }
            return View(student);
        }


        [HttpPost("{id}"), ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(url + id).Result; ;
            if (response.IsSuccessStatusCode)
            {
                TempData["delete_message"] = " Student Deleted..";
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}

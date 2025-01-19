using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagementSystem.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7009/api/StudentAPI/";
        private HttpClient client = new HttpClient();
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
        public IActionResult Create(Student student)
        {
            string std = JsonConvert.SerializeObject(student);
            StringContent content = new StringContent(std, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = " Student Added..";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
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

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            string std = JsonConvert.SerializeObject(student);
            StringContent content = new StringContent(std, Encoding.UTF8, "application/json");
            string finalUrl = url + student.Id;
            HttpResponseMessage response = client.PutAsync(url + student.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = " Student Updated..";
                return RedirectToAction("Index");
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

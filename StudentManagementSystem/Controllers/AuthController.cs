using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagementSystem.Models;
using System.Text;

namespace StudentManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        private string url = "https://localhost:7009/api/Auth";
        private HttpClient client = new HttpClient();

        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            string usr = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(usr, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login loginModel)
        {
            string usr = JsonConvert.SerializeObject(loginModel);
            StringContent content = new StringContent(usr, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url + "/login", content).Result;
            var token = response.Content.ReadAsStringAsync().Result;

            HttpContext.Session.SetString("AuthToken", token);
            return RedirectToAction("Index", "Student");
            //HttpClient client= new HttpClient();

            //if (response.IsSuccessStatusCode)
            //{
            //    // Extract the token from the response body
            //    var responseBody = response.Content.ReadAsStringAsync().Result;

            //    try
            //    {
            //        // Deserialize the JSON response
            //        var responseDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

            //        if (responseDict != null && responseDict.ContainsKey("Token"))
            //        {
            //            string token = responseDict["Token"];

            //            // Store token in Session
            //            HttpContext.Session.SetString("AuthToken", token);

            //            return RedirectToAction("Index", "Student");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Token key missing in API response.");
            //            return View("Error", "Invalid Token Response");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Deserialization Error: " + ex.Message);
            //        return View("Error", "Invalid Token Format");
            //    }
            //    //var token = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody)["Token"];

            //    //// Store token in Session (or TempData)
            //    //HttpContext.Session.SetString("AuthToken", token);

            //    //return RedirectToAction("Index", "Student");
            //}

            return View();
        }

    }
}

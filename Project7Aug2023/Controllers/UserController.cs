using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Project7Aug2023.Models;

namespace Project7Aug2023.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //For other services
        [HttpPost]
        public ActionResult<bool> Send(Employee emp)
        {
            string url = ""; // Write URL in here
            url = "https://jsonmock.hackerrank.com/api/football_matches";
            
            //string url = "https://dummy.restapiexample.com/api/v1/create?name=sincanc";
            using (var client = new WebClient())
            {
                try
                {
                    client.Headers.Add("Authorization", "Sance");
                    
                    string response = client.DownloadString(url);
                    var json = JObject.Parse(response);
                    var b = json.GetValue("page");
                    var data = new Employee();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }    
        }

        [HttpPost]
        public int ApiPost(Employee emp)
        {
            string apiUrl = "https://jsonmock.hackerrank.com/api/football_matches"; // Write URL in here
            string apiUrl2 = "https://dummy.restapiexample.com/api/v1/create";

            try
            {
                // Create an instance of HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Set request headers (if needed)
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Sance Aenul Yakin");
     

                    // Send the GET request synchronously
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content synchronously
                        string responseContent = response.Content.ReadAsStringAsync().Result;

                        JObject jsonObject = JObject.Parse(responseContent);

                        // Access JSON properties as needed

                        //string title = (string)jsonObject["page"];
                        var b = jsonObject.GetValue("page");
                        var data = jsonObject.GetValue("data");
                        List<int> yrs = new List<int>();
                        List<dataObj> dt = data.ToObject<List<dataObj>>();
                        foreach (var datas in dt)
                        {
                            yrs.Add(datas.year);
                        }

                    }
                    else
                    {
                        // Handle non-successful responses
                        Console.WriteLine($"HTTP Status Code: {response.StatusCode}");
                        Console.WriteLine("Request was not successful.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return 0;
        }

        public class dataObj
        {
            public int year { get; set; }
            
        }

        // Local data for testing without database
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Username = "user1", Email = "user1@example.com" },
            new User { Id = 2, Username = "user2", Email = "user2@example.com" }
        };


        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }


        [HttpGet]
        public ActionResult<User> GetUser(int id)
        {
            var user = users.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User newUser)
        {
            newUser.Id = users.Count + 1;
            users.Add(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var user = users.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var user = users.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            users.Remove(user);

            return NoContent();
        }



    }
}


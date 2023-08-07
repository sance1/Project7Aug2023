using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            using (var client = new WebClient())
            {
                try
                {
                    client.Headers.Add("Name", emp.Name);
                    client.Headers.Add("Department",emp.Department);
                    string response = client.DownloadString(url);
                    var json = JObject.Parse(response);

                    var data = new Employee();
                    data.Name = json.GetValue("Name").ToString();
                    data.Department = json.GetValue("Department").ToString();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }    
        }

        // Data local
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        Uri baseaddress = new Uri("https://reqres.in/api/users");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }
        [HttpGet]
        // GET: User
        public ActionResult Index()
        {
            List <User> user = new List<User>();
            RootObject datalist = new RootObject();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                
                datalist = JsonConvert.DeserializeObject<RootObject>(data);
                foreach (var item in datalist.data)
                {
                    user.Add(item);                  
                }

            }
            var model = user.Select(i => new User
            {
                id = i.id,
                email = i.email,
                first_name = i.first_name,
                last_name = i.last_name,
                password = i.password
            }).ToList();
            return View(model);
        }

        [HttpGet]

        public ActionResult Details(int id)
        {
            List<User> user = new List<User>();
            User usermodel = new User();
            RootObjectSingle datalist = new RootObjectSingle();
            HttpResponseMessage response = client.GetAsync("https://reqres.in/api/users/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                datalist = JsonConvert.DeserializeObject<RootObjectSingle>(data);
                usermodel = datalist.data;
                user.Add(datalist.data);
            }
            return View(usermodel);
        }

        public class RootObject
        {
            public List<User> data { get; set; }
        }
        public class RootObjectSingle
        {
            public User data { get; set; }
        }
    }
}
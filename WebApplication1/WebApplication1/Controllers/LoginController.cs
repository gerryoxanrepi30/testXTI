using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using RestSharp;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(int id = 0)
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Login(User userModel)
        {

            var client = new RestClient("https://reqres.in");
            var request = new RestRequest("api/login", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(userModel);
            var respon = client.Execute(request);

            if (respon.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("../User/Index");
            }
            else
            {
                return RedirectToAction("Login/login");
            }
        }

       

    }
}
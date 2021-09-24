using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Data;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace Projects.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public IActionResult Index()
        {
            return View();
        }

        //GET-Register
        public IActionResult Register()
        {
            return View();
        }

        //POST-Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Account obj)
        {
            if (ModelState.IsValid)
            { 
                Account account = new Account();
                Random rnd = new Random();
                account.Id = rnd.Next(1, 999);
                account.Name = obj.Name;
                account.Email = obj.Email;
                account.Password = obj.Password;

                _db.Accounts.Add(account);
                _db.SaveChanges(); 
                ViewBag.Message = obj.Name + " Succsessfully Registered";
            }
            return View();
        }

        //GET-Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST-Login
        public IActionResult Login(Account obj)
        {
            var user = _db.Accounts.Single(x => x.Email == obj.Email && x.Password == obj.Password);
            if (user!= null)
            {
                HttpContext.Session.SetInt32("Id", obj.Id);
                HttpContext.Session.SetString("Name", obj.Name);
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wrong");
            }
            return View();
        }

        public IActionResult LoggedIn()
        {
            if (HttpContext.Session.GetInt32("Id")!= null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project2.Models;

namespace Project2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(UserModel u)
        {
            if(u.Password!=null)
            {
                ViewData["Message"]="Register OK";
                return View();
            }
            return View();
        }
        public IActionResult Logged(UserModel u)
        {
            if(String.IsNullOrWhiteSpace(u.Login) || String.IsNullOrWhiteSpace(u.Password))
            {
                ViewData["Message"]= "Uzupelnij pole login i haslo";
                return View("Login");
            }
            if(u.Login!="admin" && u.Password!="admin")
            {
                ViewData["Message"]= "Niepoprawne Login lub haslo";
                return View("Login");
            }
            // sprawdzanie hasla i loginu z bazą danych i logowanie do logged jak wszystko ok
            return View(u);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Middle(UserModel u)
        {
            //login czy jest taki sam w bazie danych
            string pass = u.Password;
            string login = u.Login;
            
            
            bool passValid = RegexModel.MatchPasswordWithRegex(u.Password,u.Reg);

            if(passValid)
            {

                ViewData["Register"]="ok";
                ViewData["Message"]=login+" "+pass+" "+" "+u.Reg+" "+passValid;
                return View("Index");
            }
            else
            {
                ViewData["Register"]="no";
                ViewData["Message"]="Somethink wrong!";
                return View("CreateRegex");
            }
            
        }
        public IActionResult Regist(UserModel u)
        {
            if(String.IsNullOrWhiteSpace(u.Description))
            {
                ViewData["Message"]="You must complete the description";
                return View("CreateRegex");
            }
            if(u.Description.Length>=200)
            {
                ViewData["Message"]="Description must be less than 200";
                return View("CreateRegex");
            }
            string reg = RegexModel.CreateReg(u.Description,u.MinLength,
            u.chMinLength,u.MaxLength,u.chMaxLength,u.MinUppercase,
            u.chUppercase,u.MinLowercase,u.chLowercase,u.MinSpecialSigns,
            u.chSpecialSigns,u.chDigits,u.MinDigits);
            u.Reg=reg;

            return View(u);
        }
        public IActionResult Login(UserModel u)
        {
            
            return View();
        }
        public IActionResult CreateRegex()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

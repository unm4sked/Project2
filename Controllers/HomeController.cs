using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                ViewData["Message"]= "Enter Login and Password";
                return View("Login");
            }
           
            if(Db.LoginToService(u.Login,u.Password) !=0)
            {
                UserModel u2 = new UserModel();
                var list = new List<String>();
                list = Db.ViewQuerry(u.Login);
                //u2 = Db.ViewQuerry(u.Login);
                u2.Login = list[0];
                u2.Password = list[1];
                u2.Reg = list[2];
                u2.Description = list[3];
                u2.MinLength = Int32.Parse(list[4]);
                u2.MaxLength = Int32.Parse(list[5]);
                u2.MinLowercase = Int32.Parse(list[6]);
                u2.MinSpecialSigns = Int32.Parse(list[7]);
                u2.MinUppercase = Int32.Parse(list[8]);
                u2.MinDigits = Int32.Parse(list[9]);




                return View(u2);
            }
            else
            {
                ViewData["Message"] = "Incorrect Login or Password";
                return View("Login");
            }
           
        }

        public IActionResult GenerateXml(UserModel u)
        {
            if (String.IsNullOrEmpty(u.Login))
            {
                return RedirectToAction("Index");
            }
            Xml xml = new Xml();
            try
            {
                xml.GenerateXml(u);
            }
            catch(Exception ex)
            {
                ViewData["ok"] = "no";
                ViewData["Message"] = ex.Message;
                return View(u);
            }
            ViewData["ok"] = "ok";
            ViewData["Message"] = "XML generated correctly";

            

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
            if (login == null || pass == null)
            {
                ViewData["Register"] = "no";
                ViewData["Message"] = "Login & Password cannot be empty";
                return View("Regist",u);
            }
            if (login.Length > 200 || pass.Length > 200 || u.Description.Length > 200)
            {
                ViewData["Register"] = "no";
                ViewData["Message"] = "Error";
                return View("Regist", u);
            }

            if (u.Reg == null || u.Description == null)
            {
                ViewData["Register"] = "no";
                ViewData["Message"] = "Regex & Description cannot be empty";
                return View("Regist", u);
            }
            
            bool passValid = RegexModel.MatchPasswordWithRegex(u.Password,u.Reg);

            if(passValid)
            {

                ViewData["Register"]="ok";
               
                if(Db.CheckLogin(u.Login) == 0)
                {
                    //do 
                    
                    var statusAddUser = Db.AddUser(u);
                    //ViewData["Message"]=login+" "+pass+" "+" "+u.Reg+" "+passValid + Hash.Hashing(pass);

                    ViewData["Message"] = "You are registered! Good Job " + login + "!";
                    return View("Index");
                }
                else
                {
                    ViewData["Message"] = "The login is taken by someone else";
                    return View("Regist", u);
                }
               
                
                
            }
            else
            {
                ViewData["Register"]="no";
                ViewData["Message"]="Somethink wrong! Bad password, remember about your Regex.";
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
            if(u.MinLength > u.MaxLength || u.MaxLength < u.MinDigits || u.MaxLength < u.MinLowercase || u.MaxLength < u.MinSpecialSigns || u.MaxLength < u.MinUppercase)
            {
                ViewData["Message"] = "Error";
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

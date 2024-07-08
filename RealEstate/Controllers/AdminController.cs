using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using RealEstate.Attributes;
using RealEstate.Models;
using RealEstate.Models.Entities;

namespace RealEstate.Controllers
{
    
    public class AdminController : BaseController
    {
        MyDatabase myDb = new MyDatabase();
        [HttpGet]
        public IActionResult Login()
        {
            return View(new Account());
        }

        [HttpPost]
        public IActionResult Login(Account account)
        {
            List<Account> accounts = myDb.Accounts.ToList();

            foreach (Account acc in accounts)
            {
                if (acc.Username == account.Username)
                {
                    if (BCrypt.Net.BCrypt.Verify(account.Password, acc.Password))
                    {
                        this.HttpContext.Session.SetString("userId", acc.Id.ToString());
                        return RedirectToAction("Services");
                    }
                }
            }
            account.Password = "";
            return View(account);
        }

        [HttpGet]
        public IActionResult Logout() 
        {
            this.HttpContext.Session.Remove("userId");
            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Services()
        {
            ViewBag.Services = myDb.Services.ToList();  

            return View();
        }
    }
}

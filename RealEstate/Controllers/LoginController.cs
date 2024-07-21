using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Models.Entities;

namespace RealEstate.Controllers
{
    public class LoginController : BaseController
    {
        MyDatabase myDb = new MyDatabase();

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Account());
        }

        [HttpPost]
        public IActionResult Index(Account account)
        {
            List<Account> accounts = myDb.Accounts.ToList();

            foreach (Account acc in accounts)
            {
                if (acc.Username == account.Username)
                {
                    if (acc.Disabled == true)                     
                    {
                        ViewBag.Disabled = acc.Disabled;
                        break;
                    }
                    if (BCrypt.Net.BCrypt.Verify(account.Password, acc.Password))
                    {
                        this.HttpContext.Session.SetString("userId", acc.Id.ToString());                        
                        return RedirectToAction("Services","Admin");                        
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
            this.HttpContext.Session.Remove("superior");
            return RedirectToAction("Index");
        }
    }
}

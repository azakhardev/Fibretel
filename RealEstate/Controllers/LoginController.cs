using Fibretel.Models;
using Fibretel.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fibretel.Controllers
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
                        HttpContext.Session.SetString("userId", acc.Id.ToString());

                        Log log = Logger.CreateLog(acc.Username, "Vstup do systému", "Uživatel se přihlásil do systému");

                        myDb.Logs.Add(log);
                        myDb.SaveChanges();

                        return RedirectToAction("Services", "Admin");
                    }
                }
            }
            account.Password = "";
            return View(account);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("superior");

            Log log = Logger.CreateLog(ViewBag.LoggedAs, "Opuštění systému", "Uživatel se odhlásil ze systému");
            myDb.Logs.Add(log);

            myDb.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

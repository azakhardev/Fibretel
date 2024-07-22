using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Digests;
using RealEstate.Attributes;
using RealEstate.Models;
using RealEstate.Models.Dto;
using RealEstate.Models.Entities;

namespace RealEstate.Controllers
{
    [SuperiorAuthorize]
    public class SuperiorAdminController : BaseController
    {
        MyDatabase myDb = new MyDatabase();

        [HttpGet]
        public IActionResult AccountsManagement()
        {
            int id = this.ViewBag.UserId;
            ViewBag.Accounts = myDb.Accounts.Where(x => x.Id != id).ToList();
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            CreateAccountDto acc = new CreateAccountDto();
            return View(acc);
        }

        [HttpPost]
        public IActionResult CreateAccount(CreateAccountDto acc)
        {
            if (acc.Password2 != acc.Account.Password)
            {
                this.ModelState.AddModelError("Password2", "Hesla se neshodují");
                return View(acc);
            }

            if (!ModelState.IsValid)
                return View(acc);

            acc.Account.Password = BCrypt.Net.BCrypt.HashPassword(acc.Account.Password);
            myDb.Accounts.Add(acc.Account);

            Log log = Logger.CreateLog(ViewBag.LoggedAs, "Správá účtů", $"Vytvořen účet {acc.Account.Username} pro zaměstnance {acc.Account.Name} {acc.Account.Surname}");
            myDb.Logs.Add(log);

            myDb.SaveChanges();

            return RedirectToAction("Success");
        }


        [HttpGet]
        public IActionResult ChangeRole(int id)
        {
            Account acc = myDb.Accounts.Find(id);            
            Log log = new Log();

            if (acc.Superior)
                log = Logger.CreateLog(ViewBag.LoggedAs, "Správá účtů", $"Účet {acc.Username} byl ponížen");
            else
                log = Logger.CreateLog(ViewBag.LoggedAs, "Správá účtů", $"Účet {acc.Username} byl povýšen");

            myDb.Accounts.Find(id).Superior = !acc.Superior;
            myDb.Logs.Add(log);
            myDb.SaveChanges();

            return RedirectToAction("AccountsManagement");
        }

        [HttpGet]
        public IActionResult ChangeAvilability(int id)
        {
            Account acc = myDb.Accounts.Find(id);
            Log log = new Log();

            if (acc.Disabled)
                log = Logger.CreateLog(ViewBag.LoggedAs, "Správá účtů", $"Účet {acc.Username} byl aktivován");
            else
                log = Logger.CreateLog(ViewBag.LoggedAs, "Správá účtů", $"Účet {acc.Username} byl deaktivován");

            myDb.Accounts.Find(id).Disabled = !acc.Disabled;
            myDb.Logs.Add(log);
            myDb.SaveChanges();

            return RedirectToAction("AccountsManagement");
        }

        [HttpGet]
        public IActionResult DeleteAccount(int id)
        {
            Account acc = myDb.Accounts.Find(id);
            myDb.Accounts.Remove(acc);

            Log log = Logger.CreateLog(ViewBag.LoggedAs, "Správá účtů", $"Smazán účet uživatele {acc.Username} - {acc.Name} {acc.Surname}");
            myDb.Logs.Add(log);

            myDb.SaveChanges();

            return RedirectToAction("AccountsManagement");
        }

        [HttpGet]
        public IActionResult Logs()
        {
            List<Log> logs = myDb.Logs.ToList();
            logs.Reverse();
            ViewBag.Logs = logs;
            return View();
        }

        [HttpGet]
        public IActionResult ClearLogs()
        {
            myDb.Logs.RemoveRange(myDb.Logs.Where(x => true));

            Log log = Logger.CreateLog(ViewBag.LoggedAs, "Správa logů", "Byly odstraněny všechny logy");
            log.Id = 1;
            myDb.Logs.Add(log);            

            myDb.SaveChanges();
            return RedirectToAction("Logs");
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}

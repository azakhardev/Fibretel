using Microsoft.AspNetCore.Mvc;
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

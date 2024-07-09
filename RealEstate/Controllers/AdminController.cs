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

        [Authorize]
        [HttpGet]
        public IActionResult EditService(int id)
        {
            Service service = new Service();
            this.ViewBag.Action = "Add";

            if (id != 0)
            {
                service = myDb.Services.Find(id);
                this.ViewBag.Action = "Edit";
            }

            return View(service);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditService(Service service)
        {
            if (service.Id != null)
            {
                Service updatedService = this.myDb.Services.Find(service.Id);
                updatedService.Name = service.Name;
                updatedService.Description = service.Description;
                updatedService.Photo = service.Photo;                
            }
            else 
            {
                this.myDb.Services.Add(service);                
            }

            this.myDb.SaveChanges();

            return RedirectToAction("Services");
        }

        [Authorize]
        [HttpGet]
        public IActionResult DeleteService(int id)
        {
            this.myDb.Services.Remove(this.myDb.Services.Find(id));
            this.myDb.SaveChanges();

            return RedirectToAction("Services");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Requests()
        {
            ViewBag.Requests = myDb.Requests.ToList();

            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult ViewRequest(int id)
        {
            Request request = myDb.Requests.Find(id);

            return View(request);
        }

        [Authorize]
        [HttpGet]
        public IActionResult DeleteRequest(int id)
        {
            this.myDb.Requests.Remove(this.myDb.Requests.Find(id));
            this.myDb.SaveChanges();

            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult AccountSettings()
        {
            int id = this.ViewBag.UserId;
            Account acc = myDb.Accounts.Find(id);
            return View(acc);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AccountsManagement()
        {
            int id = this.ViewBag.UserId;
            ViewBag.Accounts = myDb.Accounts.Where(x => x.Id != id).ToList();
            return View();
        }
    }
}

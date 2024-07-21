using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using RealEstate.Attributes;
using RealEstate.Models;
using RealEstate.Models.Dto;
using RealEstate.Models.Entities;

namespace RealEstate.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        MyDatabase myDb = new MyDatabase();

        [HttpGet]
        public IActionResult Services()
        {
            ViewBag.Services = myDb.Services.ToList();

            return View();
        }

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

        [HttpPost]
        public IActionResult EditService(Service service)
        {
            if (service.Id != null)
            {
                Service updatedService = this.myDb.Services.Find(service.Id);
                updatedService.Name = service.Name;
                updatedService.SmallDescription = service.SmallDescription;
                updatedService.Description = service.Description;
                updatedService.Price = service.Price;
                updatedService.Photo = service.Photo;
            }
            else
            {
                this.myDb.Services.Add(service);
            }

            this.myDb.SaveChanges();

            return RedirectToAction("Services");
        }

        [HttpGet]
        public IActionResult DeleteService(int id)
        {
            this.myDb.Services.Remove(this.myDb.Services.Find(id));
            this.myDb.SaveChanges();

            return RedirectToAction("Services");
        }

        [HttpGet]
        public IActionResult Requests()
        {
            ViewBag.Requests = myDb.Requests.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult ViewRequest(int id)
        {
            Request request = myDb.Requests.Find(id);

            return View(request);
        }

        [HttpGet]
        public IActionResult DeleteRequest(int id)
        {
            this.myDb.Requests.Remove(this.myDb.Requests.Find(id));
            this.myDb.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult AccountSettings()
        {
            int id = this.ViewBag.UserId;
            Account acc = myDb.Accounts.Find(id);
            ViewBag.Action = "Update";
            return View(acc);
        }

        [HttpPost]
        public IActionResult AccountSettings(Account account)
        {
            Account updatedAccount = this.myDb.Accounts.Find(account.Id);

            if (!this.ModelState.IsValid)
                return View(account);

            updatedAccount.Username = account.Username;
            updatedAccount.Degree = account.Degree;
            updatedAccount.Name = account.Name;
            updatedAccount.Surname = account.Surname;
            updatedAccount.Email = account.Email;

            myDb.SaveChanges();

            return RedirectToAction("Success");
        }

        [HttpGet]
        public IActionResult PasswordChange()
        {
            NewPassword newPassword = new NewPassword();
            return View(newPassword);
        }

        [HttpPost]
        public IActionResult PasswordChange(NewPassword pswd)
        {
            int id = this.ViewBag.UserId;
            Account acc = myDb.Accounts.Find(id);
            if (BCrypt.Net.BCrypt.Verify(pswd.OldPassword, acc.Password))
            {
                if (pswd.NewPassword1 == pswd.NewPassword2)
                {
                    acc.Password = BCrypt.Net.BCrypt.HashPassword(pswd.NewPassword1);
                    myDb.SaveChanges();
                    return RedirectToAction("Success");
                }
                else 
                {
                    ModelState.AddModelError("NewPassword1", "Nová hesla se neshodují");
                }
            }
            else 
            {
                ModelState.AddModelError("OldPassword", "Staré heslo je neplatné");
            }

            pswd.NewPassword2 = "";
            return View(pswd);
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        private void AddLog(Log log)
        {
            myDb.Logs.Add(log);
            myDb.SaveChanges();
        }
    }
}

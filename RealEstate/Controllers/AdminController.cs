using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using RealEstate.Attributes;
using RealEstate.Models;
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
            return View(acc);
        }

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

        private void AddLog(Log log) 
        {
            myDb.Logs.Add(log);
            myDb.SaveChanges();
        }
    }
}

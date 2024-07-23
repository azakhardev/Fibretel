using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Org.BouncyCastle.Crypto.Generators;
using RealEstate.Attributes;
using RealEstate.Models;
using RealEstate.Models.Dto;
using RealEstate.Models.Entities;
using System.Net;


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
            List<Request> requests = myDb.Requests.Where(x => x.Answered == false).ToList();
            requests.AddRange(myDb.Requests.Where(x => x.Answered).ToList());
            ViewBag.Requests = requests;

            return View();
        }

        [HttpPost]
        public IActionResult AnswerRequest(Answer answer) 
        {
            try
            {
                SendEmail(answer.CustomerEmail, $"Logo - {answer.Subject}", answer.Text);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Fail", new { message = "Nastal problém s emailovou službou SMTP. Zákazníkovi zatím musíte odpovědět přes svůj email. Prosím, obraťte se na správce webu. " });   
            }

            myDb.Requests.Find(answer.RequestId).Answered = true;

            Log log = Logger.CreateLog(ViewBag.LoggedAs, "Správa poptávek", $"Odpovězeno na poptávku zákazníka {answer.CustomerEmail} zprávou {answer.Text}");
            myDb.Logs.Add(log);

            myDb.SaveChanges();    

            return RedirectToAction("Success", new { message = "Odpověď na poptávku byla odeslána" }); ;
        }

        [HttpGet]
        public IActionResult DeleteRequest(int id)
        {
            Request request = this.myDb.Requests.Find(id);
            this.myDb.Requests.Remove(request);

            Log log = Logger.CreateLog(ViewBag.LoggedAs, "Správa poptávek", $"Byla odstraněna poptávka od zákazníka {request.Email} na službu {request.Service}");
            this.myDb.Logs.Add(log);

            this.myDb.SaveChanges();

            return RedirectToAction("Requests");
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

            this.myDb.SaveChanges();

            return RedirectToAction("Success", new { message = "Změny účtu byly úspěšně uloženy" });
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
                    return RedirectToAction("Success", new { message = "Změna hesla proběhla úspěšně" });
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
        public IActionResult Success(string message)
        {
            this.ViewBag.Message = message; 
            return View();
        }

        [HttpGet]
        public IActionResult Fail(string message)
        {
            this.ViewBag.Message = message;
            return View();
        }

        private void SendEmail(string toEmail, string subject, string body) 
        {
            string fromEmail = "ahoj@domain.com"; //Setup account
            string fromPassword = ""; //Setup password

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            
            SmtpClient smtpClient = new SmtpClient("localhost", 25);
            smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
            smtpClient.EnableSsl = false; // Enable on launch

            smtpClient.Send(mail);
        }
    }
}

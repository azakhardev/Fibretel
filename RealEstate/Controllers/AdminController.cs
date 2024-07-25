using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Org.BouncyCastle.Crypto.Generators;
using Fibretel.Attributes;
using System.Net;
using Fibretel.Models.Dto;
using Fibretel.Models;
using Fibretel.Models.Entities;


namespace Fibretel.Controllers
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
            ServiceDto service = new ServiceDto();
            ViewBag.Action = "Add";

            if (id != 0)
            {
                service.Service = myDb.Services.Find(id);
                service.Photos = myDb.Photos.Where(x => x.ServiceId == id).ToList();
                ViewBag.Action = "Edit";
            }

            return View(service);
        }

        [HttpPost]
        public IActionResult EditService(ServiceDto serviceDto)
        {
            if (!ModelState.IsValid)
                return View(serviceDto);

            Log log = new Log();
            Service service = serviceDto.Service;

            Service updatedService = myDb.Services.Find(service.Id);
            if (updatedService != null)
            {
                updatedService.Name = service.Name;
                updatedService.SmallDescription = service.SmallDescription;
                updatedService.Description = service.Description;
                updatedService.Price = service.Price;
                updatedService.Photo = service.Photo;
                log = Logger.CreateLog(ViewBag.LoggedAs, "Správa služeb", $"Byla změněna služba {service.Name}");
            }
            else
            {
                myDb.Services.Add(service);
                log = Logger.CreateLog(ViewBag.LoggedAs, "Správa služeb", $"Byla vytvořena služba {service.Name}");
            }

            myDb.Logs.Add(log);

            if (serviceDto.Photos.Any())
                SavePhotos(serviceDto.Photos);

            myDb.SaveChanges();

            return RedirectToAction("Services");
        }

        [HttpGet]
        public IActionResult DeleteService(int id)
        {
            Service service = myDb.Services.Find(id);

            myDb.Services.Remove(service);

            Log log = Logger.CreateLog(ViewBag.LoggedAs, "Správa služeb", $"Byla odstraněna služba {service.Name}");
            myDb.Logs.Add(log);

            myDb.SaveChanges();

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
                SendEmail(answer.CustomerEmail, $"Fibretel - {answer.Subject}", answer.Text);
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
            Request request = myDb.Requests.Find(id);
            myDb.Requests.Remove(request);

            Log log = Logger.CreateLog(ViewBag.LoggedAs, "Správa poptávek", $"Byla odstraněna poptávka od zákazníka {request.Email} na službu {request.Service}");
            myDb.Logs.Add(log);

            myDb.SaveChanges();

            return RedirectToAction("Requests");
        }

        [HttpGet]
        public IActionResult AccountSettings()
        {
            int id = ViewBag.UserId;
            Account acc = myDb.Accounts.Find(id);
            ViewBag.Action = "Update";
            return View(acc);
        }

        [HttpPost]
        public IActionResult AccountSettings(Account account)
        {
            Account updatedAccount = myDb.Accounts.Find(account.Id);

            if (!ModelState.IsValid)
                return View(account);

            updatedAccount.Username = account.Username;
            updatedAccount.Degree = account.Degree;
            updatedAccount.Name = account.Name;
            updatedAccount.Surname = account.Surname;
            updatedAccount.Email = account.Email;

            myDb.SaveChanges();

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
            int id = ViewBag.UserId;
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
            ViewBag.Message = message;
            return View();
        }

        [HttpGet]
        public IActionResult Fail(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        private void SavePhotos(List<Photo> photos)
        {
            foreach (Photo photo in photos)
            {
                if (!myDb.Photos.Contains(photo))
                {
                    myDb.Photos.Add(photo);
                }
                else
                {
                    Photo editedPhoto = myDb.Photos.Find(photo.Id);
                    if (editedPhoto.Path != photo.Path)
                        editedPhoto.Path = photo.Path;
                    if (editedPhoto.Text != photo.Text)
                        editedPhoto.Text = photo.Text;
                }
            }
            myDb.SaveChanges();
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            string fromEmail = "customerinfo@fibretel.com"; //Setup account
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

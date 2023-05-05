using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace FinalProjectWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class OrdersController : Controller
    {
        readonly FinalDbContext db;

        public OrdersController(FinalDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var order = db.Orders.Include(p => p.Order).Include(p => p.Product).Where(p => p.DeletedDate == null).ToList();

            return View(order);
        }

        public IActionResult Approve(int id)
        {
            var order = db.Orders.Include(p=>p.Product).FirstOrDefault(p => p.Id == id);
            order.approved = true;
            db.Orders.Update(order);
            db.SaveChanges();
            var gender = db.Genders.FirstOrDefault(p => p.Id == order.Product.GenderId);
            var orderinfo = db.Order.FirstOrDefault(p => p.Id == order.OrderId);
            var builder = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var host = configuration["Gmail:Host"];
            var port = int.Parse(configuration["Gmail:Port"]);
            var username = configuration["Gmail:Username"];
            var password = configuration["Gmail:Password"];
            var displayName = configuration["Gmail:DisplayName"];

            var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);


            MailMessage mail = new MailMessage(new MailAddress(username, displayName), new MailAddress(orderinfo.Email));
            mail.Subject = displayName;



            mail.Body = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Document</title>
    <style>

h3, h5, h4, h6, h1, h2{{
    display: inline;
    font-family:Georgia, 'Times New Roman', Times, sans-serif;
    
}}
h3{{
    font-weight: 600;
}}
h4{{
    font-weight: 500;
}}
</style>
</head>
<body>
    <table>
        <tbody>
          <tr>
            <td rowspan=""3"" width=""500"">
                <img src=""http://createsite.website/assets/img/{order.Product.ImagePath}"" alt=""watch"" width=""100%"">
            </td>
            <tr>
                <td><h3>Name:</h3><h4> {order.Product.Name}</h4></td>
            </tr>

            <tr>
                <td><h3>Gender:</h3><h4> {gender.Name}</h4></td>
            </tr>
            <tr>
                <td><h1>Price:</h1><h2> {order.Total}$</h2></td>
            </tr>
          </tr>
          <tr >
            <td colspan=""2"" width=""500"">
                <p>
                Dear {orderinfo.FirstName}, kindly be informed that your request is confirmed. Your tracking number will be sent within 24 hours.
                Thank you for choosing us!
                
               <br /> {db.SiteInfos.FirstOrDefault().Name} team.
            </p>
        </td>
        </tr>
        </tbody>
    </table>
</body>
</html>";


            mail.IsBodyHtml = true;



            SmtpClient smtpClient = new SmtpClient(host, port);
            smtpClient.Credentials = new NetworkCredential(username, password);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);



            return RedirectToAction("index");
        }

        public IActionResult Detail(int id)
        {
            var vm = new OrderViewModel();

            vm.Orders = db.Orders.Include(p => p.Order).Include(p => p.Product).FirstOrDefault(p => p.Id == id);
            vm.Color = db.Color.ToList();

            return View(vm);
        }

        public IActionResult Delete(int id)
        {
            var order = db.Orders.FirstOrDefault(p => p.Id == id);

            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}

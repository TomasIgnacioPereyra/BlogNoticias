using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogNoticias.Models;
using System.Net.Mail;
using System.Net;

namespace BlogNoticias.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Entrada()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(EnvioDeMail en)
        {
            ViewData["Message"] = "";
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(en.Email);// email del formulario
                                                         
                    msz.To.Add("correo2@yahoo.com"); //correo que recive el mensaje
                    msz.Subject = "nuevo contacto";// asunto del mensaje

                    msz.IsBodyHtml = true;// habilito formato html para que quede flama el correo
                    msz.Body = $@"
                               Email:{en.Email}<br>
                               Nombre:{en.Name}<br>
                               Nuemero telefonico:{en.Phone}<br><hr>
                               Consulta:{en.Message}
                               <img src={"http://www.theclinic.cl/wp-content/uploads/2016/10/negro11.jpg "} />
                               ";// concatenacion del formulario contact
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com"; //smtp varia depende del correo

                    smtp.Port = 587;// puerto varia depende el correo

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("correo@gmail.com", "xxxxxxx");// correo que va a enviar el mensaje y su contraseña gmail

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewData["Message"] = "GRACIAS POR CONTACTARSE"; // envio un mensaje en el header de la vista 

                    
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $"  {ex.Message}";
                }
            }



            return View();
        }


        public IActionResult Contact()
        {
           

            return View();
        }





        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    }
}

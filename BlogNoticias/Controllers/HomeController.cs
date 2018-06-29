using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogNoticias.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogNoticias.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _iconfiguration;    

        private readonly BlogNoticiasContext _context;

        public HomeController(BlogNoticiasContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;

        }

        // GET: Publicacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Publicacion.ToListAsync());
        }


        public async Task<IActionResult> Entrada(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicacion
                .SingleOrDefaultAsync(m => m.Id == id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }

        //public IActionResult Index()
        //{
        //    List<Publicacion> l = r.GetLastEntries();


        //    return View();
        //}

        public IActionResult About()
        {

            return View();
        }
        //[HttpGet]
        //public IActionResult Entrada()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Contact(EnvioDeMail en)
        {
          


            ViewData["Message"] = "";
            if (ModelState.IsValid)
            {
                try
                {

                    var To = _iconfiguration["To"];
                    var From = _iconfiguration["From"];
                    var Password = _iconfiguration["Password"];
                    var Puerto = Convert.ToInt32(_iconfiguration["Puerto"]);
                    var SMTP= _iconfiguration["smtp"];
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(en.Email);// email del formulario

                    msz.To.Add(To); //correo que recive el mensaje
                    msz.Subject = "nuevo contacto";// asunto del mensaje

                    msz.IsBodyHtml = true;// habilito formato html para que quede flama el correo
                    msz.Body = $@"
                               <img src={"https://voluntasvincit.com/wp-content/uploads/2018/02/contacto.png "} />

                               <strong>Email: </strong>{en.Email}<br>
                               <strong>Nombre: </strong>{en.Name}<br>
                               <strong>Telefono: </strong>{en.Phone}<br><hr>
                               {en.Message}
                               <img src={"https://pgroene.files.wordpress.com/2018/02/asp-net-core-logo-1.png?w=664"}/>
                               ";// concatenacion del formulario contact
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = SMTP; //smtp varia depende del correo

                    smtp.Port =Puerto ; // puerto varia depende el correo

                    smtp.Credentials = new System.Net.NetworkCredential(From,Password);// correo que va a enviar el mensaje y su contraseña gmail

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewData["Message"] = "GRACIAS POR CONTACTARSE "; // envio un mensaje en el header de la vista 

                    
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $"  {ex.Message}";
                }
            }



            return View();
        }
        public IActionResult Panel()
        {
            return View();
        }

        // POST: Publicacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Panel([Bind("Id,Autor,Cuerpo,Fecha,Subtitulo,Titulo")] Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publicacion);
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

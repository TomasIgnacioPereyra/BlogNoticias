using BlogNoticias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNoticias.Data
{
    public class DbInitializer
    {
        public static void Initialize(BlogNoticiasContext context)
        {
            context.Database.EnsureCreated();

            if (context.Publicacion.Any())
            {
                return;
            }
            var publicacion = new Publicacion[]
            {
                new Publicacion{Titulo="curso de .NET",Subtitulo="c#, core 2, javascript",Autor="Mauro Icardi",Fecha=new DateTime(2018,06,10),Cuerpo="mi primer blog"},
                new Publicacion{Titulo="curso 111mil",Subtitulo="java",Autor="Mauro Icardi",Fecha=new DateTime(2018,06,10),Cuerpo="Mi primer blog"}

            };
            foreach (Publicacion c in publicacion)
            {
                context.Publicacion.Add(c);
            }
            context.SaveChanges();
        }
    }
}

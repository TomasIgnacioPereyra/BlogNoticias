using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogNoticias.Models
{
    public class BlogNoticiasContext : DbContext
    {
        public BlogNoticiasContext (DbContextOptions<BlogNoticiasContext> options)
            : base(options)
        {
        }

        public DbSet<BlogNoticias.Models.Publicacion> Publicacion { get; set; }
    }
}

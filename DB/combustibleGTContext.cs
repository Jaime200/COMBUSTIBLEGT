using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace DB
{
    public class combustibleGTContext : DbContext
    {
       public DbSet<ENTIDADES.BOMBA> BOMBAS { get; set; }
        public DbSet<ENTIDADES.DESPACHO> DESPACHOS { get; set; }
        public DbSet<ENTIDADES.TIPO_COMBUSTIBLE> TIPOS_COMBUSTIBLE { get; set; }
        public DbSet<ENTIDADES.VEHICULO> VEHICULO { get; set; }

        public combustibleGTContext(DbContextOptions<combustibleGTContext> options) : base(options) { }
    }
}

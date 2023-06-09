﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using appPetech.Models;

namespace appPetech.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

        public DbSet<Contacto> DataContactos { get; set; }

        public DbSet<Producto> DataProductos { get; set; }

        public DbSet<Cart> DataCart {get;set;}

        public DbSet<Pedido> DataPedido {get;set;}

        public DbSet<DetallePedido> DataDetallePedido {get;set;}

         public DbSet<Delivery> DataDelivery {get;set;}

}

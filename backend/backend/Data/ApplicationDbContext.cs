﻿using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<User> Users { get; set; } 

        public DbSet<Order> Orders {  get; set; }
    }
}

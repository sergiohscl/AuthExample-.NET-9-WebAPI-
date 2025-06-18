using System;
using Microsoft.EntityFrameworkCore;
using AuthExample.Models;

namespace AuthExample.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
    
    public DbSet<User> Users { get; set; }
}

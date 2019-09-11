using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace street.Models
{
public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) 
        : base(options)
    {
        
    }

    public DbSet<ReportItem> Reports { get; set; }
    public DbSet<AuthToken> Tokens { get; set; }
    public DbSet<User> Users { get; set; }
}
}

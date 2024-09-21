using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL
{
    public class UnitOfWork : DbContext
    {
        public UnitOfWork(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Students> Students { get; set; }
    }
}   

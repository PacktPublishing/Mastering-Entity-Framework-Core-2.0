using MasteringEFCore.MultiTenancy.Starter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.Data
{
    public class BlogFilesContext : DbContext
    {
        public BlogFilesContext(DbContextOptions<BlogFilesContext> options)
            : base(options)
        {

        }
        public DbSet<File> Files { get; set; }
    }
}

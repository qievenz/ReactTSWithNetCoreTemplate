﻿using Microsoft.EntityFrameworkCore;

namespace ReactTSWithNetCoreTemplate.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<ReactTSWithNetCoreTemplateTableName> ReactTSWithNetCoreTemplateTableNames { get; set; }
    }
}

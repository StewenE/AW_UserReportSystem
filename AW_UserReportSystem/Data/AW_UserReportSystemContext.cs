using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AW_UserReportSystem.Models;

namespace AW_UserReportSystem.Data
{
    public class AW_UserReportSystemContext : DbContext
    {
        public AW_UserReportSystemContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Report> Report { get; set; }

    }
}

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
        public AW_UserReportSystemContext (DbContextOptions<AW_UserReportSystemContext> options)
            : base(options)
        {
        }

        public DbSet<AW_UserReportSystem.Models.Report> Report { get; set; } = default!;
    }
}

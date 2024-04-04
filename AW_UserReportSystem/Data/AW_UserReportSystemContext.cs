using Microsoft.EntityFrameworkCore;
using Domain;

namespace AW_UserReportSystem.Data
{
    public class AW_UserReportSystemContext : DbContext
    {
        public AW_UserReportSystemContext(DbContextOptions options) : base(options) {
        }
        public DbSet<Report> Report { get; set; }

    }
}

﻿using AW_UserReportSystem.Data;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace AW_UserReportSystem.Models;
public static class SeedData {
    public static void Initialize(IServiceProvider serviceProvider) {
        using(var context = new AW_UserReportSystemContext(
            serviceProvider.GetRequiredService<DbContextOptions<AW_UserReportSystemContext>>())) {
            if(context.Report.Any()) {
                return;   
            }
            context.Report.AddRange(
                new Report {
                    Name = "Report1",
                    Description = "Description1",
                    SubmitDate = DateTime.Today,
                    SolveByDate = DateTime.Today,                   
                },
                new Report {
                    Name = "Report2",
                    Description = "Description2",
                    SubmitDate = DateTime.Today,
                    SolveByDate = DateTime.Today,
                }
            );
            context.SaveChanges();
        }
    }
}
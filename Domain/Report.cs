﻿using Domain;
using System.ComponentModel.DataAnnotations;

namespace AW_UserReportSystem.Models {
    public class Report : Entity {
        public string? Name { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime SubmitDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime SolveByDate { get; set; }

    }
}
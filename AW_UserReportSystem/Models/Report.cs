﻿
using System.ComponentModel.DataAnnotations;

namespace AW_UserReportSystem.Models {
    public class Report : Entity {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Name { get; set; }
		[DataType(DataType.MultilineText)]
		public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubmitDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SolveByDate { get; set; }
    }
}

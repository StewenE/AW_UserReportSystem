using System.ComponentModel.DataAnnotations;

namespace AW_UserReportSystem.Models {
    public class Report {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime SubmitDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime SolveByDate { get; set; }

    }
}

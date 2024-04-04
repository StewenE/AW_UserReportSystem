using System.ComponentModel.DataAnnotations;

namespace Domain {
    public sealed class Report : Entity {
        public string? Name { get; set; }
		[DataType(DataType.MultilineText)]
		public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubmitDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SolveByDate { get; set; }
    }
}

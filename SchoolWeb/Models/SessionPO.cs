namespace SchoolWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SessionPO
    {
        [Required]
        public int SessionID { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Date is not valid format.")]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time, ErrorMessage = "Time is not valid format.")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [DataType(DataType.Time, ErrorMessage = "Time is not valid format.")]
        public TimeSpan EndTime { get; set; }

        [Required]
        public int ClassID { get; set; }

        [Required]
        public int SubjectID { get; set; }

        public string Notes { get; set; }

        public string Homework { get; set; }
    }
}
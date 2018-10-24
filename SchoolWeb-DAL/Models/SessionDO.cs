namespace SchoolWeb_DAL.Models
{
    using System;

    public class SessionDO
    {
        public int SessionID { get; set; }
        
        public DateTime Date { get; set; }
        
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int ClassID { get; set; }
        
        public int SubjectID { get; set; }

        public string Notes { get; set; }

        public string Homework { get; set; }
    }
}

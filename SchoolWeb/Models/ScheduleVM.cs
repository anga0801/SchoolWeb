namespace SchoolWeb.Models
{
    using System.Collections.Generic;

    public class ScheduleVM
    {
        public List<SessionPO> Sessions { get; set; }
        public List<ClassPO> Classes { get; set; }

        public ScheduleVM()
        {
            Sessions = new List<SessionPO>();
            Classes = new List<ClassPO>();
        }
    }
}
namespace SchoolWeb.Models
{
    using System.Collections.Generic;

    public class SessionDetailsVM
    {
        public SessionPO Session { get; set; }
        public ClassPO ClassPO { get; set; }
        public SubjectPO Subject { get; set; }
        public List<SubjectPO> Subjects { get; set; }
        public List<ClassPO> Classes { get; set; }

        public SessionDetailsVM()
        {
            Session = new SessionPO();
            ClassPO = new ClassPO();
            Subject = new SubjectPO();
            Subjects = new List<SubjectPO>();
            Classes = new List<ClassPO>();
        }
    }
}
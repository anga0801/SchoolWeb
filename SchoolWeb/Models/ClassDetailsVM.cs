namespace SchoolWeb.Models
{
    using System.Collections.Generic;

    public class ClassDetailsVM
    {
        public ClassPO Class { get; set; }
        public UserPO ClassTeacher { get; set; }
        public UserPO ClassPresident { get; set; }
        public List<UserPO> Students { get; set; }

        public ClassDetailsVM()
        {
            Class = new ClassPO();
            ClassTeacher = new UserPO();
            ClassPresident = new UserPO();
            Students = new List<UserPO>();
        }
    }
}
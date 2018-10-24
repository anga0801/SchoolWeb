namespace SchoolWeb_DAL.Models
{
    using System;

    public class ClassDO
    {
        public int ClassID { get; set; }
        
        public Int16 Grade { get; set; }
        
        public string Group { get; set; }

        public string Name { get; set; }

        public string PhotoPath { get; set; }
        
        public int? ClassTeacher { get; set; }

        public int? ClassPresident { get; set; }

        public string About { get; set; }
    }
}

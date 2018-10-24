namespace SchoolWeb_DAL.Models
{
    using System;

    public class SubjectDO
    {
        public int SubjectID { get; set; }
        
        public string Category { get; set; }
        
        public string Name { get; set; }
        
        public Int16 Grade { get; set; }

        public string Material { get; set; }

        public string Description { get; set; }
        
        public int TeacherID { get; set; }
    }
}

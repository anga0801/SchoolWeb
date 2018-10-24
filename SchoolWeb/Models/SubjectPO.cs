namespace SchoolWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SubjectPO
    {
        [Required]
        public int SubjectID { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Int16 Grade { get; set; }

        public string Material { get; set; }

        public string Description { get; set; }

        [Required]
        public int TeacherID { get; set; }
    }
}
namespace SchoolWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ClassPO
    {
        [Required]
        public int ClassID { get; set; }

        [Required]
        public Int16 Grade { get; set; }

        [Required]
        public string Group { get; set; }

        public string Name { get; set; }

        public string PhotoPath { get; set; }

        [Display(Name = "Class Teacher")]
        public int? ClassTeacher { get; set; }

        [Display(Name = "Class President")]
        public int? ClassPresident { get; set; }

        public string About { get; set; }
    }
}
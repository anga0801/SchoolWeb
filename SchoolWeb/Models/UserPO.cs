namespace SchoolWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserPO
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Nickname { get; set; }

        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Date of Birth is not valid format.")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Hired Date")]
        [DataType(DataType.Date, ErrorMessage = "Hired Date is not valid format.")]
        public DateTime? HiredDate { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail address not is valid format.")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not valid format.")]
        public string Phone { get; set; }

        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }

        [Display(Name = "Photo Path")]
        public string PhotoPath { get; set; }

        public string About { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int? ChildID { get; set; }

        [Required]
        public int RoleID { get; set; }

        public int? ClassID { get; set; }
    }
}
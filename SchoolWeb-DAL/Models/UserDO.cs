namespace SchoolWeb_DAL.Models
{
    using System;

    public class UserDO
    {
        public int UserID { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Nickname { get; set; }
        
        public string Title { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public DateTime? HiredDate { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public string HomeAddress { get; set; }
        
        public string PhotoPath { get; set; }
        
        public string About { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public int? ChildID { get; set; }
        
        public int RoleID { get; set; }
        
        public int? ClassID { get; set; }
    }
}

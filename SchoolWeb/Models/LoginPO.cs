namespace SchoolWeb.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginPO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
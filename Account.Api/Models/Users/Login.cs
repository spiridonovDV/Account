using System.ComponentModel.DataAnnotations;


namespace Account.Api.Models.Users
{
    public class Login
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

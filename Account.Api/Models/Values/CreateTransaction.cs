using System.ComponentModel.DataAnnotations;


namespace Account.Api.Models.Values
{
    public class CreateTransaction
    {
        [Required]
        public string UserFromId { get; set; }

        [Required]
        public string UserToId { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}

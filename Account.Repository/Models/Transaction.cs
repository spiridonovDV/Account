using System;


namespace Account.Repository.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public string UserFromId { get; set; }
        public User UserFrom { get; set; }

        public string UserToId { get; set; }
        public User UserTo { get; set; }

        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}

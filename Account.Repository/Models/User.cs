using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;


namespace Account.Repository.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Transaction> TransactionsIn { get; set; }
        public IEnumerable<Transaction> TransactionsOut { get; set; }

        public decimal Balance { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Account.Repository.Models;


namespace Account.Repository
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Transaction>().HasKey(tr => tr.Id);
            builder.Entity<Transaction>().HasOne(tr => tr.UserFrom)
                                         .WithMany(u => u.TransactionsOut)
                                         .HasForeignKey(tr => tr.UserFromId);

            builder.Entity<Transaction>().HasOne(tr => tr.UserTo)
                                         .WithMany(u => u.TransactionsIn)
                                         .HasForeignKey(tr => tr.UserToId);
        }
    }
}

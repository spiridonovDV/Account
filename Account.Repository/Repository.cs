using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Account.Repository.Models;


namespace Account.Repository
{
    public class Repository
    {
        private readonly DbContextOptions<AppDbContext> dbOptions;

        public Repository(DbContextOptions<AppDbContext> option) => dbOptions = option;

        public void CreateUser(User user)
        {
            using (var db = new AppDbContext(dbOptions))
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public Transaction CreateTransaction(string userFromId, string userToId, decimal amount)
        {
            if (userFromId == userToId)
            {
                throw new Exception("Вы не можете отправить перевод себе.");
            }

            using (var db = new AppDbContext(dbOptions))
            {
                var dbTransaction = db.Database.BeginTransaction();
                try
                {
                    var userFrom = db.Users.Find(userFromId) ?? throw new Exception("Отправитель не найден.");
                    var userTo = db.Users.Find(userToId) ?? throw new Exception("Получатель не найден.");

                    userFrom.Balance -= amount;
                    userTo.Balance += amount;
                    if (userFrom.Balance < 0) throw new Exception("Не хватает средств для перевода.");

                    var transaction = new Transaction
                    {
                        Amount = amount,
                        Balance = userFrom.Balance,
                        DateTime = DateTime.Now,
                        UserFrom = userFrom,
                        UserTo = userTo,
                    };
                    db.Transactions.Add(transaction);

                    db.SaveChanges();
                    dbTransaction.Commit();
                    return transaction;
                }
                catch (Exception err)
                {
                    dbTransaction.Rollback();
                    throw err;
                }
            }
        }

        public User GetUser(string userId)
        {
            using (var db = new AppDbContext(dbOptions))
            {
                return db.Users.Where(u => u.Id == userId)
                               .Include(u => u.TransactionsIn)
                               .Include(u => u.TransactionsOut)
                               .FirstOrDefault()
                       ?? throw new Exception("Пользователь не найден.");
            }
        }
    }
}

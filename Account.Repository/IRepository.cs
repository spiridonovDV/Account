using Account.Repository.Models;


namespace Account.Repository
{
    public interface IRepository
    {
        Transaction CreateTransaction(string userFromId, string userToId, decimal amount);
    }
}

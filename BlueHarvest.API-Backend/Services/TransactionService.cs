using static BlueHarvestAPI.Transaction;

namespace BlueHarvestAPI
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public Transaction CreateTransaction(double amount, TransactionType type)
        {
            Transaction transaction = new Transaction
            {
                Amount = amount,
                Date = DateTime.UtcNow,
                Type = type
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction;
        }
    }
}

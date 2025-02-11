using static BlueHarvestAPI.Transaction;

namespace BlueHarvestAPI
{
    public interface ITransactionService
    {
        Transaction CreateTransaction(double amount, TransactionType type);
    }
}

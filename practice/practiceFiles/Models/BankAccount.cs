using practice.practiceFiles.CustomExceptions;

namespace practice.practiceFiles.Models;

public class BankAccount
{
    public string AccountNumber { get; set; } = default!;
    public decimal Balance { get; set; }

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
            throw new InsufficientFundsException(AccountNumber, Balance, amount);

        Balance -= amount;
    }
}

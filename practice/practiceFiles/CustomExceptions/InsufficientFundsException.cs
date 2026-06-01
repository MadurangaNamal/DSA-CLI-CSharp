namespace practice.practiceFiles.CustomExceptions;

public class InsufficientFundsException : Exception
{
    public string AccountNumber { get; } = string.Empty;
    public decimal CurrentBalance { get; } = 0m;
    public decimal RequestedAmount { get; } = 0m;

    public InsufficientFundsException() : base() { }

    public InsufficientFundsException(string message) : base(message) { }

    public InsufficientFundsException(string message, Exception innerException)
        : base(message, innerException) { }

    public InsufficientFundsException(string accountNumber, decimal currentBalance, decimal requestedAmount)
        : base($"Insufficient funds in account {accountNumber}. " +
            $"Current balance: {currentBalance:C}, Requested: {requestedAmount:C}")
    {
        AccountNumber = accountNumber;
        CurrentBalance = currentBalance;
        RequestedAmount = requestedAmount;
    }
}

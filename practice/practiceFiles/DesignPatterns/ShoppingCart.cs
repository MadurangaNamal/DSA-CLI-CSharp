namespace practice.practiceFiles.DesignPatterns;

public class ShoppingCart
{
    private IPaymentStrategy? _paymentStrategy;

    public void SetPaymentStrategy(IPaymentStrategy strategy) => _paymentStrategy = strategy;

    public void Checkout(decimal amount)
    {
        if (_paymentStrategy is null)
            throw new InvalidOperationException("Payment strategy is not set. Call SetPaymentStrategy before Checkout.");

        _paymentStrategy.Pay(amount);
    }
}

using System;
using System.Threading.Tasks;

var account = new Account(1000);
var tasks = new Task[100];
for (int i = 0; i < tasks.Length; i++)
{
    tasks[i] = Task.Run(() => Update(account));
}
await Task.WhenAll(tasks);
Console.WriteLine($"Account's balance is {account.GetBalance()}");
// Output:
// Account's balance is 2000


void Update(Account account)
{
    decimal[] amounts = new[] { 0m, 2m, -3m, 6m, -2m, -1m, 8m, -5m, 11m, -6m };
    foreach (var amount in amounts)
    {
        if (amount >= 0)
        {
            account.Credit(amount);
        }
        else
        {
            account.Debit(Math.Abs(amount));
        }
    }
}

public class Account
{
    // Use `object` in versions earlier than C# 13
    private readonly object _balanceLock = new();
    private decimal _balance;

    public Account(decimal initialBalance) => _balance = initialBalance;

    public decimal Debit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The debit amount cannot be negative.");
        }

        decimal appliedAmount = 0;
        lock (_balanceLock)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                appliedAmount = amount;
            }
        }
        return appliedAmount;
    }

    public void Credit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The credit amount cannot be negative.");
        }

        lock (_balanceLock)
        {
            _balance += amount;
        }
    }

    public decimal GetBalance()
    {
        lock (_balanceLock)
        {
            return _balance;
        }
    }
}




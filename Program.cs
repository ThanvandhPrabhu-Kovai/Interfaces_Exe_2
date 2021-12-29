using System;

interface IAccount
{
    double calculateInterests(int numberOfMonths);
}

abstract class AAccount{ 
    public abstract double calculateInterests(int numberOfMonths);
}

class Account
{
    public int Customer { get; set; }
    public int TypeOfAccount { get; set; }
    public double Balance { get; set; }
    public double InterestRatePerMonth { get; set; }
    public Account()
    {
        Customer = 0;
        TypeOfAccount = 0;
        Balance = 0;
        InterestRatePerMonth = 0;
    }

    public Account(int customer, double balance, int typeOfAccount, double interestRatePerMonth)
    {
        Customer = customer;
        TypeOfAccount = typeOfAccount;
        Balance = balance;
        InterestRatePerMonth = interestRatePerMonth;
    }

    public double depositAmount(double balance, double amount)
    {
        double result = 0;
        result = balance + amount;
        Balance = result;
        return result;
    }

}

class DepositAccount : Account, IAccount
{

    public Account account { get; set; }

    public DepositAccount()
    {
        account = new Account();
    }

    public DepositAccount(Account accountDetails)
    {
        account = accountDetails;
    }

    public double withdrawMoney(double balance, double amount)
    {
        double result = 0;
        double difference = balance - amount;
        bool isPossible = difference >= 0;
        if (isPossible)
        {
            result = difference;
        }
        account.Balance = result;
        return result;
    }

    public double calculateInterests(int numberOfMonths)
    {
        double result = 0;
        bool isQualifiedForNoInterest = account.Balance > 0 && account.Balance < 1000;
        if (isQualifiedForNoInterest)
        {
            result = 0;
        }
        else
        {
            result = numberOfMonths * account.InterestRatePerMonth;
        }
        return result;
    }

}

class LoanAccount : Account, IAccount
{
    public Account account { get; set; }
    public int NatureOfCustomer { get; set; }

    public LoanAccount()
    {
        account = new Account();
        NatureOfCustomer = 0;
    }

    public LoanAccount(Account accountDetails, int natureOfCustomer)
    {
        account = accountDetails;
        NatureOfCustomer = natureOfCustomer - 1;
    }

    public double calculateInterests(int numberOfMonths)
    {
        double result = 0;

        bool isIndividualAndWithin3Months = numberOfMonths <= 3 && NatureOfCustomer == 0;
        bool isCompanyAndWithinTwoMonths = numberOfMonths <= 2 && NatureOfCustomer == 1;

        if (isIndividualAndWithin3Months || isCompanyAndWithinTwoMonths)
        {
            result = 0;
        }
        else
        {
            result = numberOfMonths * account.InterestRatePerMonth;
        }
        return result;
    }
}

class MortgageAccount : Account, IAccount
{
    public Account account { get; set; }
    public int NatureOfCustomer { get; set; }

    public MortgageAccount()
    {
        account = new Account();
        NatureOfCustomer = 0;
    }

    public MortgageAccount(Account accountDetails, int natureOfCustomer)
    {
        account = accountDetails;
        NatureOfCustomer = natureOfCustomer;
    }

    public double calculateInterests(int numberOfMonths)
    {
        double result = 0;
        bool isIndividualAndWithin6Months = numberOfMonths <= 6 && NatureOfCustomer == 0;
        bool isCompanyAndWithin12Months = numberOfMonths <= 12 && NatureOfCustomer == 1;
        double calculatedValue = numberOfMonths * account.InterestRatePerMonth;
        if (isCompanyAndWithin12Months)
        {
            result = calculatedValue / 2;
        }
        else
        {
            result = calculatedValue;
        }
        return result;
    }
}


class Program
{
    static void Main(string[] args)
    {
        showOutput("\n\nCalculate interests for a given period (in Months)");

        showOutput("\n\nPlease input Customer ID : ");
        int customerId = getUserInputAsInt();

        showOutput("\n\nPlease specify the balance amount in your account : ");
        double balance = getUserInputAsDouble();

        showOutput("\n\nSelect account type from the following with providing respective serial numbers as inputs");
        showOutput("\n\n1-Deposit Accounts");
        showOutput("\n2-Loan Accounts");
        showOutput("\n3-Mortgage Accounts");
        showOutput("\n\nPlease select an account type from above options : ");
        int typeOfAccount = getUserInputAsInt();

        showOutput("\n\nPlease specify the interest rate per month : ");
        double interestRatePerMonth = getUserInputAsDouble();

        Account account = new Account(customer: customerId, balance: balance, typeOfAccount: typeOfAccount, interestRatePerMonth: interestRatePerMonth);

        showOutput("\n\nPlease the number of months you have to calculate interest for : ");
        int numberOfMonths = getUserInputAsInt();

        double calculatedInterest = 0.0;

        switch (typeOfAccount)
        {
            case 1:
                DepositAccount depositAccount = new DepositAccount(account);
                calculatedInterest = depositAccount.calculateInterests(numberOfMonths: numberOfMonths);
                break;
            case 2:
                showOutput("\n\nSelect your nature from the following with providing respective serial numbers as inputs");
                showOutput("\n\n1-Individuals");
                showOutput("\n2-Company");

                showOutput("\n\nPlease select your nature : ");
                int natureOfCustomer = getUserInputAsInt();

                LoanAccount loanAccount = new LoanAccount(accountDetails: account, natureOfCustomer: natureOfCustomer);
                calculatedInterest = loanAccount.calculateInterests(numberOfMonths: numberOfMonths);
                break;
            case 3:
                MortgageAccount mortgageAccount = new MortgageAccount();
                calculatedInterest = mortgageAccount.calculateInterests(numberOfMonths: numberOfMonths);
                break;
            default:
                break;
        }

        string output = $"\n\n {calculatedInterest}% is the interest for the given period, {numberOfMonths} months";
        showOutput(output);

        getUserInput();
    }

    static string getUserInput() => Console.ReadLine();

    static int getUserInputAsInt() => Convert.ToInt32(getUserInput());

    static double getUserInputAsDouble() => Convert.ToDouble(getUserInput());

    static void showOutput(string input) => Console.Write(input);

}


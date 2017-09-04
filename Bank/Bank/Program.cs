using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank {
    class BankAccount {
        // Private variables for storing the account balance and interest rate
        private const double defaultRate = 4.1;
        private const double defaultBalance = 0;

        private double accBalance;
        private double intRate;

        /// <summary>
        /// Creates a new bank account with no starting balance and the default
        /// interest rate.
        /// </summary>
        public BankAccount() {
            accBalance = defaultBalance;
            intRate = defaultRate;
        }

        /// <summary>
        /// Creates a new bank account with a starting balance and the default
        /// interest rate.
        /// </summary>
        /// <param name="startingBalance">The starting balance</param>
        public BankAccount(double startingBalance) {
            accBalance = startingBalance;
            intRate = defaultRate;
        }

        /// <summary>
        /// Creates a new bank account with a starting balance and interest
        /// rate.
        /// </summary>
        /// <param name="startingBalance">The starting balance</param>
        /// <param name="interestRate">The interest rate (in percentage)</param>
        public BankAccount(double startingBalance, double interestRate) {
            accBalance = startingBalance;
            intRate = interestRate;
        }

        /// <summary>
        /// Reduce the balance of the bank account by 'amount' and return true.
        /// If there are insufficient funds in the account, the balance does not
        /// change and false is returned instead. 
        /// </summary>
        /// <param name="amount">The amount of money to deduct from the account
        /// </param>
        /// <returns>True if funds were deducted from the account, and false
        /// otherwise</returns>
        public bool Withdraw(double amount) {
            double totes = (accBalance - amount);
            if (totes < 0) {
                return false;
            } else { 
            accBalance = (accBalance - amount);
            return true;
            }
        }

        /// <summary>
        /// Increase the balance of the account by 'amount'
        /// </summary>
        /// <param name="amount">The amount to increase the balance by</param>
        public void Deposit(double amount) {
            accBalance = accBalance + amount;
        }

        /// <summary>
        /// Returns the total balance currently in the account.
        /// </summary>
        /// <returns>The total balance currently in the account</returns>
        public double QueryBalance() {
            return accBalance;
        }

        /// <summary>
        /// Sets the account's interest rate to the rate provided
        /// </summary>
        /// <param name="interestRate">The interest rate for this account (%)
        /// </param>
        public void SetInterestRate(double interestRate) {
            intRate = interestRate;
        }

        /// <summary>
        /// Returns the account's interest rate
        /// </summary>
        /// <returns>The percentage interest rate of this account</returns>
        public double GetInterestRate() {
            return intRate;
        }

        /// <summary>
        /// Calculates the interest on the current account balance and adds it
        /// to the account
        /// </summary>
        public void AddInterest() {
            accBalance = accBalance + (accBalance * (intRate / 100));
        }
    }
    class Program {
        static void Main(string[] args) {
            BankAccount myAccount = new BankAccount(200);
            myAccount.Deposit(1000);
            myAccount.AddInterest();
            Console.WriteLine("My current bank balance is $ {0:0.00}\n",
              myAccount.QueryBalance());
            myAccount.Withdraw(20000);
            myAccount.QueryBalance();
            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}

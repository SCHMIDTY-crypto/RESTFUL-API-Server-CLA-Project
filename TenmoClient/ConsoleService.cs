using System;
using System.Collections.Generic;
using TenmoClient.Data;
using TenmoClient.Data.Transfers;

namespace TenmoClient
{
    public class ConsoleService
    {
        /// <summary>
        /// Prompts for transfer ID to view, approve, or reject
        /// </summary>
        /// <param name="action">String to print in prompt. Expected values are "Approve" or "Reject" or "View"</param>
        /// <returns>ID of transfers to view, approve, or reject</returns>

        //public void PrintBalance(List<Account> accounts)
        //{
        //    Console.WriteLine("--------------------------------------------");
        //    Console.WriteLine("Balance");
        //    Console.WriteLine("--------------------------------------------");
        //    foreach (Account account in accounts)
        //    {
        //        Console.WriteLine("Your current balance is: ");
        //    }
        //}

        public int PromptForTransferID(string action)
        {
            Console.WriteLine("");
            Console.Write("Please enter transfer ID to " + action + " (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int auctionId))
            {
                Console.WriteLine("Invalid input. Only input a number.");
                return 0;
            }
            else
            {
                return auctionId;
            }
        }

        public LoginUser PromptForLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            string password = GetPasswordFromConsole("Password: ");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }

        public void PrintTransfer(List<Transfer> transfer)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Transfers");
            Console.WriteLine("--------------------------------------------");
            foreach(Transfer transfers in transfer)
            {
                Console.WriteLine($"Transfer ID: {transfers.TransferId}\nFrom: {transfers.AccountFrom}\nTo: {transfers.AccountTo}\nAmount: ${transfers.Amount}");
            }
            
        }

        public void PrintTransferDetails(Transfer details)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Transfer Details");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine($"Transfer ID: {details.TransferId}\nFrom: {details.AccountFrom}\nTo: {details.AccountTo}\nType:{details.TransferTypeId}\n"+
                $"Status: {details.TransferStatusId}\nAmount: ${details.Amount}");
            
        }
        public void PrintUsers(List<Users> user)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Users");
            Console.WriteLine("ID         Name");
            Console.WriteLine("--------------------------------------------");
            foreach (Users users in user)
            {
                Console.WriteLine($"{users.UserId}          {users.Username}");
            }

        }
    }
}

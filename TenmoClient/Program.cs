using System;
using System.Collections.Generic;
using System.Diagnostics;
using TenmoClient.Data;
using TenmoClient.Data.Transfers;

namespace TenmoClient
{
    class Program
    {
        private static readonly APIService api = new APIService();
        private static readonly ConsoleService consoleService = new ConsoleService();
        private static readonly AuthService authService = new AuthService();

        static void Main(string[] args)
        {
            Run();
        }
        private static void Run()
        {
            int loginRegister = -1;
            while (loginRegister != 1 && loginRegister != 2)
            {
                Console.WriteLine("Welcome to TEnmo!");
                Console.WriteLine("1: Login");
                Console.WriteLine("2: Register");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out loginRegister))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else if (loginRegister == 1)
                {
                    while (!UserService.IsLoggedIn()) //will keep looping until user is logged in
                    {
                        LoginUser loginUser = consoleService.PromptForLogin();
                        API_User user = authService.Login(loginUser);
                        if (user != null)
                        {
                            UserService.SetLogin(user);
                        }
                    }
                }
                else if (loginRegister == 2)
                {
                    bool isRegistered = false;
                    while (!isRegistered) //will keep looping until user is registered
                    {
                        LoginUser registerUser = consoleService.PromptForLogin();
                        isRegistered = authService.Register(registerUser);
                        if (isRegistered)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Registration successful. You can now log in.");
                            loginRegister = -1; //reset outer loop to allow choice for login
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }

            MenuSelection();
        }

        private static void MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers");
                Console.WriteLine("3: View your pending requests");
                Console.WriteLine("4: Send TE bucks");
                Console.WriteLine("5: Request TE bucks");
                Console.WriteLine("6: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else if (menuSelection == 1)
                {
                    decimal balance = api.GetBalance();
                    if (balance >= 0) 
                    {
                        Console.WriteLine($"Your account balance is: ${balance}");
                    }
                    
                }
                else if (menuSelection == 2)
                {
                    List<Transfer> transferHistory = api.GetTransfers();

                    if (transferHistory != null && transferHistory.Count > 0)
                    {
                        consoleService.PrintTransfer(transferHistory);
                    }
                    Console.WriteLine($"Please enter transfer ID to view details (0 to cancel):");
                    if (!int.TryParse(Console.ReadLine(), out int transferSelection))
                    {
                        Console.WriteLine("Invalid input. Please enter only a number.");
                    }
                    if (transferSelection > 0)
                    {
                        Transfer transferDetails = api.GetTransferDetails(transferSelection);
                        consoleService.PrintTransferDetails(transferDetails);
                    }
                    else if (transferSelection == 0)
                    {
                        MenuSelection();
                    }

                    // Pulls list with all transfers relating to user THEN hase menu to look at details of each transfer  (5 & 6 From sample Screen)
                }
                else if (menuSelection == 3)
                {
                    // Is part of optional use cases ( Step 8 on ReadMe )
                }
                else if (menuSelection == 4)
                {
                    List<Users> users = api.GetUsers();
                    if (users != null && users.Count > 0)
                    {
                        consoleService.PrintUsers(users);
                    }

                    Console.WriteLine("Enter ID of user you are sending to(0 to cancel):");

                    if (!int.TryParse(Console.ReadLine(), out int userSelection))
                    {
                        Console.WriteLine("Invalid input. Please enter only a number.");
                    }

                    Console.WriteLine("Enter amount: ");

                    if (!decimal.TryParse(Console.ReadLine(), out decimal amountToSend))
                    {
                        Console.WriteLine("Invalid input. Please enter only a number.");
                    }
                    Transfer transfer = new Transfer
                    {
                        TransferTypeId = 2,
                        TransferStatusId = 2,
                        AccountFrom = UserService.GetUserId(),
                        AccountTo = userSelection,
                        Amount = amountToSend
                    };

                    api.UpdateUserBalance(transfer);
                    api.UpdateRecieverBalance(transfer);

                    if(api.TransferRequest(transfer))
                    {
                        Console.WriteLine("Ya boi got his money");
                    }
                    else
                    {
                        Console.WriteLine("YOU ARE A FAILURE!");
                    }
                }
                else if (menuSelection == 5)
                {
                    // Is part of option use cases ( Step 7 on the ReadMe )
                }
                else if (menuSelection == 6)
                {
                    Console.WriteLine("");
                    UserService.SetLogin(new API_User()); //wipe out previous login info
                    Run(); //return to entry point
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S00209029_CA3
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Console.OutputEncoding = Encoding.UTF8;
            char euroSymbol = '€';

            Console.WriteLine("Welcome to BlackJack!");
            Console.WriteLine("Please enter your starting balance:");
            int balanceInput = Int32.Parse(Console.ReadLine());

            Balance balance = new Balance(balanceInput);

            Console.Clear();
            int gameResult;
            
            while (true)
            {
                Console.WriteLine("Your balance is {0}{1}.",euroSymbol, balance.Funds);
                Console.WriteLine("Please enter your bet amount:");

                balanceInput = Int32.Parse(Console.ReadLine());

                //Checks player has enough funds to cover the bet.
                if (balanceInput <= balance.Funds)
                {
                    //Runs a round of the game, checks if the player won, lost, or tied the game and adjusts their balance accordingly.
                    gameResult = game.StartGame();
                    break;
                }

                Console.WriteLine("Insufficient funds.");
            }


            //Adds/subtracts funds from account as per the result of the game.
            balance = ManageBet(gameResult, balanceInput, balance);

            //Asks if player wants to play another round of the game.
            while (true)
            {

                //Checks if player's account is empty
                if(balance.Funds <= 0){
                    Console.Clear();
                    Console.WriteLine("You are bust!");
                    Console.WriteLine("Thank you for playing!");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine("Do you want to play again - y/n?");
                string input = Console.ReadLine();

                if (input.ToLower().Equals("y"))
                {
                    Console.Clear();

                    Console.WriteLine("Your balance is {0}{1}.",euroSymbol, balance.Funds);
                    Console.WriteLine("Please enter your bet amount:");

                    balanceInput = Int32.Parse(Console.ReadLine());

                    if (balanceInput <= balance.Funds)
                    {
                        gameResult = game.StartGame();
                        //Adds/subtracts funds from account as per the result of the game.
                        balance = ManageBet(gameResult, balanceInput, balance);
                    }

                    else
                    {
                        Console.WriteLine("Insufficient funds.");
                    } 

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Your final balance is {0}{1}.", euroSymbol, balance.Funds);
                    Console.WriteLine("Thank you for playing!");
                    Console.ReadLine();
                    break;
                }
            }
            

        }

        private static Balance ManageBet(int gameResult, int balanceInput, Balance balance)
        {
            char euroSymbol = '€';

            //If game returns a win
            if (gameResult == 1)
            {
                balance.AddFunds(balanceInput);
                Console.WriteLine("You won {0}{1}. Your balance is now {2}{3}.", euroSymbol, balanceInput, euroSymbol, balance.Funds);
                return balance;
            }
            //Higher payout if player won with a BlackJack starting hand (i.e. an ace and a 10-value card).
            else if (gameResult == 2)
            {
                int fundsToAdd = (int)Math.Round(balanceInput * 1.5);
                balance.AddFunds(fundsToAdd);
                Console.WriteLine("You won {0}{1}. Your balance is now {2}{3}.", euroSymbol, (fundsToAdd), euroSymbol, balance.Funds);
                return balance;
            }

            //If game returns a loss
            else if (gameResult == 0)
            {
                balance.SubtractFunds(balanceInput);
                Console.WriteLine("You lost {0}{1}. Your balance is now {2}{3}.", euroSymbol,balanceInput, euroSymbol,balance.Funds);
                return balance;
            }
        
        //If game returns a tie    
        Console.WriteLine("Tie: bet has been returned to you. Your balance is {0}{1}.",euroSymbol, balance.Funds);
        return balance;
            


        }
    }
}

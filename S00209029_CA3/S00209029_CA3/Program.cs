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

            game.StartGame();

            //Asks if player wants to play another round of the game.
            while (true)
            {
                Console.WriteLine("Do you want to play again - y/n?");
                string input = Console.ReadLine();

                if (input.ToLower().Equals("y"))
                {
                    Console.Clear();
                    game.StartGame();

                }
                else
                {
                    break;
                }
            }
            

        }
    }
}

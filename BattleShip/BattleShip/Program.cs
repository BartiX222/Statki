using System;
namespace BattleShip
{
    internal class Program
    {
        void MakeMove(Player player, int x, int y)
        {
            Console.WriteLine($"{player} makes a move at ({x}, {y})");
        }
        public static void Main(string[] args)
        {
            Player player1 = new Player();
            Player player2 = new Player();

            player1.SetupShips();
            player1.ownBoard.PrintBoard();
            Console.Clear();
            Console.WriteLine("Press any key to change player to player2...");
            Console.ReadLine();
            Console.Clear();
            player2.SetupShips();
            player2.ownBoard.PrintBoard();
            Console.Clear();
            Console.WriteLine("Press any key to change player to player1...");
            Console.ReadLine();
            Console.Clear();
            while (player1.hits < 20 && player2.hits < 20)
            {
                Console.Clear();
                player1.ownBoard.PrintBoard();
                player1.targetBoard.PrintBoard();
                player1.Shoot(player2);
                Console.WriteLine("Press any key to change player to player2...");
                Console.ReadLine();
                Console.Clear();
                if (player1.hits == 20) break;
                player2.ownBoard.PrintBoard();
                player2.targetBoard.PrintBoard();
                player2.Shoot(player1);
                Console.WriteLine("Press any key to change player to player1....");
                Console.ReadLine();
                Console.Clear();
            }

            if (player1.hits == 20)
            {
                Console.WriteLine("Player 1 wins!");
            }
            else
            {
                Console.WriteLine("Player 2 wins!");
            }

            Console.ReadLine();
        }
    }
}

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
            Console.WriteLine("Press any key to change player to player2...");
            Console.ReadLine();
            Console.Clear();

            player2.SetupShips();
            player2.ownBoard.PrintBoard();
            Console.WriteLine("Press any key to change player to player1...");
            Console.ReadLine();
            Console.Clear();

            while (player1.hits < 20 && player2.hits < 20)
            {
                Console.Clear();
                Console.WriteLine("Player 1's turn. Press any key to continue...");
                Console.ReadLine();
                Console.Clear();

                player1.ownBoard.PrintBoard();
                player1.targetBoard.PrintBoard();
                bool hit = player1.Shoot(player2);
                while (hit)
                {
                    Console.WriteLine("Trafiony! Strzelaj ponownie.");
                    player1.ownBoard.PrintBoard();
                    player1.targetBoard.PrintBoard();
                    hit = player1.Shoot(player2);
                }
                if (player1.hits == 20) break;

                Console.WriteLine("Press any key to change player to player2...");
                Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Player 2's turn. Press any key to continue...");
                Console.ReadLine();
                Console.Clear();

                player2.ownBoard.PrintBoard();
                player2.targetBoard.PrintBoard();
                hit = player2.Shoot(player1);
                while (hit)
                {
                    Console.WriteLine("Trafiony! Strzelaj ponownie.");
                    player2.ownBoard.PrintBoard();
                    player2.targetBoard.PrintBoard();
                    hit = player2.Shoot(player1);
                }
                if (player2.hits == 20) break;

                Console.WriteLine("Press any key to change player to player1...");
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

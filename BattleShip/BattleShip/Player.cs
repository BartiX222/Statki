using System;

namespace BattleShip
{
    public class Player
    {
        public Ship[] ships = new Ship[10];
        public Board enemyBoard;
        public Board ownBoard;
        public Board targetBoard;
        public int hits = 0;
        public Player()
        {
            ownBoard = new Board();
            targetBoard = new Board();
            ownBoard.SetupBoard();
            targetBoard.SetupBoard();
        }

        public void SetupShips()
        {
            for (int i = 0; i < 4; i++)
            {
                ships[i] = new Ship(1);
                ownBoard.PlaceShip(ships[i]);
            }

            for (int i = 0; i < 3; i++)
            {
                ships[i + 4] = new Ship(2);
                ownBoard.PlaceShip(ships[i + 4]);
            }

            for (int i = 0; i < 2; i++)
            {
                ships[i + 7] = new Ship(3);
                ownBoard.PlaceShip(ships[i + 7]);
            }

            ships[9] = new Ship(4);
            ownBoard.PlaceShip(ships[9]);
        }

        public void Shoot(Player enemy)
        {
            Console.WriteLine("Tura ataku!");
            int[] cords = GetPlayerInput();
            if (enemy.ownBoard.board[cords[0], cords[1]] == 'S')
            {
                Console.WriteLine("Trafiony!");
                enemy.ownBoard.board[cords[0], cords[1]] = 'X';
                targetBoard.board[cords[0], cords[1]] = 'X';
                hits++;
            }
            else
            {
                Console.WriteLine("Pudlo!");
                targetBoard.board[cords[0], cords[1]] = 'O';
            }
        }
        public static int[] GetPlayerInput()
        {
            Console.WriteLine("Podaj pierwsza wspolrzedna (A-J): ");
            try
            {
                int y = Console.ReadLine().ToLower()[0] - 'a' + 1;
                Console.WriteLine("Podaj druga wspolrzedna (1-10): ");
                int x = int.Parse(Console.ReadLine());

                if (x > 10 || x < 1 || y > 10 || y < 1)
                {
                    Console.WriteLine("Podano niepoprawne dane. Sprobuj ponownie.");

                    return GetPlayerInput();
                }

                return new[] { x - 1, y - 1 };
            }
            catch (Exception e)
            {
                Console.WriteLine("Podano niepoprawne dane. Sprobuj ponownie.");
            }

            return GetPlayerInput();
        }
    }
}

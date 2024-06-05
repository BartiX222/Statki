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

        public bool Shoot(Player enemy)
        {
            Console.WriteLine("Tura ataku!");
            int[] cords = GetPlayerInput();
            if (enemy.ownBoard.board[cords[0], cords[1]] == 'S')
            {
                Console.WriteLine("Trafiony!");
                enemy.ownBoard.board[cords[0], cords[1]] = 'X';
                targetBoard.board[cords[0], cords[1]] = 'X';
                hits++;
                return true; // Trafienie
            }
            else
            {
                Console.WriteLine("Pudlo!");
                targetBoard.board[cords[0], cords[1]] = 'O';
                return false; // Pudło
            }
        }

        public static int[] GetPlayerInput()
        {
            Console.WriteLine("Podaj współrzędne (np. A1): ");
            string input = Console.ReadLine().ToUpper();

            if (input.Length < 2 || input.Length > 3)
            {
                Console.WriteLine("Podano niepoprawne dane. Sprobuj ponownie.");
                return GetPlayerInput();
            }

            try
            {
                int y = input[0] - 'A';
                int x = int.Parse(input.Substring(1)) - 1;

                if (x > 9 || x < 0 || y > 9 || y < 0)
                {
                    Console.WriteLine("Podano niepoprawne dane. Sprobuj ponownie.");
                    return GetPlayerInput();
                }

                return new[] { x, y };
            }
            catch (Exception)
            {
                Console.WriteLine("Podano niepoprawne dane. Sprobuj ponownie.");
                return GetPlayerInput();
            }
        }
    }
}

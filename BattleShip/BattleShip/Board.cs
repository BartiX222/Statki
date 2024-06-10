using System;

namespace BattleShip
{
    public class Board
    {
        public char[,] board = new char[10, 10];

        public void SetupBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = '~';
                }
            }
        }

        public void PrintBoard()
        {
            Console.Write("    A   B   C   D   E   F   G   H   I   J");
            for (int i = 0; i < 10; i++)
            {
                if (i + 1 != 10) Console.Write("\n" + (i + 1) + " | ");
                else Console.Write("\n" + (i + 1) + "| ");

                for (int j = 0; j < 10; j++)
                {
                    Console.Write(board[i, j] + " | ");
                }
            }
            Console.WriteLine("");
        }

        private bool IsPositionValid(int x, int y, int size, bool horizontal)
        {
            int startX = Math.Max(0, x - 1);
            int endX = Math.Min(9, horizontal ? x + 1 : x + size);
            int startY = Math.Max(0, y - 1);
            int endY = Math.Min(9, horizontal ? y + size : y + 1);

            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (board[i, j] == 'S')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void PlaceShip(Ship ship)
        {
            char[,] backup = board.Clone() as char[,];
            PrintBoard();
            Console.WriteLine($"Stawianie statku o dlugosci {ship.Size}");
            int[] cords = Player.GetPlayerInput();

            if (board[cords[0], cords[1]] == 'S')
            {
                Console.WriteLine("Na podanych wspolrzednych znajduje sie juz statek. Sprobuj ponownie.");
                PlaceShip(ship);
                return;
            }
            if (ship.Size == 1)
            {
                if (IsPositionValid(cords[0], cords[1], ship.Size, true))
                {
                    board[cords[0], cords[1]] = 'S';
                }
                else
                {
                    Console.WriteLine("Nie mozna postawic statku na sąsiadujących polach. Sprobuj ponownie.");
                    PlaceShip(ship);
                    return;
                }
            }
            else
            {
                Console.WriteLine("Podaj kierunek statku: Poziomo(H), Pionowo(V).");
                string direction = Console.ReadLine().ToLower();
                if (direction != "h" && direction != "v")
                {
                    Console.WriteLine("Podano niepoprawny kierunek. Sprobuj ponownie.");
                    PlaceShip(ship);
                    return;
                }
                if (direction == "h")
                {
                    if (cords[1] + ship.Size > 10)
                    {
                        Console.WriteLine("Nie mozna postawic statku poza plansza. Sprobuj ponownie.");
                        PlaceShip(ship);
                        return;
                    }
                    if (IsPositionValid(cords[0], cords[1], ship.Size, true))
                    {
                        for (int i = 0; i < ship.Size; i++)
                        {
                            board[cords[0], cords[1] + i] = 'S';
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nie mozna postawic statku na sąsiadujących polach. Sprobuj ponownie.");
                        PlaceShip(ship);
                        return;
                    }
                }
                else
                {
                    if (cords[0] + ship.Size > 10)
                    {
                        Console.WriteLine("Nie mozna postawic statku poza plansza. Sprobuj ponownie.");
                        PlaceShip(ship);
                        return;
                    }
                    if (IsPositionValid(cords[0], cords[1], ship.Size, false))
                    {
                        for (int i = 0; i < ship.Size; i++)
                        {
                            board[cords[0] + i, cords[1]] = 'S';
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nie mozna postawic statku na sąsiadujących polach. Sprobuj ponownie.");
                        PlaceShip(ship);
                        return;
                    }
                }
            }
        }
    }
}

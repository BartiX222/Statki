using System;
using System.Collections.Generic;
using System.Linq;

public class Board
{
    public const int Size = 10;
    public char[,] Grid { get; private set; }
    public List<Ship> Ships { get; }
    public static List<(int, int)> Hits { get; private set; }

    public Board()
    {
        Grid = new char[Size, Size];
        Ships = new List<Ship>();
        Hits = new List<(int, int)>();

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Grid[i, j] = '~';
            }
        }
    }

    public void PlaceShip(Ship ship, int row, int col, bool isHorizontal)
    {
        if (isHorizontal)
        {
            for (int i = 0; i < ship.Size; i++)
            {
                Grid[row, col + i] = 'S';
                ship.AddPosition(row, col + i);
            }
        }
        else
        {
            for (int i = 0; i < ship.Size; i++)
            {
                Grid[row + i, col] = 'S';
                ship.AddPosition(row + i, col);
            }
        }

        Ships.Add(ship);
    }

    public bool ReceiveAttack(int row, int col)
    {
        if (row < 0 || row >= Size || col < 0 || col >= Size)
        {
            throw new ArgumentException("Pole poza zakresem planszy.");
        }

        if (Hits.Contains((row, col)))
        {
            throw new InvalidOperationException("To pole zostało już zaatakowane.");
        }

        Hits.Add((row, col));
        if (Grid[row, col] == 'S')
        {
            Grid[row, col] = 'X';
            return true;
        }
        else
        {
            Grid[row, col] = 'O';
            return false;
        }
    }

    public void Display()
    {
        Console.Write("  ");
        for (int i = 1; i <= Size; i++)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        for (int i = 0; i < Size; i++)
        {
            Console.Write((char)('A' + i) + " ");
            for (int j = 0; j < Size; j++)
            {
                Console.Write(Grid[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void DisplayForOpponent()
    {
        Console.Write("  ");
        for (int i = 1; i <= Size; i++)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        for (int i = 0; i < Size; i++)
        {
            Console.Write((char)('A' + i) + " ");
            for (int j = 0; j < Size; j++)
            {
                if (Hits.Contains((i, j)))
                {
                    Console.Write(Grid[i, j] + " ");
                }
                else
                {
                    Console.Write("~ ");
                }
            }
            Console.WriteLine();
        }
    }

    public bool AllShipsSunk()
    {
        return Ships.All(ship => ship.IsSunk);
    }
}

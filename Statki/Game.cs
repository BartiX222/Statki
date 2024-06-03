using System;

public class Game
{
    private Board playerBoard;
    private Board computerBoard;
    private Random random;
    private int playerHits;
    private int playerMisses;
    private int computerHits;
    private int computerMisses;

    public Game()
    {
        playerBoard = new Board();
        computerBoard = new Board();
        random = new Random();
        playerHits = 0;
        playerMisses = 0;
        computerHits = 0;
        computerMisses = 0;
    }

    private void PlaceShips(Board board)
    {
        int[] shipSizes = { 5, 4, 3, 3, 2 };

        foreach (int size in shipSizes)
        {
            bool placed = false;
            while (!placed)
            {
                int row = random.Next(Board.Size);
                int col = random.Next(Board.Size);
                bool isHorizontal = random.Next(2) == 0;

                if (CanPlaceShip(board, size, row, col, isHorizontal))
                {
                    board.PlaceShip(new Ship(size), row, col, isHorizontal);
                    placed = true;
                }
            }
        }
    }

    private bool CanPlaceShip(Board board, int size, int row, int col, bool isHorizontal)
    {
        if (isHorizontal)
        {
            if (col + size > Board.Size) return false;
            for (int i = 0; i < size; i++)
            {
                if (board.Grid[row, col + i] == 'S' || !IsValidPosition(board, row, col + i)) return false;
            }
        }
        else
        {
            if (row + size > Board.Size) return false;
            for (int i = 0; i < size; i++)
            {
                if (board.Grid[row + i, col] == 'S' || !IsValidPosition(board, row + i, col)) return false;
            }
        }
        return true;
    }

    private bool IsValidPosition(Board board, int row, int col)
    {
        for (int i = Math.Max(0, row - 1); i <= Math.Min(Board.Size - 1, row + 1); i++)
        {
            for (int j = Math.Max(0, col - 1); j <= Math.Min(Board.Size - 1, col + 1); j++)
            {
                if (board.Grid[i, j] == 'S') return false;
            }
        }
        return true;
    }

    private void PlacePlayerShips()
    {
        int[] shipSizes = { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };

        foreach (int size in shipSizes)
        {
            bool placed = false;
            while (!placed)
            {
                playerBoard.Display();
                Console.WriteLine($"Podaj współrzędne dla statku o rozmiarze {size} (format: A1):");
                var input = Console.ReadLine();
                try
                {
                    var (row, col) = ParseCoordinates(input);
                    bool isHorizontal = true;

                    if (size > 1)
                    {
                        Console.WriteLine("Podaj orientację statku (h dla poziomej, v dla pionowej):");
                        isHorizontal = Console.ReadLine().ToLower() == "h";
                    }

                    if (CanPlaceShip(playerBoard, size, row, col, isHorizontal))
                    {
                        playerBoard.PlaceShip(new Ship(size), row, col, isHorizontal);
                        placed = true;
                    }
                    else
                    {
                        Console.WriteLine("Nie można umieścić statku w tym miejscu. Spróbuj ponownie.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    private (int, int) ParseCoordinates(string input)
    {
        if (input.Length < 2)
            throw new ArgumentException("Współrzędne zostały wpisane w złym formacie, proszę zapisać je w formacie \"A1\"!!!");

        char rowChar = input[0];
        if (!char.IsLetter(rowChar))
            throw new ArgumentException("Współrzędne zostały wpisane w złym formacie, proszę zapisać je w formacie \"A1\"!!!");

        rowChar = char.ToUpper(rowChar);
        int row = rowChar - 'A';

        if (!int.TryParse(input.Substring(1), out int col))
            throw new ArgumentException("Współrzędne zostały wpisane w złym formacie, proszę zapisać je w formacie \"A1\"!!!");

        col -= 1;

        if (row < 0 || row >= Board.Size || col < 0 || col >= Board.Size)
            throw new ArgumentException("Współrzędne poza zakresem planszy.");

        return (row, col);
    }

    public void Start()
    {
        Console.WriteLine("Ustaw swoje statki:");
        PlacePlayerShips();
        PlaceShips(computerBoard);

        while (true)
        {
            Console.WriteLine("Twoja plansza:");
            playerBoard.Display();
            Console.WriteLine("Plansza przeciwnika:");
            computerBoard.DisplayForOpponent();

            bool validAttack = false;
            while (!validAttack)
            {
                try
                {
                    Console.WriteLine("Podaj współrzędne do ataku (format: A1):");
                    var input = Console.ReadLine();
                    var (playerRow, playerCol) = ParseCoordinates(input);

                    bool playerHit = computerBoard.ReceiveAttack(playerRow, playerCol);
                    if (playerHit)
                    {
                        Console.WriteLine("Trafiony!");
                        playerHits++;
                    }
                    else
                    {
                        Console.WriteLine("Pudło!");
                        playerMisses++;
                    }
                    validAttack = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}. Spróbuj ponownie.");
                }
            }

            if (computerBoard.AllShipsSunk())
            {
                Console.WriteLine("Wygrałeś!");
                DisplayStatistics();
                break;
            }

            int computerRow = random.Next(Board.Size);
            int computerCol = random.Next(Board.Size);

            bool computerHit = playerBoard.ReceiveAttack(computerRow, computerCol);
            if (computerHit)
            {
                Console.WriteLine("Przeciwnik trafił!");
                computerHits++;
            }
            else
            {
                Console.WriteLine("Przeciwnik pudłował!");
                computerMisses++;
            }

            if (playerBoard.AllShipsSunk())
            {
                Console.WriteLine("Przegrałeś!");
                DisplayStatistics();
                break;
            }
        }
    }

    public void DisplayStatistics()
    {
        Console.WriteLine("Statystyki gry:");
        Console.WriteLine($"Twoje trafienia: {playerHits}");
        Console.WriteLine($"Twoje pudła: {playerMisses}");
        Console.WriteLine($"Trafienia przeciwnika: {computerHits}");
        Console.WriteLine($"Pudła przeciwnika: {computerMisses}");
    }
}

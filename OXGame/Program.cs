namespace OXGame
{
    internal class Program
    {
        int board_size;
        char[,] board;
        string?[] player = new string[2];
        char[] symbol = { 'O', 'X' };
        string? winner;
        Program()
        {
            Console.WriteLine("Enter the board size: (2,3,..)");
            board_size = Convert.ToUInt16(Console.ReadLine());
            board = new char[board_size, board_size];
            initBoard();
            setupPlayers();
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.play();
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        bool SeekHorizondalMatch()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                int OmatchCount = 0, XmatchCount = 0;
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == symbol[0])
                    {
                        OmatchCount++;
                    }
                    else if (board[i, j] == symbol[1])
                    {
                        XmatchCount++;
                    }
                }
                if (OmatchCount == board_size)
                {
                    winner = player[0];
                    return true;
                }
                else if (XmatchCount == board_size)
                {
                    winner = player[1];
                    return true;
                }
            }
            return false;
        }
        bool SeekVerticalMatch()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                int OmatchCount = 0, XmatchCount = 0;
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[j, i] == symbol[0])
                    {
                        OmatchCount++;
                    }
                    else if (board[j, i] == symbol[1])
                    {
                        XmatchCount++;
                    }
                }
                if (OmatchCount == board_size)
                {
                    winner = player[0];
                    return true;
                }
                else if (XmatchCount == board_size)
                {
                    winner = player[1];
                    return true;
                }
            }
            return false;
        }
        bool SeekDiagonalMatch()
        {
            int OmatchCount = 0, XmatchCount = 0;
            // Check right diagonal
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, i] == symbol[0])
                {
                    OmatchCount++;
                }
                else if (board[i, i] == symbol[1])
                {
                    XmatchCount++;
                }
            }
            if (OmatchCount == board_size)
            {
                winner = player[0];
                return true;
            }
            else if (XmatchCount == board_size)
            {
                winner = player[1];
                return true;
            }
            // Check left diagonal
            OmatchCount = XmatchCount = 0;
            int j = board.GetLength(0) - 1;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, j] == symbol[0])
                {
                    OmatchCount++;
                }
                else if (board[i, j] == symbol[1])
                {
                    XmatchCount++;
                }
                j--;
            }
            if (OmatchCount == board_size)
            {
                winner = player[0];
                return true;
            }
            else if (XmatchCount == board_size)
            {
                winner = player[1];
                return true;
            }
            return false;

        }
        bool hasMatch()
        {
            if (SeekHorizondalMatch() || SeekVerticalMatch() || SeekDiagonalMatch())
            {
                return true;
            }
            return false;
        }
        void play()
        {
            bool gotMatch = false, exhausted = false;
            while (true)
            {
                for (int i = 0; i < 2; i++)
                {
                    Console.Clear();
                    printBoard();
                    Console.WriteLine($"**{player[i]}**");
                    Console.WriteLine("Enter the x coords:");
                    int x = Convert.ToUInt16(Console.ReadLine());
                    x--;
                    Console.WriteLine("Enter the y coords:");
                    int y = Convert.ToUInt16(Console.ReadLine());
                    y--;
                    if (board[x, y] == '-')
                    {
                        board[x, y] = symbol[i];
                    }
                    else
                    {
                        continue;
                    }
                    if (hasMatch())
                    {
                        gotMatch = true;
                        break;
                    }
                    else if (is_exhausted())
                    {
                        exhausted = true;
                        break;
                    }
                }
                if (gotMatch)
                {
                    Console.Clear();
                    printBoard();
                    Console.WriteLine($"{winner} is the winner! Congrats!");
                    break;
                }
                else if (exhausted)
                {
                    Console.WriteLine("Match is draw!!");
                    break;
                }
            }
        }
        bool is_exhausted()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == '-')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        void setupPlayers()
        {
            Console.WriteLine("Enter the player 1 name:");
            player[0] = Console.ReadLine();
            Console.WriteLine("Enter the player 2 name:");
            player[1] = Console.ReadLine();
            Console.WriteLine($"{player[0]}: Do you take O? (yes/no)");
            string? choice = Console.ReadLine();
            if (choice != "yes")
            {
                string? temp = player[0];
                player[0] = player[1];
                player[1] = temp;
            }
        }
        void initBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = '-';
                }
            }
        }
        void printBoard()
        {
            printHorizondalIndex();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                // Print vertical index
                Console.Write(i + 1);
                // Print elements
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write("\t");
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
        }
        void printHorizondalIndex()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("\t");
                Console.Write(i + 1);
            }
            Console.WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Spectory
{
    /// <summary>
    /// Player info struct - playerName and playerSign
    /// </summary>
    public struct playerInfo
    {
        public String playerName;
        public char playerSign;
    };

    class Fourinarow
    {
        static void Main(string[] args)
        {
            char[,] board = new char[9, 10];
            int full = 0;
            int win = 0;
            int dropChoice;
            // Declare and initialize struct objects.
            playerInfo playerOne = new playerInfo();
            playerInfo playerTwo = new playerInfo();
            Console.WriteLine("Game started");
            Console.WriteLine("Player One please enter your name: ");
            playerOne.playerName = Console.ReadLine();
            playerOne.playerSign = 'X';
            Console.WriteLine("Player Two please enter your name: ");
            playerTwo.playerName = Console.ReadLine();
            playerTwo.playerSign = 'O';
            DisplayBoard(board); //Display the board after player choice
            do
            {
                //Player One start to play first
                dropChoice = PlayerDrop(board, playerOne);
                CheckBellow(board, playerOne, dropChoice);
                DisplayBoard(board);
                //Connected four check
                win = CheckFour(board, playerOne);
                //Player Two drop chice
                dropChoice = PlayerDrop(board, playerTwo);
                CheckBellow(board, playerTwo, dropChoice);
                DisplayBoard(board);
                win = CheckFour(board, playerTwo);
                //If there is a win
                if (win == 1)
                {
                    PlayerWin(playerTwo);
                }
                //Check if the board is full that means if we don't have space to drop our choice and game finished with draw
                full = FullBoard(board);
                if (full == 7)
                {
                    Console.WriteLine("The board is full, it is a draw");
                    Console.ReadLine();
                }
            } while (full != 1);
        }

        /// <summary>
        /// This function returns the user choice - refer for both users. 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="activePlayer"></param>
        /// <returns></returns>
        static int PlayerDrop(char[,] board, playerInfo activePlayer)
        {
            int dropChoice;
            Console.WriteLine(activePlayer.playerName + "'s Turn ");
            do
            {
                Console.WriteLine("Please enter a number between 1 and 7: ");
                dropChoice = Convert.ToInt32(Console.ReadLine());
            } while (dropChoice < 1 || dropChoice > 7);
            while (board[1, dropChoice] == 'X' || board[1, dropChoice] == 'O')
            {
                Console.WriteLine("That column is full, please enter a new column: ");
                dropChoice = Convert.ToInt32(Console.ReadLine());
            }
            return dropChoice;
        }
        /// <summary>
        /// This function assign user drop choice to the board
        /// </summary>
        /// <param name="board"></param>
        /// <param name="activePlayer"></param>
        /// <param name="dropChoice"></param>
        static void CheckBellow(char[,] board, playerInfo activePlayer, int dropChoice)
        {
            int length = 6, turn = 0;
            do
            {
                if (board[length, dropChoice] != 'X' && board[length, dropChoice] != 'O')
                {
                    board[length, dropChoice] = activePlayer.playerSign;
                    turn = 1;
                }
                else
                    --length;
            } while (turn != 1);
        }

        /// <summary>
        /// This function display the board in the console after every user drop choice
        /// </summary>
        /// <param name="board"></param>
        static void DisplayBoard(char[,] board)
        {
            int rows = 6, columns = 7, i, ix;
            for (i = 1; i <= rows; i++)
            {
                Console.Write("|");
                for (ix = 1; ix <= columns; ix++)
                {
                    if (board[i, ix] != 'X' && board[i, ix] != 'O')
                        board[i, ix] = '#';
                    Console.Write(board[i, ix]);
                }
                Console.Write("| \n");
            }
        }
        /// <summary>
        /// This function check if there is a win according to 4 win searches: vertical, horizontal, right and down, right and up.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="activePlayer"></param>
        /// <returns></returns>
        static int CheckFour(char[,] board, playerInfo activePlayer)
        {
            //Player sign that is X or O according to the active player
            char sign = activePlayer.playerSign;
            int win = 0;
            for (int i = 8; i >= 1; --i)
            {
                for (int ix = 9; ix >= 1; --ix)
                {
                    //Connected 4 Horizontal search
                    if (board[i, ix] == sign &&
                        board[i, ix + 1] == sign &&
                        board[i, ix + 2] == sign &&
                        board[i, ix + 3] == sign)
                    {
                        win = 1;
                    }

                    //Connected 4 Vertical search
                    if (board[i, ix] == sign &&
                        board[i - 1, ix] == sign &&
                        board[i - 2, ix] == sign &&
                        board[i - 3, ix] == sign)
                    {
                        win = 1;
                    }

                    //Connected 4 diagonal
                    if (board[i, ix] == sign &&
                        board[i - 1, ix - 1] == sign &&
                        board[i - 2, ix - 2] == sign &&
                        board[i - 3, ix - 3] == sign)
                    {
                        win = 1;
                    }

                    //Connected 4 diagonal
                    if (board[i, ix] == sign &&
                        board[i - 1, ix + 1] == sign &&
                        board[i - 2, ix + 2] == sign &&
                        board[i - 3, ix + 3] == sign)
                    {
                        win = 1;
                    }

                }
            }
            return win;
        }

        /// <summary>
        /// This function checks if the board is full after every drop choice
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        static int FullBoard(char[,] board)
        {
            int full = 0;
            for (int i = 1; i <= 7; ++i)
            {
                if (board[1, i] != '#')
                    ++full;
            }
            return full;
        }

        /// <summary>
        /// This function Prints the winning player name
        /// </summary>
        /// <param name="activePlayer"></param>
        static void PlayerWin(playerInfo activePlayer)
        {
            Console.WriteLine(activePlayer.playerName + " Win the connected 4 game!");
        }
    }
}
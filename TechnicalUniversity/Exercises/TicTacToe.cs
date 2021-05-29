using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTTT
{
    class TicTacToe
    {
        public const int PLAYER1 = 1;
        public const int PLAYER2 = 2;

        public static Int32 Winner(
            Int32[,] board)
        {
            var c1 = board[0, 0] & board[1, 0] & board[2, 0];
            var c2 = board[0, 1] & board[1, 1] & board[2, 1];
            var c3 = board[0, 2] & board[1, 2] & board[2, 2];
            var l1 = board[0, 0] & board[0, 1] & board[0, 2];
            var l2 = board[1, 0] & board[1, 1] & board[1, 2];
            var l3 = board[2, 0] & board[2, 1] & board[2, 2];
            var d1 = board[0, 0] & board[1, 1] & board[2, 2];
            var d2 = board[0, 2] & board[1, 1] & board[2, 0];

            return
                c1 == 1 ||
                c2 == 1 ||
                c3 == 1 ||
                l1 == 1 ||
                l2 == 1 ||
                l3 == 1 ||
                d1 == 1 ||
                d2 == 1 ? 1 :
                c1 == 2 ||
                c2 == 2 ||
                c3 == 2 ||
                l1 == 2 ||
                l2 == 2 ||
                l3 == 2 ||
                d1 == 2 ||
                d2 == 2 ? 2 :
                0;
        }

        public class Node
        {
            public List<Node> Children = new List<Node>();
            public int[,] Board = new int[3, 3];
            public int? Value;
        }

        public static void Grow(
            Node node,
            int player,
            int maximizer)
        {
            var winner = Winner(node.Board);
            if (winner != 0)
            {
                node.Value = winner == maximizer
                    ? 1
                    : -1;

                return;
            }

            for (int i = 0; i < node.Board.GetLength(0); i++)
            {
                for (int k = 0; k < node.Board.GetLength(1); k++)
                {
                    if (node.Board[i, k] == 0)
                    {
                        var newChild = new Node();

                        Array.Copy(node.Board, newChild.Board, node.Board.Length);

                        newChild.Board[i, k] = player;

                        node.Children.Add(newChild);

                        Grow(
                            newChild,
                            player == PLAYER1
                                ? PLAYER2
                                : PLAYER1,
                            maximizer);

                        if (node.Value == null ||
                            (player == maximizer && node.Value < newChild.Value) ||
                            (player != maximizer && node.Value > newChild.Value))
                        {
                            node.Value = newChild.Value;
                        }
                    }
                }
            }

            if (node.Value == null)
            {
                node.Value = 0;
            }
        }

        public static void PrintBoard(
            int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int k = 0; k < board.GetLength(1); k++)
                {
                    Console.Write(
                        (board[i, k] == PLAYER1 ? "X" :
                            board[i, k] == PLAYER2 ? "O" :
                            (i * 3 + k + 1).ToString()) + " ");
                }

                Console.WriteLine();
            }
        }

        static void Run()
        {
            var root = new Node();

            Grow(
                root,
                PLAYER1,
                PLAYER1);

            var currentNode = root;
            var currentPlayer = PLAYER1;
            while (currentNode != null)
            {
                Console.Clear();
                PrintBoard(currentNode.Board);

                if (currentPlayer == PLAYER1) //human
                {
                    var move = Console.ReadKey().KeyChar - '1';

                    Node moveNode = null;
                    foreach (var child in currentNode.Children)
                    {
                        for (int i = 0; i < currentNode.Board.GetLength(0) && moveNode == null; i++)
                        {
                            for (int k = 0; k < currentNode.Board.GetLength(1) && moveNode == null; k++)
                            {
                                if (currentNode.Board[i, k] != child.Board[i, k] &&
                                    i * 3 + k == move)
                                {
                                    moveNode = child;
                                }
                            }
                        }

                        if (moveNode != null)
                        {
                            currentNode = moveNode;
                            break;
                        }
                    }
                }
                else //pc
                {
                    Node minNode = null;
                    foreach (var child in currentNode.Children)
                    {
                        if (minNode == null ||
                            minNode.Value > child.Value)
                        {
                            minNode = child;
                        }
                    }

                    currentNode = minNode;
                }

                currentPlayer = currentPlayer == PLAYER1
                    ? PLAYER2
                    : PLAYER1;
            }
        }
    }
}
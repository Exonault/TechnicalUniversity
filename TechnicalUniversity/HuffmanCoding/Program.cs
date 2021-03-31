using System;
using System.Collections.Generic;

namespace HuffmanCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter text");
            string text = Console.ReadLine();

            List<Node> nodes = new List<Node>();

            for (int i = 0; i < text.Length; i++)
            {
                Node node = null;
                for (int j = 0; j < nodes.Count; j++)
                {
                    if (nodes[j].symbol == text[i])
                    {
                        node = nodes[j];
                        break;
                    }
                }

                if (node == null)
                {
                    nodes.Add(new Node
                    {
                        symbol = text[i],
                        frequency = 1
                    });
                }

                else
                {
                    node.frequency++;
                }
            }

            List<Node> SortedNodes = new List<Node>();

            foreach (var node in SortedNodes)
            {
                InsertNode(SortedNodes, node);
            }

            while (SortedNodes.Count > 1)
            {
                Node node = new Node
                {
                    leftNode = SortedNodes[0],
                    rightNode = SortedNodes[1],
                    frequency = SortedNodes[0].frequency + SortedNodes[1].frequency
                };

                SortedNodes.RemoveAt(0);
                SortedNodes.RemoveAt(0);

                InsertNode(SortedNodes, node);
            }

            Console.WriteLine("Encoded text");
            for (int i = 0; i < text.Length; i++)
            {
                Console.WriteLine(Encode(SortedNodes[0],text[i]));
            }
        }

        public static string Encode(Node node, char symbol)
        {
            if (node == null)
            {
                return null;
            }

            if (node.symbol == symbol)
            {
                return "";
            }

            string code;
            if ((code = Encode(node.leftNode, symbol)) != null)
            {
                return code + "0";
            }
            else
            {
                if ((code = Encode(node.rightNode, symbol)) != null)
                {
                    return code + "1";
                }
            }

            return null;
        }

        public static void InsertNode(List<Node> nodes, Node node)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].frequency > node.frequency)
                {
                    nodes.Insert(i, node);
                    return;
                }
            }

            nodes.Add(node);
        }
    }
}
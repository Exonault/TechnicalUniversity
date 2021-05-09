using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Exercises
{
    public class FamilyTree
    {
        private Node root = null;

        public void Run()
        {
            Node root = new Node()
            {
                name = 'a',
            };

            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("a - Add Parent ;f- Find parent; q - Quit");

                switch (Console.ReadKey().KeyChar)
                {
                    case 'a':
                    {
                        Console.WriteLine("Enter name");
                        char name = Console.ReadKey().KeyChar;
                        Console.WriteLine();

                        Node node = FindByName(root, name);

                        Console.WriteLine("Enter father name:");
                        char fatherName = Console.ReadKey().KeyChar;
                        Console.WriteLine();

                        Node fatherNode =
                            FindByName(root, fatherName)
                            ??
                            new Node()
                            {
                                name = fatherName
                            };


                        Console.WriteLine("Enter mother name:");
                        char motherName = Console.ReadKey().KeyChar;
                        Console.WriteLine();

                        Node motherNode =
                            FindByName(root, motherName)
                            ??
                            new Node()
                            {
                                name = motherName
                            };
                        node.father = fatherNode;
                        node.mother = motherNode;
                    }

                        break;
                    case 'f':
                        break;
                    case 'q':
                        return;
                }
            }
        }

        private void Display(IEnumerable<Node> nodes, int depth)
        {
            double spaceCenter = Math.Pow(2, depth) - 1;

            List<Node> nextLevelNodes = new List<Node>();

            foreach (var node in nodes)
            {
                Console.WriteLine(string.Join(" ", spaceCenter) + ((node?.name.ToString()) ?? " ") + string.Join(" ", spaceCenter + 1));

                nextLevelNodes.Add(node?.father);
                nextLevelNodes.Add(node?.mother);
            }

            Console.WriteLine();

            if (depth > 0)
            {
                Display(nextLevelNodes, depth - 1);
            }
        }

        private int Depth(IEnumerable<Node> levelNodes, int depth)
        {
            List<Node> nextLevelNodes = new List<Node>();

            foreach (var node in levelNodes)
            {
                if (node.father != null)
                {
                    nextLevelNodes.Add(node.father);
                }

                if (node.mother != null)
                {
                    nextLevelNodes.Add(node.mother);
                }
            }

            if (nextLevelNodes.Count == 0)
            {
                return depth;
            }
            else return Depth(nextLevelNodes, depth + 1);
        }

        private Node FindByName(Node node, char name)
        {
            if (node == null)
            {
                return null;
            }

            if (node.name == name)
            {
                return node;
            }

            return FindByName(node.father, name) ?? FindByName(node.mother, name);
        }
    }

    class Node
    {
        public char name;
        public Node father;
        public Node mother;
    }
}
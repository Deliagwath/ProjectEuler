using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            Problem15 p = new Problem15();
            Console.WriteLine(p.Problem(2));
            Console.ReadLine();
        }

        class Problem15
        {
            public int Problem(int graphSize)
            {
                // Generate Graph

                // Find all paths

            }

            class Node
            {
                public Node parent;
                public Node down;
                public Node right;

                public Node(Node parent, Node down, Node right)
                {
                    this.parent = parent;
                    this.down = down;
                    this.right = right;
                }
            }
        }
        class Problem14
        {
            public int Problem()
            {
                uint rolling = 0;
                int min = 0, current = 0, num = 0;
                for (int i = 999167; i > 1; i--)
                {
                    rolling = (uint)i;
                    current = 0;
                    //Console.Write(i);
                    while (rolling != 1)
                    {
                        if (rolling % 2 == 0) { rolling /= 2; }
                        else { rolling = (rolling * 3) + 1; }
                        current++;
                    }
                    if (current > min)
                    {
                        min = current;
                        num = i;
                    }
                    //Console.WriteLine(" " + current);
                }
                return num;
            }
        }
        class Problem13
        {
            public string Problem()
            {
                List<string> strNum = File.ReadLines("Q13Numbers").ToList();
                List<BigInteger> numbers = strNum.Select(str => BigInteger.Parse(str)).ToList();
                BigInteger sum = numbers.Aggregate(BigInteger.Add);
                return sum.ToString();
            }
        }
        class Problem12
        {
            public int Problem()
            {
                // BRUTE FORCE THEM ALLLLLLLLLLLLLL
                int triangleIndex = 2, triangleNumber = 0, factorCount = 0;
                bool complete = false;

                while (!complete)
                {
                    // Generate Triangle Number
                    triangleNumber = 0;
                    for (int i = 1; i < triangleIndex; i++)
                    {
                        triangleNumber += i;
                    }

                    // Find number of factors
                    for (int i = 1; i < Math.Ceiling(Math.Sqrt(triangleNumber)); i++)
                    {
                        if (triangleNumber % i == 0) { factorCount += 2; }
                        if (factorCount >= 499) { return triangleNumber; }
                    }
                    Console.WriteLine(String.Format("{0}\t\t{1}", triangleNumber, factorCount));
                    factorCount = 0;
                    triangleIndex++;
                }

                return -1;
            }
        }
        class Problem11
        {
            public int Problem()
            {
                List<string> inputMatrix = File.ReadLines("Q11Matrix").ToList();
                List<List<int>> matrix = inputMatrix.Select(line => line.Split(' ').Select(i => int.Parse(i)).ToList()).ToList();
                /*foreach (List<int> row in matrix)
                {
                    foreach (int element in row)
                    {
                        Console.Write(element.ToString() + " ");
                    }
                    Console.Write(Environment.NewLine);
                }*/
                Queue<int> horizontalStack = new Queue<int>(4);
                Queue<int> verticalStack = new Queue<int>(4);
                Queue<int> LRdiagonalStack = new Queue<int>(4);
                Queue<int> RLdiagonalStack = new Queue<int>(4);
                int current = 1, max = 0;

                for (int i = 0; i < matrix.Count; i++)
                {
                    horizontalStack.Clear();
                    verticalStack.Clear();
                    for (int j = 0; j < matrix[i].Count; j++)
                    {
                        // Horizontal
                        horizontalStack.Enqueue(matrix[i][j]);
                        if (horizontalStack.Count > 4) { horizontalStack.Dequeue(); }
                        current = multiplyList(horizontalStack);
                        if (current > max) { max = current; }

                        // Vertical
                        verticalStack.Enqueue(matrix[j][i]);
                        if (verticalStack.Count > 4) { verticalStack.Dequeue(); }
                        current = multiplyList(verticalStack);
                        if (current > max) { max = current; }

                        // Diagonal
                        // LEFT-RIGHT
                        if (i < matrix.Count - 3 && j < matrix[i].Count - 3)
                        {
                            LRdiagonalStack.Enqueue(matrix[i][j]);
                            LRdiagonalStack.Enqueue(matrix[i + 1][j + 1]);
                            LRdiagonalStack.Enqueue(matrix[i + 2][j + 2]);
                            LRdiagonalStack.Enqueue(matrix[i + 3][j + 3]);
                            current = multiplyList(LRdiagonalStack);
                            if (current > max) { max = current; }
                            LRdiagonalStack.Clear();
                        }

                        // Diagonal
                        // RIGHT-LEFT
                        if (i < matrix.Count - 3 && j > 3)
                        {
                            RLdiagonalStack.Enqueue(matrix[i][j]);
                            RLdiagonalStack.Enqueue(matrix[i + 1][j - 1]);
                            RLdiagonalStack.Enqueue(matrix[i + 2][j - 2]);
                            RLdiagonalStack.Enqueue(matrix[i + 3][j - 3]);
                            current = multiplyList(RLdiagonalStack);
                            if (current > max) { max = current; }
                            RLdiagonalStack.Clear();
                        }
                    }
                }

                //foreach (List<int> row in matrix) { foreach (int element in row) { Console.WriteLine(element); } }
                return max;
            }

            public int multiplyList(Queue<int> array)
            {
                int product = 1;
                foreach (int i in array)
                {
                    product *= i;
                }
                return product;
            }
        }
    }
}

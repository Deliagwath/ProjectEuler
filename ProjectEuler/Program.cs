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
            Console.WriteLine(Problem12());
            Console.ReadLine();
        }

        public static string Problem13()
        {
            List<string> strNum = File.ReadLines("Q13Numbers").ToList();
            List<BigInteger> numbers = strNum.Select(str => BigInteger.Parse(str)).ToList();
            BigInteger sum = numbers.Aggregate(BigInteger.Add);
            return "";
        }

        public static int Problem12()
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
                    if (triangleNumber % i == 0) { factorCount+=2; }
                    if (factorCount >= 499) { return triangleNumber; }
                }
                Console.WriteLine(String.Format("{0}\t\t{1}", triangleNumber, factorCount));
                factorCount = 0;
                triangleIndex++;
            }

            return -1;
        }

        public static int Problem11()
        {
            List<string> inputMatrix = File.ReadLines("Q11Matrix").ToList();
            List<List<int>> matrix = inputMatrix.Select(line => line.Split(' ').Select(i => int.Parse(i)).ToList()).ToList();
            //printMatrix(matrix);
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

        public static int multiplyList(Queue<int> array)
        {
            int product = 1;
            foreach (int i in array)
            {
                product *= i;
            }
            return product;
        }

        public static void printMatrix(List<List<int>> matrix)
        {
            foreach (List<int> row in matrix)
            {
                foreach (int element in row)
                {
                    Console.Write(element.ToString() + " ");
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}

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
            Problem2 p = new Problem2();
            Console.WriteLine(p.Problem(2));
            Console.ReadLine();
        }

        class Problem15
        {
            /*public int Problem(int graphSize)
            {
                // Generate Graph

                // Find all paths

            }*/

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
        class Problem8
        {
            public long Problem()
            {
                string num = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
                List<int> numArray = num.Select(c => (int) Char.GetNumericValue(c)).ToList();
                long ans = 0, tmp;
                for (int i = 0; i < numArray.Count - 12; i++)
                {
                    tmp = 1;
                    for (int j = 0; j < 13; j++)
                    {
                        tmp *= numArray[i + j];
                    }
                    if (tmp > ans) { ans = tmp; }
                }
                return ans;
            }
        }
        class Problem2
        {
            public int Problem(int method_number)
            {
                Dictionary<int, Func<int>> funcs = new Dictionary<int, Func<int>>();
                funcs.Add(1, () => Implementation1());
                funcs.Add(2, () => Implementation2());
                //funcs.Add(3, () => Implementation3());
                return funcs[method_number].Invoke();
            }

            // Recursive Implementation
            private int Implementation1()
            {
                return I1Helper(1, 1, 0);
            }

            private int I1Helper(int first, int second, int sum)
            {
                if (first > 4000000) { return sum; }
                int next_sum = first % 2 == 0 ? first + sum : sum;
                return I1Helper(second, first + second, next_sum);
            }

            // Iterative Implementation
            private int Implementation2()
            {
                int first = 1, second = 1, sum = 0, tmp = 0;
                while (first < 4000000)
                {
                    sum += first % 2 == 0 ? first : 0;
                    tmp = first;
                    first = second;
                    second += tmp;
                }
                return sum;
            }
        }
        class Problem1
        {
            public int Problem(int method_number)
            {
                Dictionary<int, Func<int>> funcs = new Dictionary<int, Func<int>>();
                funcs.Add(1, () => Implementation1());
                funcs.Add(2, () => Implementation2());
                funcs.Add(3, () => Implementation3());
                return funcs[method_number].Invoke();
            }

            // Set Implementation
            private int Implementation1()
            {
                HashSet<int> hs3 = new HashSet<int>();
                HashSet<int> hs5 = new HashSet<int>();
                for (int i = 3; i < 1000; i += 3) { hs3.Add(i); }
                for (int i = 5; i < 1000; i += 5) { hs5.Add(i); }
                hs3.UnionWith(hs5);
                int sum = 0;
                foreach (int i in hs3) { sum += i; }
                return sum;
            }

            // Iterative Implementation
            private int Implementation2()
            {
                int sum = 0;
                for (int i = 1; i < 1000; i++)
                {
                    if (i % 5 == 0 || i % 3 == 0) { sum += i; }
                }
                return sum;
            }

            // Formula Implementation
            // https://codereview.stackexchange.com/questions/2/project-euler-problem-1-in-python-multiples-of-3-and-5
            private int Implementation3()
            {
                int threes = I3Helper(3, 1000);
                int fives = I3Helper(5, 1000);
                int fifteen = I3Helper(15, 1000);
                return threes + fives - fifteen;
            }

            private int I3Helper(int factor, int max)
            {
                int part_sum = (max - 1) / factor;
                return factor * part_sum * (part_sum + 1) / 2;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_algorithm
{
    class Program
    {
        

        static double[] multMatrixVector(double[,] matrixA,double[] vectorX,int n)
        {
            double[] result = new double[n];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    result[i] += matrixA[i, j] * vectorX[j];
                }
            }

            return result;
        }
        static double vectorNorm2(double[] vector)
        {
            double res = 0;
            for(int i = 0; i < vector.Length; i++)
            {
                res += Math.Pow(vector[i],2);
            }
            return Math.Sqrt(res);
        }
        static bool IsExist(int[] vector,int num)
        {
            bool exist = true;
            for(int i = 0; i < vector.Count(); i++)
            {
                if (vector[i] == num)
                    exist = true;
                else
                    exist = false;
            }
            return exist;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            const int n = 3;
            int k = 1;
            int current = -1;
            double delta = 0.001, eps = 0.0001;
            double[,] matrixA = new double[n, n] { {2,1,0 },{1,3,1 },{0,1,2 } };
            double[] vectorXcurr = new double[n];
            double[] vectorXprev = new double[n];
            double[] vectorYCurr = new double[n];
            double[] vectorYPrev = new double[n] {1,1,1 };
            double[] vectorLambdaPrev = new double[n] {4,2,1 };
            double[] vectorLambdaCurr = new double[n];
            int[] vectorS = new int[n];
            List<int> vectorSList = new List<int>();
            //Step 1:
            for (int i = 0; i < n; i++)
            {
                vectorXprev[i] = vectorYPrev[i] / vectorNorm2(vectorYPrev) ;
                if (Math.Abs(vectorXprev[i]) > delta)
                {
                    vectorSList.Add(i);
                }
            }
            bool go=true;
            while (go)
            {
                
                
                vectorYCurr = multMatrixVector(matrixA, vectorXprev, n);
                for(int i = 0; i < n; i++)
                {
                    if (vectorSList.Contains(i))
                    {
                        vectorLambdaCurr[i] = vectorYCurr[i] / vectorXprev[i];
                    }
                    else
                    {
                        vectorLambdaCurr[i] = vectorLambdaPrev[i];
                    }

                }

                for (int i = 0; i < n; i++)
                {
                    vectorXcurr[i] = vectorYCurr[i] / vectorNorm2(vectorYCurr);
                    if (Math.Abs(vectorXprev[i]) > delta)
                    {
                        vectorSList.Add(i);
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(vectorLambdaCurr[i] - vectorLambdaPrev[i]) <= eps)
                    {
                        go = false;
                    }
                }
                k++;
                for (int i = 0; i < n; i++)
                {
                    vectorXprev[i] = vectorXcurr[i];
                    vectorLambdaPrev[i] = vectorLambdaCurr[i];
                }
                
                
            }

            /*for (int i = 0; i < n; i++)
            {
                Console.WriteLine(vectorLambdaCurr[i]);
            }*/

            double lambda1 = vectorLambdaCurr.Max();
            Console.WriteLine($"λ1 = {lambda1}");
            double[] result_AX = multMatrixVector(matrixA, vectorXcurr, n);
            double[] result_Lambda1X = new double[n];
            for (int i = 0; i < n; i++)
            {
                result_Lambda1X[i] = lambda1 * vectorXcurr[i];
            }
            Console.WriteLine("Ax~λx");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{result_AX[i]}~{result_Lambda1X[i]}");
            }
        }
    }
}

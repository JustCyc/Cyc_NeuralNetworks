using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Drawing.Drawing2D;

namespace Lab1
{
    public class MatrixMethods
    {
        public bool IsSymmetric(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(0) + 1; j++)
                {
                    if (matrix[i,j] != matrix[j, i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void Yakobi(double[,] matrix, double eps, out double[,] yakobiMatrix, out double[,] vectors)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            vectors = new double[rows, rows];
            yakobiMatrix = new double[rows, rows];
            yakobiMatrix = CopyMatrix(matrix);
            vectors = IdentityMatrix(vectors);
            List<double> ndElements = NonDiagonalElements(yakobiMatrix); 
            double ndSum = ndElements.Sum(el => el*el);
            int p;
            int q;
            do
            {
                Random rnd = new();
                do
                {
                    p = rnd.Next(0, rows);
                    q = rnd.Next(0, rows);
                }
                while (p == q || yakobiMatrix[p, q] == 0);
                double c = (yakobiMatrix[q, q] - yakobiMatrix[p, p]) / (2 * yakobiMatrix[p, q]);
                double tgPhi;
                if (c > 0)
                {
                    tgPhi = 1 / (c + Math.Sqrt(c * c + 1));
                }
                else
                {
                    tgPhi = 1 / (c - Math.Sqrt(c * c + 1));
                }

                double cosPhi = 1 / Math.Sqrt(1 + tgPhi * tgPhi);
                double sinPhi = tgPhi * cosPhi;

                double[,] modMatrix = new double[rows, rows];
                double[,] modVectors = new double[rows, rows];
                modMatrix = CopyMatrix(yakobiMatrix);
                modVectors = CopyMatrix(vectors);
                modMatrix[p, q] = 0;
                modMatrix[q, p] = 0;
                modMatrix[p, p] = yakobiMatrix[p, p] - yakobiMatrix[p, q] * tgPhi;
                modMatrix[q, q] = yakobiMatrix[q, q] + yakobiMatrix[p, q] * tgPhi;
                for (int r = 0; r < yakobiMatrix.GetUpperBound(0) + 1; r++)
                {
                    modVectors[r, p] = vectors[r, p] * cosPhi - vectors[r, q] * sinPhi;
                    modVectors[r, q] = vectors[r, p] * sinPhi + vectors[r, q] * cosPhi;
                    if (r != p && r != q)
                    {
                        if (yakobiMatrix[r, q] != 0 && yakobiMatrix[r, p] != 0)
                        {
                            modMatrix[p, r] = yakobiMatrix[r, p] - (sinPhi * (yakobiMatrix[r, q] + ((sinPhi / (1 + cosPhi)) * yakobiMatrix[r, p])));
                            modMatrix[r, p] = yakobiMatrix[r, p] - (sinPhi * (yakobiMatrix[r, q] + ((sinPhi / (1 + cosPhi)) * yakobiMatrix[r, p])));
                            modMatrix[q, r] = yakobiMatrix[r, q] + (sinPhi * (yakobiMatrix[r, p] - ((sinPhi / (1 + cosPhi)) * yakobiMatrix[r, q])));
                            modMatrix[r, q] = yakobiMatrix[r, q] + (sinPhi * (yakobiMatrix[r, p] - ((sinPhi / (1 + cosPhi)) * yakobiMatrix[r, q])));
                        }
                    }
                }
                ndSum = ndSum - Math.Pow(yakobiMatrix[p, q], 2) - Math.Pow(yakobiMatrix[q, p], 2);
                yakobiMatrix = CopyMatrix(modMatrix);
                vectors = CopyMatrix(modVectors);
            }
            while (ndSum >= eps);
        }

        static double[,] IdentityMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(0) + 1; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1;
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
            return matrix;
        }

        List<double> NonDiagonalElements(double[,] matrix)
        {
            List<double> resultList = [];
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(0) + 1; j++)
                {
                    if (i != j)
                    {
                        resultList.Add(matrix[i, j]);
                    }
                }
            }
            return resultList;
        }

        double[,] CopyMatrix(double[,] matrix)
        {
            double[,] resMatrix = new double[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(0) + 1];
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(0) + 1; j++)
                {
                    resMatrix[i, j] = matrix[i,j];
                }
            }
            return resMatrix;
        }

        public double[,] MultMatrix(double[,] matrixA, double[,] matrixB)
        {
            double[,] resMatrix = new double[matrixA.GetUpperBound(0) + 1, matrixA.GetUpperBound(0) + 1];

            for (var i = 0; i < matrixA.GetUpperBound(0) + 1; i++)
            {
                for (var j = 0; j < matrixA.GetUpperBound(0) + 1; j++)
                {
                    resMatrix[i, j] = 0;

                    for (var k = 0; k < matrixA.GetUpperBound(0) + 1; k++)
                    {
                        resMatrix[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }
            return resMatrix;
        }

        public double[,] TranspMatrix(double[,] matrix)
        {
            double[,] resMatrix = new double[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(0) + 1];
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(0) + 1; j++)
                {
                    resMatrix[i, j] = matrix[j, i];
                }
            }
            return resMatrix;
        }
    }
}

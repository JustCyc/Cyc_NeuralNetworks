using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Lab1.Tests
{
    [TestClass()]
    public class MatrixMethodsTests
    {
        MatrixMethods _matrix = new();
        [TestMethod()]
        public void IsSymmetricTest()
        {
            double[,] matrix = new double[,] { {1, 3, 0}, {3, 2, 6}, {0, 6, 5} };
            Assert.IsTrue(_matrix.IsSymmetric(matrix));
        }

        [TestMethod()]
        public void IsUnsymmetricTest()
        {
            double[,] matrix = new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Assert.IsTrue(!_matrix.IsSymmetric(matrix));
        }

        [TestMethod()]
        public void MultMatrixTest()
        {
            double[,] matrixB = new double[,] { { 2, 3, 4 }, { 3, 2, 6 }, { 0, 6, 5 } };
            double[,] matrixA = new double[,] { { 2, 3, 6, 6, 2 }, { 2, 2, 2, 2, 2 }, { 0, 3, 5, 1, 5 }, { 0, 3, 5, 8, 9 } };
            _matrix.MultMatrix(matrixA, matrixB);
        }
    }
}
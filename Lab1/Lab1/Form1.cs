using System;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        int rows;
        int cols;
        double eps;
        MatrixMethods _matrix = new();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 0;
            dataGridView1.ColumnCount = 0;
            try
            {
                rows = Convert.ToInt32(numericUpDown1.Value);
                cols = Convert.ToInt32(numericUpDown2.Value);
                if (rows != cols)
                {
                    throw new Exception("Matrix must be square");
                }
                else
                {
                    dataGridView1.RowCount = rows;
                    dataGridView1.ColumnCount = cols;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[,] matrix = new double[rows, cols];
            try
            {
                if (dataGridView1.RowCount == 0)
                {
                    throw new Exception("Create a matrix by clicking on the button \"Set size\"");
                }
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (dataGridView1[j, i].Value is null)
                        {
                            throw new Exception("All matrix cells must be filled");
                        }
                        if (!double.TryParse(dataGridView1[j, i].Value.ToString(), out double cellValue))
                        {
                            throw new Exception("Invalid data type");
                        }
                        else
                        {
                            matrix[i, j] = cellValue;
                        }
                    }
                }
                if (!_matrix.IsSymmetric(matrix))
                {
                    throw new Exception("Matrix must be symmetric");
                }
                eps = Convert.ToDouble(textBox1.Text);
                double[,] resultMatrix = new double[rows, cols];
                double[,] vectors = new double[rows, cols];
                _matrix.Yakobi(matrix, eps, out resultMatrix, out vectors);

                double[,] vectorsTransp = new double[rows, cols];
                vectorsTransp = _matrix.TranspMatrix(vectors);
                double[,] validation = new double[rows, cols];
                validation = 
                    _matrix.MultMatrix(_matrix.MultMatrix(vectors, resultMatrix), vectorsTransp);

                dataGridView2.RowCount = rows;
                dataGridView2.ColumnCount = cols;
                dataGridView3.RowCount = rows;
                dataGridView3.ColumnCount = cols;
                dataGridView4.RowCount = rows;
                dataGridView4.ColumnCount = cols;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        dataGridView2[j, i].Value = resultMatrix[i,j].ToString();
                    }
                }
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        dataGridView3[j, i].Value = vectors[i, j].ToString();
                    }
                }
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        dataGridView4[j, i].Value = Math.Round(validation[i, j], 1,  MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}");
                return;
            }
        }
    }
}

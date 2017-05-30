using System;

namespace OptimisationMethods.Core
{
    public class Point
    {
        /// <summary>
        /// индекс функции
        /// </summary>
        public int FunctionIndex { get; }
        /// <summary>
        /// матрица, содержащая координаты точки
        /// </summary>
        public double[,] matrix { get; set; }
        /// <summary>
        /// число строк
        /// </summary>
        public readonly int sizeRow;
        /// <summary>
        /// число столбцов
        /// </summary>
        public readonly int sizeCol;
        /// <summary>
        /// значение функции в текущей точке
        /// </summary>
        public double FunctionValue
        {
            get
            {
                return functions(this);
            }
        }
        //конструкторы
        public Point()
        {
        }
        public Point(int row, int col, int index)
        {
            sizeRow = row;
            sizeCol = col;
            matrix = new double[row, col];
            FunctionIndex = index;
        }
        public Point(Point x)
        {
            sizeRow = x.sizeRow;
            sizeCol = x.sizeCol;

            matrix = new double[sizeRow, sizeCol];

            for (int i = 0; i < sizeRow; i++)
                for (int j = 0; j < sizeCol; j++)
                {
                    matrix[i, j] = x.matrix[i, j];
                }
            FunctionIndex = x.FunctionIndex;
        }
        /// <summary>
        /// Список функций
        /// </summary>
        /// <param name="x">точка, в которой необходимо вычислить значение функции</param>
        /// <returns>значение функции в точке</returns>
        private static double functions(Point x)
        {
            double y = 0;
            if (x.sizeCol == 1)
            {
                switch (x.FunctionIndex)
                {
                    case 0: y = 2 * x.matrix[0, 0] * x.matrix[0, 0] + 3 * Math.Exp(-x.matrix[0, 0]); break;
                    case 1: y = -Math.Exp(-x.matrix[0, 0]) * Math.Log(x.matrix[0, 0]); break;
                    case 2: y = 2 * x.matrix[0, 0] * x.matrix[0, 0] - Math.Exp(x.matrix[0, 0]); break;
                    case 3: y = Math.Pow(x.matrix[0, 0], 4) - 14 * Math.Pow(x.matrix[0, 0], 3) + 60 * Math.Pow(x.matrix[0, 0], 2) - 70 * x.matrix[0, 0]; break;

                    case 4:
                        {
                            if (x.matrix[0, 0] >= 0)
                                y = 4 * Math.Pow(x.matrix[0, 0], 3) - 3 * Math.Pow(x.matrix[0, 0], 4);
                            else
                                y = 4 * Math.Pow(x.matrix[0, 0], 3) + 3 * Math.Pow(x.matrix[0, 0], 4);
                        }
                        break;

                    case 5: y = Math.Pow(x.matrix[0, 0], 2) + 2 * x.matrix[0, 0]; break;
                    case 6: y = 2 * Math.Pow(x.matrix[0, 0], 2) + 16 / x.matrix[0, 0]; break;
                    case 7: y = (10 * x.matrix[0, 0] * x.matrix[0, 0] * x.matrix[0, 0] + 3 * x.matrix[0, 0] * x.matrix[0, 0] + x.matrix[0, 0] + 5) * (10 * x.matrix[0, 0] * x.matrix[0, 0] * x.matrix[0, 0] + 3 * x.matrix[0, 0] * x.matrix[0, 0] + x.matrix[0, 0] + 5); break;
                    case 8: y = (3 * x.matrix[0, 0] * x.matrix[0, 0] + (12 / (x.matrix[0, 0] * x.matrix[0, 0] * x.matrix[0, 0])) - 5); break;
                }
            }
            if (x.sizeCol != 1)
            {
                switch (x.FunctionIndex)
                {
                    case 9: y = x.matrix[0, 0] * x.matrix[0, 0] + 3 * x.matrix[0, 1] * x.matrix[0, 1] + 2 * x.matrix[0, 0] * x.matrix[0, 1]; break;
                    case 10: y = 100 * (x.matrix[0, 1] - x.matrix[0, 0] * x.matrix[0, 0]) * (x.matrix[0, 1] - x.matrix[0, 0] * x.matrix[0, 0]) + (1 - x.matrix[0, 0]) * (1 - x.matrix[0, 0]); break;
                    case 11: y = -12 * x.matrix[0, 1] + 4 * x.matrix[0, 0] * x.matrix[0, 0] + 4 * x.matrix[0, 1] * x.matrix[0, 1] - 4 * x.matrix[0, 0] * x.matrix[0, 1]; break;
                    case 12: y = (x.matrix[0, 0] - 2) * (x.matrix[0, 0] - 2) * (x.matrix[0, 0] - 2) * (x.matrix[0, 0] - 2) + (x.matrix[0, 0] - 2 * x.matrix[0, 1]) * (x.matrix[0, 0] - 2 * x.matrix[0, 1]); break;
                    case 13: y = 4 * (x.matrix[0, 0] - 5) * (x.matrix[0, 0] - 5) + (x.matrix[0, 1] - 6) * (x.matrix[0, 1] - 6); break;
                    case 14: y = Math.Pow(x.matrix[0, 0] - 2, 4) + Math.Pow(x.matrix[0, 0] - 2 * x.matrix[0, 1], 2); break;
                    case 15: y = 2 * Math.Pow(x.matrix[0, 0], 3) + 4 * x.matrix[0, 0] * Math.Pow(x.matrix[0, 1], 3) - 10 * x.matrix[0, 0] * x.matrix[0, 1] + x.matrix[0, 1] * x.matrix[0, 1]; break;
                    case 16: y = 8 * x.matrix[0, 0] * x.matrix[0, 0] + 4 * x.matrix[0, 0] * x.matrix[0, 1] + 5 * x.matrix[0, 1] * x.matrix[0, 1]; break;
                    case 17: y = 4 * (x.matrix[0, 0] - 5) * (x.matrix[0, 0] - 5) + (x.matrix[0, 1] - 6) * (x.matrix[0, 1] - 6); break;
                    case 18: y = 100 * Math.Pow((x.matrix[0, 1] - Math.Pow(x.matrix[0, 0], 2)), 2) + Math.Pow((1 - x.matrix[0, 0]), 2); break;
                    case 19: y = Math.Pow((x.matrix[0, 0] - 1), 2) + Math.Pow((x.matrix[0, 1] - 3), 2) + 4 * Math.Pow(x.matrix[0, 2] + 5, 2); break;
                    case 20: y = 8 * x.matrix[0, 0] * x.matrix[0, 0] + 4 * x.matrix[0, 0] * x.matrix[0, 1] + 5 * x.matrix[0, 1] * x.matrix[0, 1]; break;
                    case 21: y = 4 * (x.matrix[0, 0] - 5) * (x.matrix[0, 0] - 5) + (x.matrix[0, 1] - 6) * (x.matrix[0, 1] - 6); break;
                    case 22: y = Math.Pow(x.matrix[0, 1] - x.matrix[0, 0] * x.matrix[0, 0], 2) + Math.Pow(1 - x.matrix[0, 0], 2); break;
                    case 23: y = Math.Pow(x.matrix[0, 1] - x.matrix[0, 0] * x.matrix[0, 0], 2) + 100 * Math.Pow(1 - x.matrix[0, 0] * x.matrix[0, 0], 2); break;
                    case 24: y = 3 * Math.Pow(x.matrix[0, 0] - 4, 2) + 4 * Math.Pow(x.matrix[0, 1] + 3, 2) + 7 * Math.Pow(2 * x.matrix[0, 2] + 1, 2); break;
                    case 25: y = Math.Pow(x.matrix[0, 0], 3) + Math.Pow(x.matrix[0, 1], 2) - 3 * x.matrix[0, 0] - 2 * x.matrix[0, 1] + 2; break;
                    case 26: y = -12 * x.matrix[0, 1] + 4 * Math.Pow(x.matrix[0, 0], 2) + 4 * Math.Pow(x.matrix[0, 1], 2) - 4 * x.matrix[0, 0] * x.matrix[0, 1]; break;
                    case 27: y = Math.Pow(x.matrix[0, 0] - 2, 4) + Math.Pow(x.matrix[0, 0] - 2 * x.matrix[0, 1], 2); break;
                    case 28: y = Math.Pow(x.matrix[0, 0] * x.matrix[0, 1] * x.matrix[0, 2] - 1, 2) + 5 * Math.Pow(x.matrix[0, 2] * (x.matrix[0, 0] + x.matrix[0, 1]) - 2, 2) + 2 * Math.Pow(x.matrix[0, 0] + x.matrix[0, 1] + x.matrix[0, 2] - 3, 2); break;
                    case 29: y = 4 * Math.Pow(x.matrix[0, 0], 2) + 3 * Math.Pow(x.matrix[0, 1], 2) - 4 * x.matrix[0, 0] * Math.Pow(x.matrix[0, 1], 2) + x.matrix[0, 0]; break;
                    case 30: y = Math.Pow(x.matrix[0, 0] * x.matrix[0, 0] + x.matrix[0, 1] - 11, 2) + Math.Pow(x.matrix[0, 0] + x.matrix[0, 1] * x.matrix[0, 1] - 7, 2); break;
                    case 31: y = 100 * Math.Pow(x.matrix[0, 1] - x.matrix[0, 0] * x.matrix[0, 0] * x.matrix[0, 0], 2) + Math.Pow(1 - x.matrix[0, 0], 2); break;
                    case 32: y = Math.Pow(1.5 - x.matrix[0, 0] * (1 - x.matrix[0, 1]), 2) + Math.Pow(2.25 - x.matrix[0, 0] * (1 - x.matrix[0, 1] * x.matrix[0, 1]), 2) + Math.Pow(2.625 - x.matrix[0, 0] * (1 - x.matrix[0, 1] * x.matrix[0, 1] * x.matrix[0, 1]), 2); break;
                    case 33: y = Math.Pow(x.matrix[0, 0] + 10 * x.matrix[0, 1], 2) + 5 * Math.Pow(x.matrix[0, 2] - x.matrix[0, 3], 2) + Math.Pow(x.matrix[0, 1] - 2 * x.matrix[0, 2], 4) + 10 * Math.Pow(x.matrix[0, 0] - x.matrix[0, 3], 4); break;
                    case 34: y = 100 * Math.Pow(x.matrix[0, 1] - x.matrix[0, 0] * x.matrix[0, 0], 2) + Math.Pow(1 - x.matrix[0, 0], 2) + 90 * Math.Pow(x.matrix[0, 3] - x.matrix[0, 2] * x.matrix[0, 2], 2) + Math.Pow(1 - x.matrix[0, 2], 3) + 10.1 * (Math.Pow(x.matrix[0, 1] - 1, 2) + Math.Pow(x.matrix[0, 3] - 1, 2)) + 19.8 * (x.matrix[0, 1] - 1) * (x.matrix[0, 3] - 1); break;
                    case 35: y = (2 * Math.Pow(x.matrix[0, 0], 2) + 3 * Math.Pow(x.matrix[0, 1], 2)) * Math.Exp(Math.Pow(x.matrix[0, 0], 2) - Math.Pow(x.matrix[0, 1], 2)); break;
                    case 36: y = 0.1 * (12 + Math.Pow(x.matrix[0, 0], 2) + (1 + Math.Pow(x.matrix[0, 1], 2)) / Math.Pow(x.matrix[0, 0], 2) + (x.matrix[0, 0] * x.matrix[0, 0] * x.matrix[0, 1] * x.matrix[0, 1] + 100) / (Math.Pow(x.matrix[0, 0], 4) * Math.Pow(x.matrix[0, 1], 4))); break;
                    case 37: y = 100 * Math.Pow(x.matrix[0, 2] - 0.25 * Math.Pow(x.matrix[0, 0] + x.matrix[0, 1], 2), 2) + Math.Pow(1 - x.matrix[0, 0], 2) + Math.Pow(1 - x.matrix[0, 1], 2); break;
                    case 38: y = 4 * Math.Pow(x.matrix[0, 0], 2) + Math.Pow(x.matrix[0, 1], 2) - 12 * x.matrix[0, 1] + 4; break;
                    case 39: y = x.matrix[0, 0] * x.matrix[0, 0] + 2 * x.matrix[0, 1] * x.matrix[0, 1] + 5 * x.matrix[0, 2] * x.matrix[0, 2] - 2 * x.matrix[0, 0] * x.matrix[0, 1] - 4 * x.matrix[0, 1] * x.matrix[0, 2] - 2 * x.matrix[0, 2]; break;
                    case 40: y = Math.Pow(x.matrix[0, 0], 2) + 3 * Math.Pow(x.matrix[0, 1], 2) + 3 * x.matrix[0, 0] * x.matrix[0, 1] + x.matrix[0, 0]; break;
                    case 41: y = x.matrix[0, 0] * x.matrix[0, 0] + 2 * x.matrix[0, 1] * x.matrix[0, 1] - 2 * x.matrix[0, 0] * x.matrix[0, 1] + x.matrix[0, 0]; break;
                    case 42: y = 2 * x.matrix[0, 0] * x.matrix[0, 0] + x.matrix[0, 1] * x.matrix[0, 1] - 2 * x.matrix[0, 0] * x.matrix[0, 1] + x.matrix[0, 1]; break;
                    case 43: y = 2 * x.matrix[0, 0] * x.matrix[0, 0] + 2 * x.matrix[0, 1] * x.matrix[0, 1] - x.matrix[0, 0] * x.matrix[0, 1] + x.matrix[0, 0] + 10; break;
                }
            }
            return y;
        }
        //перегрузки операторов
        public static Point operator +(Point a, Point b)
        {
            if (a.sizeRow != b.sizeRow || a.sizeCol != b.sizeCol)
            {
                return a;//возвращаем начальную
            }

            Point m = new Point(a.sizeRow, a.sizeCol, a.FunctionIndex);

            for (int i = 0; i < a.sizeRow; i++)
                for (int j = 0; j < a.sizeCol; j++)
                {
                    m.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];
                }
            return m;
        }
        public static Point operator +(Point a, double b)
        {
            Point m = new Point(a.sizeRow, a.sizeCol, a.FunctionIndex);

            for (int i = 0; i < a.sizeRow; i++)
                for (int j = 0; j < a.sizeCol; j++)
                {
                    m.matrix[i, j] = a.matrix[i, j] + b;
                }
            return m;
        }
        public static Point operator -(Point a, double b)
        {
            Point m = new Point(a.sizeRow, a.sizeCol, a.FunctionIndex);

            for (int i = 0; i < a.sizeRow; i++)
                for (int j = 0; j < a.sizeCol; j++)
                {
                    m.matrix[i, j] = a.matrix[i, j] - b;
                }
            return m;
        }
        public static Point operator -(Point a, Point b)
        {
            if (a.sizeRow != b.sizeRow || a.sizeCol != b.sizeCol)
            {
                return a;//возвращаем начальную
            }

            Point m = new Point(a.sizeRow, a.sizeCol, a.FunctionIndex);

            for (int i = 0; i < a.sizeRow; i++)
                for (int j = 0; j < a.sizeCol; j++)
                    m.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j];
            return m;
        }
        public static Point operator *(Point a, Point b)
        {
            Point x = new Point(a.sizeRow, b.sizeCol, a.FunctionIndex);

            for (int i = 0; i < a.sizeRow; i++)
                for (int j = 0; j < b.sizeCol; j++)
                    for (int k = 0; k < b.sizeRow; k++)
                        x.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j];
            return x;
        }
        public static Point operator -(Point a)
        {
            Point m = new Point(a);
            for (int i = 0; i < a.sizeRow; i++)
                for (int j = 0; j < a.sizeCol; j++)
                    m.matrix[i, j] = -a.matrix[i, j];
            return m;
        }
        public static Point operator /(Point a, double b)
        {
            Point m = new Point(a.sizeRow, a.sizeCol, a.FunctionIndex);

            for (int i = 0; i < a.sizeRow; i++)
                for (int j = 0; j < a.sizeCol; j++)
                    m.matrix[i, j] = a.matrix[i, j] / b;
            return m;
        }
        public static Point operator *(double a, Point b)
        {
            Point m = new Point(b.sizeRow, b.sizeCol, b.FunctionIndex);
            for (int i = 0; i < b.sizeRow; i++)
                for (int j = 0; j < b.sizeCol; j++)
                    m.matrix[i, j] = b.matrix[i, j] * a;
            return m;
        }
        public static double operator /(Point a, Point b)
        {
            double y = a.matrix[0, 0] / b.matrix[0, 0];
            return y;
        }
    }
}

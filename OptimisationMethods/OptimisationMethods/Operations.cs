using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimisationMethods
{
    public static class Operations
    {         
        /// <summary>
        /// Транспонирование вектора
        /// </summary>
        /// <param name="x">точка, корорую необходимо транспонировать</param>
        /// <returns>транспонированная точка</returns>
        public static Point Transpose(Point x)
        {
            Point m = new Point(x.sizeCol, x.sizeRow,x.FunctionIndex);

            for (int i = 0; i < x.sizeRow; i++)
                for (int j = 0; j < x.sizeCol; j++)
                {
                    m.matrix[j, i] = x.matrix[i, j];
                }
            return m;
        }       
        /// <summary>
        /// Вычисление модуля в точке
        /// </summary>
        /// <param name="x">точка, в которой необходимо вычислить модуль</param>
        /// <returns>модуль</returns>
        public static Point Modulus(Point x)
        {
            Point m = new Point(x.sizeCol, x.sizeRow, x.FunctionIndex);

            for (int i = 0; i < m.sizeRow; i++)
                for (int j = 0; j < m.sizeCol; j++)
                    m.matrix[i, j] = Math.Abs(x.matrix[i, j]);
            return m;
        }
        /// <summary>
        /// Вычисление Стартового направления поиска вдоль оси по первой координате
        /// </summary>
        /// <param name="sizeCol">Количество столбцов</param>
        /// <param name="sizeRow">Количество строк</param>
        /// <returns>Стартовое направление поиска вдоль оси по первой координате</returns>
        public static Point InitialDirection(int sizeCol, int sizeRow)
        {
            Point p0 = new Point(sizeRow, sizeCol, 0);

            for (int i = 0; i < sizeRow; i++)
                for (int j = 0; j < sizeCol; j++)
                    p0.matrix[i, j] = 0;

            p0.matrix[0, 0] = 1;

            return p0;
        }      
        /// <summary>
        /// Вычисление нового направления поиска вдоль осей координат
        /// </summary>
        /// <param name="direction">направление поиска</param>
        /// <returns>новое направление поиска</returns>
        public static Point InvertingDirection(Point direction)
        {
            Point y = new Point(direction);
            //перебираем все координаты
            for (int i = 0; i < direction.sizeCol; i++)
            {
                //если единица и не последний
                if (direction.matrix[0, i] == 1 && i != direction.sizeCol - 1)
                {
                    y.matrix[0, i] = 0;
                    y.matrix[0, i + 1] = 1;
                }
                //если единица и последний
                else if (direction.matrix[0, i] == 1 && i == direction.sizeCol - 1)
                {
                    y.matrix[0, i] = 0;
                    y.matrix[0, 0] = 1;
                }
            }
            return y;
        }        
        /// <summary>
        /// Функция вычисления производной
        /// </summary>
        /// <param name="x">точка, в которой необходимо вычислить производную</param>
        /// <param name="p">направение поиска</param>
        /// <returns></returns>
        public static double Derivative(Point x, Point p)
        {
            return (p * Transpose(Gradient(x))).matrix[0,0];
        }       
        /// <summary>
        /// Вычисления градиента в точке
        /// </summary>
        /// <param name="x">точка, в который необходимо вычислить градиент</param>
        /// <returns></returns>
        public static Point Gradient(Point x)
        {
            Point result = new Point(x);
            Point leftpoint = new Point(x);
            Point rightPoint = new Point(x);
            double h = Math.Pow(10, -10);
            for (int i = 0; i < x.sizeCol; i++)
            {
                leftpoint.matrix[0, i] -= h;
                rightPoint.matrix[0, i] += h;
                result.matrix[0, i] = ((leftpoint.FunctionValue - 4 * x.FunctionValue + 3 * rightPoint.FunctionValue) 
                    / (2 * h));
                leftpoint.matrix[0, i] += h;
                rightPoint.matrix[0, i] -= h;
            }
            return result;
        }       
        /// <summary>
        /// Формулы методов переменной метрики
        /// </summary>
        /// <param name="A"></param>
        /// <param name="gamma"></param>
        /// <param name="S"></param>
        /// <param name="dx"></param>
        /// <param name="index">индекс метода</param>
        /// <returns></returns>
        public static Point VariableMetricFunctions(Point A, Point gamma, Point S, Point dx, int index)
        {
            double p;
            switch (index)
            {
                case 7:
                    p = (dx * Operations.Transpose(dx)) / (gamma * Operations.Transpose(dx)) - (S * Operations.Transpose(S)) / (gamma * Operations.Transpose(S));
                    A = A + p;
                    break;
                case 8:
                    Point E = new Point(A.sizeCol, A.sizeRow, A.FunctionIndex);
                    Point B = new Point();

                    for (int i = 0; i < A.sizeCol; i++)
                        for (int j = 0; j < A.sizeRow; j++)
                        {
                            if (i == j)
                                E.matrix[i, j] = 1;
                            else
                                E.matrix[i, j] = 0;
                        }

                    B = E - (dx * Operations.Transpose(gamma)) / (dx * Operations.Transpose(gamma));
                    A = B * A * B + (dx * Operations.Transpose(dx)) / (gamma * Operations.Transpose(dx));

                    break;
                case 9:
                    p = (1 + ((gamma) * Operations.Transpose(S)) / (dx * Operations.Transpose(gamma))) * (((dx) * Operations.Transpose(dx)) / (gamma * Operations.Transpose(dx))) - ((dx) * Operations.Transpose(S) + (S) * Operations.Transpose(dx)) / (gamma * Operations.Transpose(dx));
                    A = A + p;
                    break;
                case 10:
                    p = ((dx) * Operations.Transpose(dx)) / (gamma * Operations.Transpose(dx)) - ((S) * Operations.Transpose(dx)) / (gamma * Operations.Transpose(dx));
                    A = A + p;
                    break;
                case 11:
                    p = ((dx - S) * Operations.Transpose(dx - S)) / (gamma * Operations.Transpose(dx - S));
                    A = A + p;
                    break;
                case 12:
                    p = ((dx - S) * Operations.Transpose(dx)) / (gamma * Operations.Transpose(dx));
                    A = A + p;
                    break;
                case 13:
                    p = ((dx - S) * Operations.Transpose(S)) / (S * Operations.Transpose(gamma));
                    A = A + p;
                    break;
                case 14:
                    p = (S * Operations.Transpose(S)) / (S * Operations.Transpose(gamma));
                    A = A - p;
                    break;
            }

            return A;
        }  
        /// <summary>
        /// Вспомогательный метод для методов переменной метрики
        /// </summary>
        /// <param name="A"></param>
        /// <param name="x0"></param>
        /// <param name="x"></param>
        /// <param name="k"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Point VariableMetricMethodsAncillary(Point A, Point x0, Point x, int k, int index)
        {
            if (k % 2 == 0)
            {

                for (int i = 0; i < A.sizeCol; i++)
                    for (int j = 0; j < A.sizeRow; j++)
                        if (i == j)
                            A.matrix[i, j] = 1;
                        else
                            A.matrix[i, j] = 0;
            }
            else
            {
                Point gamma = new Point();
                Point S = new Point();
                Point dx = x - x0;
                gamma = Operations.Gradient(x) - Operations.Gradient(x0);
                S = gamma * A;
                A = VariableMetricFunctions(A, gamma, S, dx, index);

            }

            return A;
        }       
        /// <summary>
        /// Формулы методов сопряженных градиентов
        /// </summary>
        /// <param name="x0">предыдущая точка поиска</param>
        /// <param name="p0">предыдущее направление поиска</param>
        /// <param name="x">текущаяя точка поиска</param>
        /// <param name="index">индекс метода</param>
        /// <returns>вспомогательный коэффициент B</returns>
        public static double NonlinearConjugateGradientFunctions(Point x0, Point p0, Point x, int index)
        {
            double d = 0;
            Point gamma = Gradient(x) - Gradient(x0);

            switch (index)
            {
                case 15:
                    d = (gamma * Operations.Transpose(Gradient(x))) / (gamma * Operations.Transpose(p0));
                    break;
                case 16:
                    d = (Gradient(x) * Operations.Transpose(gamma)) / (Gradient(x0) * Operations.Transpose(Gradient(x0)));
                    break;
                case 17:
                    d = (Gradient(x) * Operations.Transpose(Gradient(x))) / (Gradient(x0) * Operations.Transpose(Gradient(x0)));
                    break;
                case 18:
                    d = -(Gradient(x) * Operations.Transpose(Gradient(x))) / (Gradient(x0) * Operations.Transpose(Gradient(p0)));
                    break;
            }
            return d;

        }
        /// <summary>
        /// Формулы интерполяции
        /// </summary>
        /// <param name="ch">индекс метода</param>
        /// <param name="a1">левая альфа</param>
        /// <param name="b1">средняя альфа</param>
        /// <param name="c1">правая альфа</param>
        /// <param name="x">точка</param>
        /// <param name="p">направление поиска</param>
        /// <returns></returns>
        public static double interpolationFunctions(int ch, double a1, double b1, double c1, Point x, Point p)
        {
            double d = 0;
            if (ch == 1)
                d = ((x + a1 * p).FunctionValue * (b1 * b1 - c1 * c1) + (x + b1 * p).FunctionValue * (c1 * c1 - a1 * a1) + (x + c1 * p).FunctionValue * (a1 * a1 - b1 * b1)) / (((x + a1 * p).FunctionValue * (b1 - c1) + (x + b1 * p).FunctionValue * (c1 - a1) + (x + c1 * p).FunctionValue * (a1 - b1)) * 2);
            if (ch == 2)
                d = (a1 + b1) / 2 + ((x + a1 * p).FunctionValue - (x + b1 * p).FunctionValue) * ((x + a1 * p).FunctionValue - (x + b1 * p).FunctionValue) * (b1 - c1) * (c1 - a1) / (((x + a1 * p).FunctionValue * (b1 - c1) + (x + b1 * p).FunctionValue * (c1 - a1) + (x + c1 * p).FunctionValue * (a1 - b1)) * 2);
            if (ch == 3)
                d = b1 + ((b1 - a1) * (b1 - a1) * ((x + b1 * p).FunctionValue - (x + c1 * p).FunctionValue) - (b1 - c1) * (b1 - c1) * ((x + b1 * p).FunctionValue - (x + a1 * p).FunctionValue)) / ((b1 - a1) * ((x + b1 * p).FunctionValue - (x + c1 * p).FunctionValue) - (b1 - c1) * ((x + b1 * p).FunctionValue - (x + a1 * p).FunctionValue));
            if (ch == 4)
                d = b1 + ((b1 - a1) * ((x + a1 * p).FunctionValue - (x + c1 * p).FunctionValue)) / (((x + a1 * p).FunctionValue - 2 * (x + b1 * p).FunctionValue + (x + c1 * p).FunctionValue) * 2);
            if (ch == 5)
            {
                double z = Operations.Derivative(x + a1 * p, p) + Operations.Derivative(x + b1 * p, p) + 3 * ((x + a1 * p).FunctionValue - (x + b1 * p).FunctionValue) / (b1 - a1);
                double w = Math.Sqrt(z * z - Operations.Derivative(x + a1 * p, p) * Operations.Derivative(x + b1 * p, p));
                double y = (z + w - Operations.Derivative(x + a1 * p, p)) / (Operations.Derivative(x + b1 * p, p) - Operations.Derivative(x + a1 * p, p) + 2 * w);
                if (0 <= y && y <= 1)
                    d = a1 + y * (b1 - a1);
                else
                {
                    if (y > 1)
                        d = b1;
                    if (y < 0)
                        d = a1;
                }
            }
            return d;
        }
        /// <summary>
        /// Функция сортировки для метода Нелдера-Мида
        /// </summary>
        /// <param name="a">Список точек, который необходимо отсортировать</param>
        public static void Sorting(Point[] a)
        {
            for (int i = 0; i < a[0].sizeRow; i++)
                for (int j = 0; j < a[0].sizeCol; j++)
                    if (a[j].FunctionValue > a[j + 1].FunctionValue)
                    {
                        Point b = new Point(a[j]);
                        for (int k = 0; k < a[0].sizeCol; k++)
                        {
                            a[j].matrix[0, k] = a[j + 1].matrix[0, k];
                            a[j + 1].matrix[0, k] = b.matrix[0, k];
                        }
                    }
        }
        /// <summary>
        /// Вспомогательный метод для Хукка-Дживса
        /// </summary>
        /// <param name="x">первая точка</param>
        /// <param name="l">вторая точка</param>
        /// <param name="r">третья точка</param>
        /// <param name="temp">четвертая точка</param>
        /// <param name="sh">шаг поиска</param>
        public static void Search(Point x, Point l, Point r, Point temp, double sh)
        {
            l = new Point(x);
            r = new Point(x);

            for (int i = 0; i < x.sizeCol; i++)
            {
                l.matrix[0, i] -= sh;
                r.matrix[0, i] += sh;

                if (x.FunctionValue > r.FunctionValue && r.FunctionValue < l.FunctionValue)
                    temp.matrix[0, i] = r.matrix[0, i];
                if (x.FunctionValue > l.FunctionValue && r.FunctionValue > l.FunctionValue)
                    temp.matrix[0, i] = l.matrix[0, i];
                if (x.FunctionValue == l.FunctionValue && x.FunctionValue == r.FunctionValue)
                    temp.matrix[0, i] = x.matrix[0, i];
            }

        }
        /// <summary>
        /// Вычисление единичного вектора
        /// </summary>
        /// <param name="p">направление поиска</param>
        /// <returns>нормированное направление поиска</returns>
        public static Point UnitVector(Point p)
        {
            Point x = new Point(p);

            for (int i = 0; i < x.sizeRow; i++)
                for (int j = 0; j < x.sizeCol; j++)
                {
                    x.matrix[i, j] = p.matrix[i, j] / Norm(p);
                }
            return x;

        }        
        /// <summary>
        /// Норма вектора
        /// </summary>
        /// <param name="x">точка</param>
        /// <returns></returns>
        public static double Norm(Point x)
        {
            double n = 0;
            for (int i = 0; i < x.sizeCol; i++)
            {
                n += Math.Pow(x.matrix[0, i], 2);
            }

            return Math.Sqrt(n);
        }
        /// <summary>
        /// вычисление матрицы Гёссе
        /// </summary>
        /// <param name="x">точка</param>
        /// <returns></returns>
        public static Point HessianMatrix(Point x)
        {
            Point result = new Point(x.sizeCol, x.sizeCol, x.FunctionIndex);
            Point temp = new Point(x);
            Point temp1 = new Point(x);
            Point temp2 = new Point(x);
            Point temp3 = new Point(x);
            Point temp4 = new Point(x);
            double h = Math.Pow(10, -7);
            for (int i = 0; i < x.sizeCol; i++)
                for (int j = 0; j < x.sizeCol; j++)
                {
                    /*if(i!=j)
                    {*/
                    temp.matrix[0, i] += h;
                    temp.matrix[0, j] += h;
                    temp1.matrix[0, i] += h;
                    temp1.matrix[0, j] -= h;
                    temp2.matrix[0, i] -= h;
                    temp2.matrix[0, j] += h;
                    temp3.matrix[0, i] -= h;
                    temp3.matrix[0, j] -= h;

                    result.matrix[i, j] = ((temp.FunctionValue - temp1.FunctionValue - temp2.FunctionValue + temp3.FunctionValue) / (4 * h * h));

                    //result.matrix[i, j] = ((2*f2(temp) - 5*f2(temp1) + 4*f2(temp2) - f2(temp3)) / ( h * h));
                    temp.matrix[0, i] -= h;
                    temp.matrix[0, j] -= h;
                    temp1.matrix[0, i] -= h;
                    temp1.matrix[0, j] += h;
                    temp2.matrix[0, i] += h;
                    temp2.matrix[0, j] -= h;
                    temp3.matrix[0, i] += h;
                    temp3.matrix[0, j] += h;
                    /*}
                    else
                    {
                            temp.matrix[0, j] -= h;
                            temp1.matrix[0, j] += h;
                            result.matrix[i, j] = ((f2(temp) - 2 * f2(x) + f2(temp1)) / (h * h));
                            temp.matrix[0, j] += h;
                            temp1.matrix[0, j] -= h;       
                    }*/
                }

            return result;
        }
        /// <summary>
        /// Вычисление обратной матрицы
        /// </summary>
        /// <param name="x">точка</param>
        /// <returns></returns>
        public static Point InvertibleMatrix(Point x)
        {
            Point y = new Point(x);
            if (x.sizeRow == 2)
            {
                double temp = y.matrix[0, 0];
                y.matrix[0, 0] = y.matrix[1, 1];
                y.matrix[1, 1] = temp;
                y.matrix[0, 1] = -y.matrix[0, 1];
                y.matrix[1, 0] = -y.matrix[1, 0];
                y = (1 / (y.matrix[0, 0] * y.matrix[1, 1] - y.matrix[1, 0] * y.matrix[0, 1])) * y;
                return y;
            }
            else
            {
                y = Operations.InvertibleMatrixGauss(x);
                return y;
            }
        }
        /// <summary>
        /// Вычисление обратной матрицы методом Гаусса-Жордана
        /// </summary>
        /// <param name="x">точка</param>
        /// <returns></returns>
        public static Point InvertibleMatrixGauss(Point x)
        {
            Point x0 = new Point(x);
            double e = Math.Pow(10, -4);
            double N1 = 0, N2 = 0;
            for (int i = 0; i < x0.sizeRow; i++)
            {
                double cols = 0, rows = 0;
                for (int j = 0; j < x0.sizeCol; j++)
                {
                    rows += Math.Abs(x0.matrix[i, j]);
                    cols += Math.Abs(x0.matrix[j, i]);
                }
                N1 = Math.Max(cols, N1);
                N2 = Math.Max(rows, N2);
            }

            x0 = Transpose(x0);

            x0 = (1 / (N1 * N2)) * x0;

            Point E = new Point(x0.sizeCol, x0.sizeRow, x0.FunctionIndex);
            for (int i = 0; i < E.sizeCol; i++)
                for (int j = 0; j < E.sizeRow; j++)
                {
                    if (i == j)
                        E.matrix[i, j] = 2;
                    else
                        E.matrix[i, j] = 0;
                }

            Point x1 = new Point(x0);

            if (Determinant(x) != 0)
            {
                while (Math.Abs(Determinant(x * x1) - 1) >= e)
                {
                    Point y = new Point(x1);
                    x1 = x * y;
                    x1 = -x1;
                    x1 = x1 + E;
                    x1 = y * x1;
                }
            }
            else
                return x;

            return x1;
        }        
        /// <summary>
        /// Определитель матрицы
        /// </summary>
        /// <param name="x">точка, в которой необходимо найти определитель</param>
        /// <returns></returns>
        public static double Determinant(Point x)
        {
            Point y = new Point(x);
            for (int i = 0; i < y.sizeCol - 1; i++)
                for (int j = i + 1; j < y.sizeCol; j++)
                {
                    double k = -y.matrix[j, i] / y.matrix[i, i];
                    for (int col = i; col < y.sizeCol; col++)
                        y.matrix[j, col] += y.matrix[i, col] * k;
                }
            double d = 1;
            for (int i = 0; i < y.sizeCol; i++)
                d *= y.matrix[i, i];
            return d;
        }
        /// <summary>
        /// Вычисление числа фибоначчи
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Fibonacci(double x)
        {
            if (x >= 1)
            {
                double f = 1, temp = 0;
                for (int i = 0; i < x; i++)
                {
                    f = f + temp;
                    temp = f - temp;
                }
                return f;
            }
            else
                return 1;
        }
    }
}

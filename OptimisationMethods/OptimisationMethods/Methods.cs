using OptimisationMethods.Entities;
using System;
using System.Linq;

namespace OptimisationMethods
{
    public static class Methods
    {
        /// <summary>
        /// Поиск минимума выбранным методом
        /// </summary>
        /// <param name="search"></param>
        /// <param name="indexMethod">индекс метода</param>
        /// <returns></returns>
        public static SearchResults Method(Search search, int indexMethod)
        {
            SearchResults results;

            switch (indexMethod)
            {
                case 0:
                    results = Methods.LinearSearch(search); break;
                case 1:
                    results = Methods.LinearSearch(search); break;
                case 2: results = Methods.Cauchy(search); break;
                case 3: results = Methods.CyclicCoordinateWiseDescent(search); break;
                case 4: results = Methods.Partan1(search); break;
                case 5: results = Methods.NelderMead(search); break;
                case 6: results = Methods.HookeJeeves(search); break;
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14: results = Methods.VariableMetricMethods(search); break;
                case 15:
                case 16:
                case 17:
                case 18: results = Methods.NonlinearConjugateGradient(search); break;
                case 19: results = Methods.Partan2(search); break;
                case 20: results = Methods.Newtoon(search); break;
                case 21: results = Methods.LinearSearch(search); break;
                case 22: results = Methods.Rosenbrock(search); break;
                case 23: results = Methods.Powell2(search); break;
                default: results = Methods.Powell2(search); break;

            }
            return results;
        }
        /// <summary>
        /// Метод Свенна
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static Search Svenn(Search search)
        {
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;

            double alpha = 0;
            double step = search.Error;
            if (Operations.Derivative(point, direction) > 0)
                step = -step;

            while (Operations.Derivative(point, direction) * Operations.Derivative(point + step * direction, direction) > 0)
            {
                search.alpha += step;
                step = 2 * step;
            }
            if (step < 0)
            {
                search.alphaB = alpha - step;
                search.alphaA = alpha + step;
            }
            else
            {
                search.alphaA = alpha - step;
                search.alphaB = alpha + step;
            }

            return search;
        }        
        /// <summary>
        /// Метод Дихотомии
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static Search Dichotomy(Search search, int max)
        {
            double d = 0.1 * search.Error;
            double x1, x2;

            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;

            int counter = 1;
            do
            {
                x1 = (search.alphaA + search.alphaB - d) / 2;
                x2 = (search.alphaA + search.alphaB + d) / 2;
                double y1 = (point + x1 * direction).FunctionValue;
                double y2 = (point + x2 * direction).FunctionValue;
                if ((point + x1 * direction).FunctionValue < (point + x2 * direction).FunctionValue)
                    search.alphaB = x2;
                else
                    search.alphaA = x1;
                counter++;
            }
            while (Math.Abs(search.alphaB - search.alphaA) > search.Error && counter < max);

            search.alpha = (search.alphaA + search.alphaB) / 2;
            search.CurrentPoint = search.CurrentPoint + search.alpha * search.SearchDirection;

            return search;
        }    
        /// <summary>
        /// Метод Больцано
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static Search Bolzano(Search search, int max)
        {
            int counter = 0;
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;

            do
            {
                search.alpha = (search.alphaA + search.alphaB) / 2;
                if (Operations.Derivative(point + search.alpha * direction, direction) > 0)
                    search.alphaB = search.alpha;
                else
                    search.alphaA = search.alpha;
                counter++;
            }
            while (Math.Abs(search.alphaA - search.alphaB) > search.Error 
            && Math.Abs(Operations.Derivative(point + search.alpha * direction, direction)) > search.Error 
            && counter < max);

            search.alpha = (search.alphaA + search.alphaB) / 2;
            search.CurrentPoint = search.CurrentPoint + search.alpha * search.SearchDirection;

            return search;
        }
        /// <summary>
        /// Метод Фибоначчи 1
        /// </summary>
        /// <param name="search"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search Fibonacci1(Search search, int max)
        {
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;

            int counter = 1;
            double ln = 0.01 * search.Error, n = 0, x1, x2;
            while (Operations.Fibonacci(n) < (search.alphaB - search.alphaA) / ln)
                n++;
            x1 = search.alphaA + Operations.Fibonacci(n - 2) 
                * (search.alphaB - search.alphaA) / Operations.Fibonacci(n);
            x2 = search.alphaA + Operations.Fibonacci(n - 1) 
                * (search.alphaB - search.alphaA) / Operations.Fibonacci(n);
            do
            {
                if ((point + x1 * direction).FunctionValue < (point + x2 * direction).FunctionValue)
                {
                    search.alphaB = x2;
                    x2 = x1;
                    x1 = search.alphaA + Operations.Fibonacci(n - 2 - counter) 
                        * (search.alphaB - search.alphaA) / Operations.Fibonacci(n - counter);
                }
                else
                {
                    search.alphaA = x1;
                    x1 = x2;
                    x2 = search.alphaA + Operations.Fibonacci(n - 1 - counter) 
                        * (search.alphaB - search.alphaA) / Operations.Fibonacci(n - counter);
                }
                counter++;
            }
            while (counter != n && counter < max);
            if ((point + x1 * direction).FunctionValue <= (point + x2 * direction).FunctionValue)
            {
                search.alphaA = x1;
                // b1=x2;
            }
            else
            {
                // a1=x11;
                search.alphaB = x1;
            }

            search.alpha = (search.alphaA + search.alphaB) / 2;
            search.CurrentPoint = search.CurrentPoint + search.alpha * search.SearchDirection;

            return search;
        }
        /// <summary>
        /// Метод Фибоначчи 2
        /// </summary>
        /// <param name="search"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search Fibonacci2(Search search, int max)
        {
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;

            double x11, x2;
            double ln = 0.001 * search.Error;
            int n = 0;
            int counter = 0;
            while (Operations.Fibonacci(n) < (search.alphaB - search.alphaA) / ln)
            {
                n++;
            }
            x11 = search.alphaA + Operations.Fibonacci(n - 1) * (search.alphaB - search.alphaA) 
                / Operations.Fibonacci(n) + Math.Pow((-1.0), n) * search.Error / Operations.Fibonacci(n);
            while (counter < n && counter < max)
            {
                x2 = search.alphaA + search.alphaB - x11;
                if ((point + x11 * direction).FunctionValue < (point + x2 * direction).FunctionValue)
                {
                    if (x11 < x2)
                        search.alphaB = x2;
                    else
                        search.alphaA = x2;
                }
                else
                {
                    if (x11 < x2)
                        search.alphaA = x11;
                    else
                        search.alphaB = x11;
                    x11 = x2;
                }
                counter++;
            }

            search.alpha = (search.alphaA + search.alphaB) / 2;
            search.CurrentPoint = search.CurrentPoint + search.alpha * search.SearchDirection;

            return search;
        }
        /// <summary>
        /// Метод золотого сечения 1
        /// </summary>
        /// <param name="search"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search GoldenRatio1(Search search, int max)
        {
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;
            //0.6
            double t1 = (Math.Sqrt((double)5) - 1) / 2;
            //0.3
            double t2 = (3 - Math.Sqrt((double)5)) / 2;
            double x1 = search.alphaA + t2 * Math.Abs(search.alphaA - search.alphaB);//первая точка
            double x2 = search.alphaA + t1 * Math.Abs(search.alphaA - search.alphaB);//вторая точка
            int k = 0;
            while (Math.Abs(search.alphaB - search.alphaA) >= search.Error && k < max)
            {
                if ((point + x1 * direction).FunctionValue < (point + x2 * direction).FunctionValue)
                {
                    search.alphaB = x2;
                    x2 = x1;
                    x1 = search.alphaA + t2 * Math.Abs(search.alphaA - search.alphaB);
                }
                else
                {
                    search.alphaA = x1;
                    x1 = x2;
                    x2 = search.alphaA + t1 * Math.Abs(search.alphaA - search.alphaB);
                }
                k++;
            }

            search.alpha = (search.alphaA + search.alphaB) / 2;
            search.CurrentPoint = search.CurrentPoint + search.alpha * search.SearchDirection;

            return search;
        }
        /// <summary>
        /// Метод золотого сечения 2
        /// </summary>
        /// <param name="search"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search GoldenRatio2(Search search, int max)
        {
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;
            //0.6
            double t = (Math.Sqrt(5) - 1) / 2; 
            //первая точка
            double x1 = search.alphaA + t * Math.Abs(search.alphaA - search.alphaB);
            //вторая точка
            double x2 = search.alphaA + search.alphaB - x1;
            int counter = 0;
            while (Math.Abs(search.alphaB - search.alphaA) > search.Error 
                && Math.Abs((search.alphaA + search.alphaB) / 2) > search.Error && counter < max)
            {
                x2 = search.alphaA + search.alphaB - x1;
                if ((point + x1 * direction).FunctionValue > (point + x2 * direction).FunctionValue)
                {
                    if (x1 > x2)
                        search.alphaB = x1;
                    else
                        search.alphaA = x1;
                    x1 = search.alphaA + t * Math.Abs(search.alphaA - search.alphaB);
                }
                else
                {
                    if (x1 > x2)
                        search.alphaA = x2;
                    else
                        search.alphaB = x2;
                }
                counter++;
            }

            search.alpha = (search.alphaA + search.alphaB) / 2;
            search.CurrentPoint = search.CurrentPoint + search.alpha * search.SearchDirection;

            return search;
        }
        /// <summary>
        /// Метод Трехточечного поиска
        /// </summary>
        /// <param name="search"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search ThreePointSearch(Search search, int max)
        {
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;

            double x3 = (search.alphaA + search.alphaB) / 2, l, x1, x2;
            int counter = 1;
            do
            {
                l = Math.Abs(search.alphaB - search.alphaA);
                x1 = search.alphaA + l / 4;
                x2 = search.alphaB - l / 4;
                if ((point + x1 * direction).FunctionValue < (point + x3 * direction).FunctionValue)
                {
                    search.alphaB = x3;
                    x3 = x1;
                }
                else
                {
                    if ((point + x1 * direction).FunctionValue >= (point + x3 * direction).FunctionValue 
                        && (point + x3 * direction).FunctionValue <= (point + x2 * direction).FunctionValue)
                    {
                        search.alphaA = x1;
                        search.alphaB = x2;
                    }
                    else
                    {
                        search.alphaA = x3;
                        x3 = x2;
                    }
                }
                counter++;
            }
            while (Math.Abs(search.alphaB - search.alphaA) > search.Error && counter < max);

            search.alpha = (search.alphaA + search.alphaB) / 2;
            search.CurrentPoint = search.CurrentPoint + search.alpha * search.SearchDirection;

            return search;
        }
        /// <summary>
        /// Метод Дэвиса-Свенна-Кэмпи
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static Search Dsc(Search search, int max)
        {
            //метод не работает
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;

            double center = (search.alphaA + search.alphaB) / 2, step = 0.00001, d;
            search.alphaA = center - step;
            search.alphaB = center + step;
            int count = 1;
            d = Operations.interpolationFunctions(4, search.alphaA, center, search.alphaB, point, direction);
            while (Math.Abs((center - d) / center) > search.Error
                && (Math.Abs(((point + center * direction).FunctionValue 
                - (point + d * direction).FunctionValue) 
                / (point + center * direction).FunctionValue) > search.Error) 
                && count < max)
            {
                step = step / 2;
                if ((point + center * direction).FunctionValue > (point + d * direction).FunctionValue)
                    center = d;
                search.alphaA = center - step;
                search.alphaB = center + step;
                d = Operations.interpolationFunctions(4, search.alphaA, center, search.alphaB, point, direction);
                count++;
            }
            search.alphaA = center;
            search.alphaB = d;
            search.alpha = (search.alphaA + search.alphaB) / 2;
            search.CurrentPoint = search.CurrentPoint + search.alpha * direction;

            return search;
        }
        /// <summary>
        /// Метод Линейной интерполяции
        /// </summary>
        /// <param name="search"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search LinearInterpolation(Search search, int max)
        {
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;

            int counter = 1;
            search.alpha = search.alphaB - Operations.Derivative(point + search.alphaB * direction, direction) 
                * (search.alphaB - search.alphaA) 
                / (Operations.Derivative(point + search.alphaB * direction, direction) 
                - Operations.Derivative(point + search.alphaA * direction, direction));
            while (Math.Abs(Operations.Derivative(point + search.alpha * direction, direction)) > search.Error 
                && counter < max)
            {
                if (Operations.Derivative(point + search.alpha * direction, direction) > 0)
                    search.alphaB = search.alpha;
                else
                    search.alphaA = search.alpha;
                search.alpha = search.alphaB - Operations.Derivative(point + search.alphaB * direction, direction) 
                    * (search.alphaB - search.alphaA) 
                    / (Operations.Derivative(point + search.alphaB * direction, direction) 
                    - Operations.Derivative(point + search.alphaA * direction, direction));
                counter++;
            }
            search.CurrentPoint = search.CurrentPoint + search.alpha * direction;
            return search;
        }
        /// <summary>
        /// Метод Пауэлла
        /// </summary>
        /// <param name="search"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search Powell(Search search, int max)
        {
            //метод не рабоатет
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;

            double c = (search.alphaA + search.alphaB) / 2, d;
            int counter = 1;
            d = Operations.interpolationFunctions(1, search.alphaA, c, search.alphaB, point, direction);
            while (Math.Abs((c - d) / c) > search.Error 
                && (Math.Abs(((point + c * direction).FunctionValue 
                - (point + d * direction).FunctionValue) 
                / (point + c * direction).FunctionValue) > search.Error) 
                && counter < max)
            {
                if ((point + c * direction).FunctionValue > (point + d * direction).FunctionValue)
                {
                    if (c < d)
                        search.alphaA = c;
                    else
                        search.alphaB = c;
                    c = d;
                }
                else
                {
                    if (c < d)
                        search.alphaB = d;
                    else
                        search.alphaA = d;
                }
                d = Operations.interpolationFunctions(4, search.alphaA, c, search.alphaB, point, direction);
                counter++;
            }
            search.alphaA = c;
            search.alphaB = d;
            search.alpha = (search.alphaA + search.alphaB) / 2;
            search.CurrentPoint = search.CurrentPoint + search.alpha * search.SearchDirection;

            return search;
        }
        /// <summary>
        /// Метод бикубической интерполяции
        /// </summary>
        /// <param name="search"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search BicubicInterpolation(Search search, int max)
        {
            //метод не работает
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;
            int counter = 1;
            double d = Operations.interpolationFunctions(5, search.alphaA, search.alphaB, 0, point, direction);
            while (Math.Abs(Operations.Derivative(point + d * direction, direction)) > search.Error && counter < max)
            {
                if (Operations.Derivative(point + d * direction, direction) > 0)
                    search.alphaB = d;
                else
                    search.alphaA = d;
                d = Operations.interpolationFunctions(5, search.alphaA, search.alphaB, 0, point, direction);
                counter++;
            }
            search.alpha = (search.alphaA + search.alphaB) / 2;
            search.CurrentPoint = search.CurrentPoint + search.alpha * search.SearchDirection;
            return search;
        }
        /// <summary>
        /// Метод Экстраполяции-Интерполяции
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static Search Ei(Search search, int max)
        {
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;
            int counter = 1;
            search.alpha = search.alphaB 
                - Operations.Derivative(point + search.alphaB * direction, direction) 
                * (search.alphaB - search.alphaA) 
                / (Operations.Derivative(point + search.alphaB * direction, direction) 
                - Operations.Derivative(point + search.alphaA * direction, direction));
            while (Math.Abs(Operations.Derivative(point + search.alpha * direction, direction)) > search.Error 
                && counter < max)
            {
                if (Operations.Derivative(point + search.alpha * direction, direction) > 0)
                {
                    search.alphaB = search.alpha;
                }
                else
                {
                    search.alphaA = search.alpha;
                }
                search.alpha = search.alphaB 
                    - Operations.Derivative(point + search.alphaB * direction, direction) 
                    * (search.alphaB - search.alphaA) 
                    / (Operations.Derivative(point + search.alphaB * direction, direction) 
                    - Operations.Derivative(point + search.alphaA * direction, direction));
                counter++;
            }

            search.CurrentPoint = search.CurrentPoint + search.alpha * direction;

            return search;
        }
        /// <summary>
        /// Метод Ньютона
        /// </summary>
        /// <param name="search"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search Newtoon(Search search, int max)
        {
            //метод не работает
            Point point = search.CurrentPoint;
            Point direction = search.SearchDirection;
            search.alpha = (search.alphaA+search.alphaB) / 2;
            int counter = 0;
            while (Math.Abs(Operations.Derivative(point + search.alpha * direction, direction)) > search.Error 
                && counter < max)
            {
                search.alpha -= (point + search.alpha * direction).FunctionValue 
                    / Operations.Derivative(point + search.alpha * direction, direction);
                counter++;
            }
            search.CurrentPoint = search.CurrentPoint + search.alpha * search.SearchDirection;

            return search;
        }
        /// <summary>
        /// Метод Нелдера-Мида
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults NelderMead(Search search)
        {
            //количество точек
            int n = 3;
            double al = 1,        //к. отражения
                bt = 0.5,         //к. сжатия
                gm = 2,           //к. растяжения
                step = 0.5;          //шаг

            Point[] simplexX = new Point[n];
            Point xh, xg, xl, xr, xe, xc, xs;
            double[] simplexY = new double[n];

            for (int i = 0; i < n; i++)
            {
                simplexX[i] = new Point(search.StartPoint);
            }

            simplexX[0].matrix[0, 0] += step;
            simplexY[0] = simplexX[0].FunctionValue;

            simplexX[1].matrix[0, 0] -= step;
            simplexX[1].matrix[0, 1] -= step;
            simplexY[1] = simplexX[1].FunctionValue;

            simplexX[2].matrix[0, 0] -= step;
            simplexX[2].matrix[0, 1] += step;
            simplexY[2] = simplexX[2].FunctionValue;

            do
            {

                //1 step
                Operations.Sorting(simplexX);
                xh = simplexX[2];
                xg = simplexX[1];
                xl = simplexX[0];

                //2 step
                xc = (simplexX[1] + simplexX[0]) / 2;

                //3 step
                xr = (al + 1) * xc - al * simplexX[2];

                //4step
                double fr = xr.FunctionValue;
                double fh = simplexX[2].FunctionValue;
                double fg = simplexX[1].FunctionValue;
                double fl = simplexX[0].FunctionValue;
                double fe, fs;

                if (fr < fl)
                {
                    xe = (1 - gm) * xc + gm * xr;
                    fe = xe.FunctionValue;
                    if (fe < fl)
                    {
                        simplexX[2] = xe;
                        goto step9;
                    }
                    else if (fe >= fl)
                    {
                        simplexX[2] = xr;
                        goto step9;
                    }
                }
                else if (fl <= fr && fr < fg)
                {
                    simplexX[2] = xr;
                    goto step9;
                }
                else if (fh > fr && fr >= fg)
                {
                    Point tmp = new Point(xr);
                    xr = simplexX[2];
                    simplexX[2] = tmp;

                    goto step5;
                }
                else if (fr >= fh)
                {
                    goto step5;
                }

            //step 5
            step5:
                xs = bt * simplexX[2] + (1 - bt) * xc;
                fs = xs.FunctionValue;

                //step 6
                if (fs < fh)
                {
                    simplexX[2] = xs;
                }
                //step 8
                else
                {
                    simplexX[2] = simplexX[0] + (simplexX[2] - simplexX[0]) / 2;
                    simplexX[1] = simplexX[0] + (simplexX[1] - simplexX[0]) / 2;
                }


            step9:
                //step 9 KOP
                search.Iterations++;
                if (Operations.Norm(Operations.Gradient(simplexX[0])) < search.Error
                    || search.Iterations >= search.MaxIterations)
                {
                    SearchResults results = new SearchResults(search);
                    results.EndPoint = simplexX[0];
                    return results;
                }
            }
            while (true);
        }
        /// <summary>
        /// Метод коши
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults Cauchy(Search search)
        {
            do
            {
                search.SearchDirection = -(Operations.Gradient(search.CurrentPoint));
                //находим локальный минимум 
                search = ComboSearch(search);
                search.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(search.CurrentPoint)) > search.Error
            && search.Iterations < search.MaxIterations);

            SearchResults results = new SearchResults(search);
            return results;
        }
        /// <summary>
        /// Метод циклического покоординатного спуска
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults CyclicCoordinateWiseDescent(Search search)
        {
            do
            {
                //строим ортогональные направления по ЦПС
                search.SearchDirection = Operations.InitialDirection(search.StartPoint.sizeCol, search.StartPoint.sizeRow);
                //проходим по всем направлениям
                for (int i = 0; i < search.StartPoint.sizeCol * search.StartPoint.sizeRow; i++)
                {
                    //находим локальный минимум
                    search = ComboSearch(search);                    
                    //изменяем направление по оси (ЦПС)
                    search.SearchDirection = Operations.InvertingDirection(search.SearchDirection);
                }

                search.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(search.CurrentPoint)) > search.Error
            && search.Iterations < search.MaxIterations);

            SearchResults results = new SearchResults(search);
            return results;
        }
        /// <summary>
        /// Метод Партан 1
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults Partan1(Search search)
        {
            Point x0 = search.StartPoint;
            Point x1 = search.StartPoint;
            Point x2 = search.StartPoint;
            Point x3 = search.StartPoint;

            search.SearchDirection = -(Operations.Gradient(search.CurrentPoint));
            //находим локальный минимум 
            search = ComboSearch(search);

            do
            {
                search.SearchDirection = -(Operations.Gradient(search.CurrentPoint));
                //находим локальный минимум 
                search = ComboSearch(search);

                x2 = new Point(search.CurrentPoint);
                x3 = x2 + search.alpha * (x2 - x0);
                x0 = new Point(x1);
                x1 = new Point(x3);
                search.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(x3)) > search.Error
            && search.Iterations < search.MaxIterations);

            SearchResults results = new SearchResults(search);
            results.EndPoint = x3;
            return results;
        }
        /// <summary>
        /// Метод Партан 2
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults Partan2(Search search)
        {
            Point x0 = search.StartPoint;
            Point x1 = search.StartPoint;
            Point x2 = search.StartPoint;
            Point x3 = search.StartPoint;

            search.SearchDirection = -(Operations.Gradient(search.CurrentPoint));
            //находим локальный минимум 
            search = ComboSearch(search);
            x1 = new Point(search.CurrentPoint);

            do
            {
                search.SearchDirection = -(Operations.Gradient(x1));
                //находим локальный минимум 
                search = ComboSearch(search);

                x2 = x1 + search.alpha * search.SearchDirection;
                x3 = 3 * x2 - 2 * x0;

                x0 = new Point(x1);
                x1 = new Point(x2);
                x2 = new Point(x3);
                search.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(x3)) > search.Error
            && search.Iterations < search.MaxIterations);

            SearchResults results = new SearchResults(search);
            results.EndPoint = x3;
            return results;
        }
        /// <summary>
        /// Методы сопряженных градиентов
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults NonlinearConjugateGradient(Search search)
        {
            Point xo = new Point();
            Point po = new Point();
            double beta = 0;
            do
            {
                if (search.Iterations % 2 == 0)
                    search.SearchDirection = -(Operations.Gradient(search.CurrentPoint));
                else
                {
                    beta = Operations.NonlinearConjugateGradientFunctions(po, xo, search.CurrentPoint, search.Function);

                    search.SearchDirection = -(Operations.Gradient(search.CurrentPoint)) + beta * search.SearchDirection;
                }

                search = ComboSearch(search);
                xo = search.CurrentPoint;
                po = search.SearchDirection;

                search.Iterations++;
            }
            while (Operations.Norm(
                Operations.Gradient(search.CurrentPoint)) > search.Error
                && search.Iterations < search.MaxIterations);

            SearchResults results = new SearchResults(search);
            return results;
        }
        /// <summary>
        /// Метод Хукка-Дживса
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults HookeJeeves(Search search)
        {
            double step = 1;
            Point x = new Point(search.StartPoint);
            Point x4 = new Point(search.StartPoint);
            Point x3 = new Point(search.StartPoint);
            Point x2 = new Point(search.StartPoint);
            Point l = new Point(search.StartPoint);
            Point r = new Point(search.StartPoint);

            do
            {
                double test = step;

                Operations.Search(x, l, r, x2, test);
                while (x2.FunctionValue > x.FunctionValue)
                {
                    test /= 2;
                    Operations.Search(x, l, r, x2, test);
                    if (test < search.Error)
                        break;
                }

                x3 = 2 * x2 - x;

                Operations.Search(x3, l, r, x4, test);

                if (x4.FunctionValue < x3.FunctionValue)
                {
                    x = x2;
                    x2 = x4;
                }
                else
                    step /= 2;

                search.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(x2)) > search.Error 
            && search.Iterations < search.MaxIterations);

            SearchResults results = new SearchResults(search);
            results.EndPoint = x2;
            return results;
        }
        /// <summary>
        /// Методы переменной метрики
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults VariableMetricMethods(Search search)
        {
            

            Point A = new Point(search.StartPoint.matrix.Length, search.StartPoint.matrix.Length, search.Function);

            Point xo = new Point();
            do
            {
                A = Operations.VariableMetricMethodsAncillary(A, xo, search.CurrentPoint, search.Iterations, search.Function);
                search.SearchDirection = -(Operations.Gradient(search.CurrentPoint)) * A;
                xo = search.CurrentPoint;
                search = ComboSearch(search);
                search.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(search.CurrentPoint)) > search.Error);

            SearchResults results = new SearchResults(search);
            return results;
        }
        /// <summary>
        /// Метод поиска по направлению
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults LinearSearch(Search search)
        {
            search = ComboSearch(search);
            search.Iterations = 1;
            SearchResults results = new SearchResults(search);
            return results;
        }
        /// <summary>
        /// Обобщенный метод Ньютона
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults Newtoon(Search search)
        {
            Point dx = new Point();
            do
            {
                dx = Operations.Gradient(search.CurrentPoint) * Operations.InvertibleMatrix(Operations.HessianMatrix(search.CurrentPoint));
                search.CurrentPoint = search.CurrentPoint - dx;
                search.Iterations++;
            }
            while (
            Operations.Norm(Operations.Gradient(search.CurrentPoint)) > search.Error
            && search.Iterations < search.MaxIterations);

            SearchResults results = new SearchResults(search);
            return results;
        }
        /// <summary>
        /// Метод Розенброка
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults Rosenbrock(Search search)
        {           
            //массивы для хранения точек, направлений и единичных векторов
            Point[] Pi = new Point[search.StartPoint.sizeCol * search.StartPoint.sizeRow];
            Point[] ei = new Point[search.StartPoint.sizeCol * search.StartPoint.sizeRow];
            //начальная точка
            Point x0 = new Point(search.StartPoint);
            //конечная точка
            Point x1 = new Point(search.StartPoint);
            //строим ортогональные направления по ЦПС
            search.SearchDirection = Operations.InitialDirection(search.StartPoint.sizeCol, search.StartPoint.sizeRow);
            //проходим по всем направлениям
            for (int i = 0; i < Pi.Length; i++)
            {
                //находим локальный минимум
                search = ComboSearch(search);

                //добавляем направление в массив
                if (search.alpha == 0)
                {
                    Pi[i] = search.SearchDirection;
                }
                else
                {
                    Pi[i] = search.alpha * search.SearchDirection;
                }
                //изменяем направление по оси (ЦПС)
                search.SearchDirection = Operations.InvertingDirection(search.SearchDirection);
            }

            search.Iterations++;
            //запоминаем конечную точку
            x1 = new Point(search.CurrentPoint);


            while (Operations.Norm(Operations.Gradient(search.CurrentPoint)) > search.Error
                && search.Iterations < search.MaxIterations)
            {
                //вычисляем новое направление поиска Bi, i = 1
                Pi[0] = x1 - x0;
                //строим единичный вектор
                ei[0] = Operations.UnitVector(Pi[0]);
                //строим ортогональные направления по методу Грамма-Шмидта Bi, i>1
                for (int i = 1; i < Pi.Length; i++)
                {
                    for (int k = 0; k < i; k++)
                    {
                        Pi[i] = Pi[i] - (ei[k] * Operations.Transpose(Pi[i])) * ei[k];
                    }
                    //нормируем векторы
                    ei[i] = Operations.UnitVector(Pi[i]);
                }
                //запоминаем начальную точку
                x0 = new Point(x1);

                //выполняем поиск по выбранным направлениям
                for (int i = 0; i < Pi.Length; i++)
                {
                    //изменяем направление по оси
                    search.SearchDirection = new Point(ei[i]);
                    //добавляем направление в массив
                    Pi[i] = search.SearchDirection;
                    //находим локальный минимум
                    search = ComboSearch(search);
                }
                search.Iterations++;
                //запоминаем конечную точку
                x1 = new Point(search.CurrentPoint);
            }

            SearchResults results = new SearchResults(search);
            return results;
        }
        /// <summary>
        /// Метод Пауэлла 2
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static SearchResults Powell2(Search search)
        {
            //массивы для хранения точек, направлений и единичных векторов
            Point[] Pi = new Point[search.StartPoint.sizeCol * search.StartPoint.sizeRow + 1];
            Point[] ei = new Point[search.StartPoint.sizeCol * search.StartPoint.sizeRow + 1];
            Point[] Xi = new Point[search.StartPoint.sizeCol * search.StartPoint.sizeRow + 1];
            //строим ортогональные направления по ЦПС
            search.SearchDirection = Operations.InitialDirection(search.StartPoint.sizeCol, search.StartPoint.sizeRow);
            //проходим по всем направлениям
            for (int i = 0; i < Pi.Length - 1; i++)
            {
                //находим локальный минимум
                search = ComboSearch(search);
                //добавляем направление в массив
                Xi[i] = new Point(search.CurrentPoint);
                Pi[i] = search.SearchDirection;
                //изменяем направление по оси (ЦПС)
                search.SearchDirection = Operations.InvertingDirection(search.SearchDirection);
            }

            //последнее направление стоится как первое
            Pi[Pi.Length - 1] = Pi[0];
            //и выполняем по этому направлению поиск
            search.SearchDirection = Pi[0];
            //находим локальный минимум
            search = ComboSearch(search);
            //добавляем направление в массив
            Pi[Pi.Length - 1] = search.SearchDirection;
            //запоминаем координату минимума
            Xi[Pi.Length - 1] = new Point(search.CurrentPoint);
            search.Iterations++;

            while (search.Iterations < search.MaxIterations 
                && Operations.Norm(Operations.Gradient(search.CurrentPoint)) > search.Error)
            {
                Pi[0] = Xi.Last() - Xi[0];
                Pi[0] = Operations.UnitVector(Pi[0]);

                Pi[Pi.Length - 1] = Pi[0];

                for (int i = 1; i < Pi.Length - 2; i++)
                {
                    Pi[i] = Pi[i + 1];
                }

                //выполняем поиск по выбранным направлениям
                for (int i = 0; i < Pi.Length; i++)
                {
                    //изменяем направление по оси
                    search.SearchDirection = new Point(Pi[i]);
                    //добавляем направление в массив
                    Pi[i] = search.SearchDirection;

                    //находим локальный минимум
                    search = ComboSearch(search);
                    //запоминаем координату минимума
                    Xi[i] = new Point(search.CurrentPoint);
                }
                search.Iterations++;
            }

            SearchResults results = new SearchResults(search);
            return results;
        }
        /// <summary>
        /// комбинированная стратегия поиска
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static Search ComboSearch(Search search)
        {
            search = Svenn(search);
            search = Bolzano(search, 10);
            return LinearInterpolation(search, 3);
        }
    }
}

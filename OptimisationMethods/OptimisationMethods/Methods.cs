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
        public static SearchResults Method(Search search)
        {
            SearchResults results;

            switch (search.IndexMethod)
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
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static Search Svenn(Search search)
        {
            Search searchParameters = new Search(search);

            double alpha = 0;
            double step = searchParameters.DiscretizationError;
            if (Operations.Derivative(searchParameters.CurrentPoint, search.SearchDirection) > 0)
                step = -step;

            while (Operations.Derivative(searchParameters.CurrentPoint, search.SearchDirection) 
                * Operations.Derivative(searchParameters.CurrentPoint + step * search.SearchDirection, search.SearchDirection) > 0)
            {
                searchParameters.alpha += step;
                step = 2 * step;
            }
            if (step < 0)
            {
                searchParameters.alphaB = alpha - step;
                searchParameters.alphaA = alpha + step;
            }
            else
            {
                searchParameters.alphaA = alpha - step;
                searchParameters.alphaB = alpha + step;
            }

            return searchParameters;
        }        
        /// <summary>
        /// Метод Дихотомии
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static Search Dichotomy(Search search, int max)
        {
            Search searchParameters = new Search(search);
            double step = 0.1 * searchParameters.DiscretizationError;
            double x1, x2;

            int counter = 1;
            do
            {
                x1 = (searchParameters.alphaA + searchParameters.alphaB - step) / 2;
                x2 = (searchParameters.alphaA + searchParameters.alphaB + step) / 2;
                double y1 = (searchParameters.CurrentPoint + x1 * searchParameters.SearchDirection).FunctionValue;
                double y2 = (searchParameters.CurrentPoint + x2 * searchParameters.SearchDirection).FunctionValue;
                if ((searchParameters.CurrentPoint + x1 * searchParameters.SearchDirection).FunctionValue < (searchParameters.CurrentPoint + x2 * searchParameters.SearchDirection).FunctionValue)
                    searchParameters.alphaB = x2;
                else
                    searchParameters.alphaA = x1;
                counter++;
            }
            while (Math.Abs(searchParameters.alphaB - searchParameters.alphaA) > searchParameters.DiscretizationError && counter < max);

            searchParameters.alpha = (searchParameters.alphaA + searchParameters.alphaB) / 2;
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;

            return searchParameters;
        }    
        /// <summary>
        /// Метод Больцано
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static Search Bolzano(Search search, int max)
        {
            int counter = 0;
            Search searchParameters = new Search(search);

            do
            {
                searchParameters.alpha = (searchParameters.alphaA + searchParameters.alphaB) / 2;
                if (Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alpha 
                    * searchParameters.SearchDirection, searchParameters.SearchDirection) > 0)
                    searchParameters.alphaB = searchParameters.alpha;
                else
                    searchParameters.alphaA = searchParameters.alpha;
                counter++;
            }
            while (Math.Abs(searchParameters.alphaA - searchParameters.alphaB) > searchParameters.DiscretizationError 
            && Math.Abs(Operations.Derivative(searchParameters.CurrentPoint 
            + searchParameters.alpha * searchParameters.SearchDirection, searchParameters.SearchDirection)) > searchParameters.DiscretizationError 
            && counter < max);

            searchParameters.alpha = (searchParameters.alphaA + searchParameters.alphaB) / 2;
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;

            return searchParameters;
        }
        /// <summary>
        /// Метод Фибоначчи 1
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search Fibonacci1(Search search, int max)
        {
            Search searchParameters = new Search(search);

            int counter = 1;
            double ln = 0.01 * searchParameters.DiscretizationError, n = 0, x1, x2;
            while (Operations.Fibonacci(n) < (searchParameters.alphaB - searchParameters.alphaA) / ln)
                n++;
            x1 = searchParameters.alphaA + Operations.Fibonacci(n - 2) 
                * (searchParameters.alphaB - searchParameters.alphaA) / Operations.Fibonacci(n);
            x2 = searchParameters.alphaA + Operations.Fibonacci(n - 1) 
                * (searchParameters.alphaB - searchParameters.alphaA) / Operations.Fibonacci(n);
            do
            {
                if ((searchParameters.CurrentPoint + x1 * searchParameters.SearchDirection).FunctionValue 
                    < (searchParameters.CurrentPoint + x2 * searchParameters.SearchDirection).FunctionValue)
                {
                    searchParameters.alphaB = x2;
                    x2 = x1;
                    x1 = searchParameters.alphaA + Operations.Fibonacci(n - 2 - counter) 
                        * (searchParameters.alphaB - searchParameters.alphaA) / Operations.Fibonacci(n - counter);
                }
                else
                {
                    searchParameters.alphaA = x1;
                    x1 = x2;
                    x2 = searchParameters.alphaA + Operations.Fibonacci(n - 1 - counter) 
                        * (searchParameters.alphaB - searchParameters.alphaA) / Operations.Fibonacci(n - counter);
                }
                counter++;
            }
            while (counter != n && counter < max);
            if ((searchParameters.CurrentPoint + x1 * searchParameters.SearchDirection).FunctionValue 
                <= (searchParameters.CurrentPoint + x2 * searchParameters.SearchDirection).FunctionValue)
            {
                searchParameters.alphaA = x1;
            }
            else
            {
                searchParameters.alphaB = x1;
            }

            searchParameters.alpha = (searchParameters.alphaA + searchParameters.alphaB) / 2;
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;

            return searchParameters;
        }
        /// <summary>
        /// Метод Фибоначчи 2
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search Fibonacci2(Search search, int max)
        {
            Search searchParameters = new Search(search);

            double x1, x2;
            double ln = 0.001 * searchParameters.DiscretizationError;
            int n = 0;
            int counter = 0;
            while (Operations.Fibonacci(n) < (searchParameters.alphaB - searchParameters.alphaA) / ln)
            {
                n++;
            }
            x1 = searchParameters.alphaA + Operations.Fibonacci(n - 1) * (searchParameters.alphaB - searchParameters.alphaA) 
                / Operations.Fibonacci(n) + Math.Pow((-1.0), n) * searchParameters.DiscretizationError / Operations.Fibonacci(n);
            while (counter < n && counter < max)
            {
                x2 = searchParameters.alphaA + searchParameters.alphaB - x1;
                if ((searchParameters.CurrentPoint + x1 * searchParameters.SearchDirection).FunctionValue 
                    < (searchParameters.CurrentPoint + x2 * searchParameters.SearchDirection).FunctionValue)
                {
                    if (x1 < x2)
                        searchParameters.alphaB = x2;
                    else
                        searchParameters.alphaA = x2;
                }
                else
                {
                    if (x1 < x2)
                        searchParameters.alphaA = x1;
                    else
                        searchParameters.alphaB = x1;
                    x1 = x2;
                }
                counter++;
            }

            searchParameters.alpha = (searchParameters.alphaA + searchParameters.alphaB) / 2;
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;

            return searchParameters;
        }
        /// <summary>
        /// Метод золотого сечения 1
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search GoldenRatio1(Search search, int max)
        {
            Search searchParameters = new Search(search);
            //0.6
            double goldenNumber1 = (Math.Sqrt((double)5) - 1) / 2;
            //0.3
            double goldenNumber2 = (3 - Math.Sqrt((double)5)) / 2;
            //first point
            double x1 = searchParameters.alphaA + goldenNumber2 * Math.Abs(searchParameters.alphaA - searchParameters.alphaB);
            //second point
            double x2 = searchParameters.alphaA + goldenNumber1 * Math.Abs(searchParameters.alphaA - searchParameters.alphaB);
            int k = 0;
            while (Math.Abs(searchParameters.alphaB - searchParameters.alphaA) >= searchParameters.DiscretizationError && k < max)
            {
                if ((searchParameters.CurrentPoint + x1 * searchParameters.SearchDirection).FunctionValue 
                    < (searchParameters.CurrentPoint + x2 * searchParameters.SearchDirection).FunctionValue)
                {
                    searchParameters.alphaB = x2;
                    x2 = x1;
                    x1 = searchParameters.alphaA + goldenNumber2 * Math.Abs(searchParameters.alphaA - searchParameters.alphaB);
                }
                else
                {
                    searchParameters.alphaA = x1;
                    x1 = x2;
                    x2 = searchParameters.alphaA + goldenNumber1 * Math.Abs(searchParameters.alphaA - searchParameters.alphaB);
                }
                k++;
            }

            searchParameters.alpha = (searchParameters.alphaA + searchParameters.alphaB) / 2;
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;

            return searchParameters;
        }
        /// <summary>
        /// Метод золотого сечения 2
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search GoldenRatio2(Search search, int max)
        {
            Search searchParameters = new Search(search);
            //0.6
            double goldenNumber = (Math.Sqrt(5) - 1) / 2; 
            //первая точка
            double x1 = searchParameters.alphaA + goldenNumber * Math.Abs(searchParameters.alphaA - searchParameters.alphaB);
            //вторая точка
            double x2 = searchParameters.alphaA + searchParameters.alphaB - x1;
            int counter = 0;
            while (Math.Abs(searchParameters.alphaB - searchParameters.alphaA) > searchParameters.DiscretizationError 
                && Math.Abs((searchParameters.alphaA + searchParameters.alphaB) / 2) > searchParameters.DiscretizationError && counter < max)
            {
                x2 = searchParameters.alphaA + searchParameters.alphaB - x1;
                if ((searchParameters.CurrentPoint + x1 * searchParameters.SearchDirection).FunctionValue 
                    > (searchParameters.CurrentPoint + x2 * searchParameters.SearchDirection).FunctionValue)
                {
                    if (x1 > x2)
                        searchParameters.alphaB = x1;
                    else
                        searchParameters.alphaA = x1;
                    x1 = searchParameters.alphaA + goldenNumber * Math.Abs(searchParameters.alphaA - searchParameters.alphaB);
                }
                else
                {
                    if (x1 > x2)
                        searchParameters.alphaA = x2;
                    else
                        searchParameters.alphaB = x2;
                }
                counter++;
            }

            searchParameters.alpha = (searchParameters.alphaA + searchParameters.alphaB) / 2;
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;

            return searchParameters;
        }
        /// <summary>
        /// Метод Трехточечного поиска
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search ThreePointSearch(Search search, int max)
        {
            Search searchParameters = new Search(search);

            double pointCenter = (searchParameters.alphaA + searchParameters.alphaB) / 2, step, pointLeft, pointRight;
            int counter = 1;
            do
            {
                step = Math.Abs(searchParameters.alphaB - searchParameters.alphaA);
                pointLeft = searchParameters.alphaA + step / 4;
                pointRight = searchParameters.alphaB - step / 4;
                if ((searchParameters.CurrentPoint + pointLeft * searchParameters.SearchDirection).FunctionValue 
                    < (searchParameters.CurrentPoint + pointCenter * searchParameters.SearchDirection).FunctionValue)
                {
                    searchParameters.alphaB = pointCenter;
                    pointCenter = pointLeft;
                }
                else
                {
                    if ((searchParameters.CurrentPoint + pointLeft * searchParameters.SearchDirection).FunctionValue 
                        >= (searchParameters.CurrentPoint + pointCenter * searchParameters.SearchDirection).FunctionValue 
                        && (searchParameters.CurrentPoint + pointCenter * searchParameters.SearchDirection).FunctionValue 
                        <= (searchParameters.CurrentPoint + pointRight * searchParameters.SearchDirection).FunctionValue)
                    {
                        searchParameters.alphaA = pointLeft;
                        searchParameters.alphaB = pointRight;
                    }
                    else
                    {
                        searchParameters.alphaA = pointCenter;
                        pointCenter = pointRight;
                    }
                }
                counter++;
            }
            while (Math.Abs(searchParameters.alphaB - searchParameters.alphaA) > searchParameters.DiscretizationError && counter < max);

            searchParameters.alpha = (searchParameters.alphaA + searchParameters.alphaB) / 2;
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;

            return searchParameters;
        }
        /// <summary>
        /// Метод Дэвиса-Свенна-Кэмпи
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static Search Dsc(Search search, int max)
        {
            Search searchParameters = new Search(search);

            double center = (searchParameters.alphaA + searchParameters.alphaB) / 2, step = 0.00001, expectedMin;
            searchParameters.alphaA = center - step;
            searchParameters.alphaB = center + step;
            int count = 1;
            expectedMin = Operations.interpolationFunctions(4, searchParameters.alphaA, center, searchParameters.alphaB, 
                searchParameters.CurrentPoint, searchParameters.SearchDirection);
            while (Math.Abs((center - expectedMin) / center) > searchParameters.DiscretizationError
                && (Math.Abs(((searchParameters.CurrentPoint + center * searchParameters.SearchDirection).FunctionValue 
                - (searchParameters.CurrentPoint + expectedMin * searchParameters.SearchDirection).FunctionValue) 
                / (searchParameters.CurrentPoint + center * searchParameters.SearchDirection).FunctionValue) 
                > searchParameters.DiscretizationError
                && Math.Abs(Operations.Derivative(searchParameters.CurrentPoint + expectedMin
                * searchParameters.SearchDirection, searchParameters.SearchDirection)) > searchParameters.DiscretizationError) 
                && count < max)
            {
                step = step / 2;
                if ((searchParameters.CurrentPoint + center * searchParameters.SearchDirection).FunctionValue 
                    > (searchParameters.CurrentPoint + expectedMin * searchParameters.SearchDirection).FunctionValue)
                    center = expectedMin;
                searchParameters.alphaA = center - step;
                searchParameters.alphaB = center + step;
                expectedMin = Operations.interpolationFunctions(4, searchParameters.alphaA, center, searchParameters.alphaB, 
                    searchParameters.CurrentPoint, searchParameters.SearchDirection);
                count++;
            }
            searchParameters.alphaA = expectedMin;
            searchParameters.alphaB = expectedMin;
            searchParameters.alpha = (searchParameters.alphaA + searchParameters.alphaB) / 2;
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;

            return searchParameters;
        }
        /// <summary>
        /// Метод Линейной интерполяции
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search LinearInterpolation(Search search, int max)
        {
            Search searchParameters = new Search(search);
            int counter = 1;
            searchParameters.alpha = searchParameters.alphaB 
                - Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alphaB 
                * searchParameters.SearchDirection, searchParameters.SearchDirection) 
                * (searchParameters.alphaB - searchParameters.alphaA) 
                / (Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alphaB 
                * searchParameters.SearchDirection, searchParameters.SearchDirection) 
                - Operations.Derivative(searchParameters.CurrentPoint 
                + searchParameters.alphaA * searchParameters.SearchDirection, searchParameters.SearchDirection));
            while (Math.Abs(Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alpha 
                * searchParameters.SearchDirection, searchParameters.SearchDirection)) 
                > searchParameters.DiscretizationError 
                && counter < max)
            {
                if (Operations.Derivative(searchParameters.CurrentPoint 
                    + searchParameters.alpha * searchParameters.SearchDirection, searchParameters.SearchDirection) > 0)
                    searchParameters.alphaB = searchParameters.alpha;
                else
                    searchParameters.alphaA = searchParameters.alpha;
                searchParameters.alpha = searchParameters.alphaB 
                    - Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alphaB * searchParameters.SearchDirection, searchParameters.SearchDirection) 
                    * (searchParameters.alphaB - searchParameters.alphaA) 
                    / (Operations.Derivative(searchParameters.CurrentPoint 
                    + searchParameters.alphaB * searchParameters.SearchDirection, searchParameters.SearchDirection) 
                    - Operations.Derivative(searchParameters.CurrentPoint 
                    + searchParameters.alphaA * searchParameters.SearchDirection, searchParameters.SearchDirection));
                counter++;
            }
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;
            return searchParameters;
        }
        /// <summary>
        /// Метод Пауэлла
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search Powell(Search search, int max)
        {
            Search searchParameters = new Search(search);

            double centerPoint = (searchParameters.alphaA + searchParameters.alphaB) / 2, expectedMin;
            int counter = 1;
            expectedMin = Operations.interpolationFunctions(1, searchParameters.alphaA, centerPoint, searchParameters.alphaB, searchParameters.CurrentPoint, searchParameters.SearchDirection);
            while (Math.Abs((centerPoint - expectedMin) / centerPoint) > searchParameters.DiscretizationError 
                && (Math.Abs(((searchParameters.CurrentPoint + centerPoint * searchParameters.SearchDirection).FunctionValue 
                - (searchParameters.CurrentPoint + expectedMin * searchParameters.SearchDirection).FunctionValue) 
                / (searchParameters.CurrentPoint + centerPoint * searchParameters.SearchDirection).FunctionValue) > searchParameters.DiscretizationError) 
                && counter < max
                && Math.Abs(Operations.Derivative(searchParameters.CurrentPoint + expectedMin 
                * searchParameters.SearchDirection, searchParameters.SearchDirection)) > searchParameters.DiscretizationError)
            {
                if ((searchParameters.CurrentPoint + centerPoint * searchParameters.SearchDirection).FunctionValue 
                    > (searchParameters.CurrentPoint + expectedMin * searchParameters.SearchDirection).FunctionValue)
                {
                    if (centerPoint < expectedMin)
                        searchParameters.alphaA = centerPoint;
                    else
                        searchParameters.alphaB = centerPoint;
                    centerPoint = expectedMin;
                }
                else
                {
                    if (centerPoint < expectedMin)
                        searchParameters.alphaB = expectedMin;
                    else
                        searchParameters.alphaA = expectedMin;
                }
                expectedMin = Operations.interpolationFunctions(4, searchParameters.alphaA, centerPoint, searchParameters.alphaB, searchParameters.CurrentPoint, searchParameters.SearchDirection);
                counter++;
            }
            searchParameters.alphaA = expectedMin;
            searchParameters.alphaB = expectedMin;
            searchParameters.alpha = (searchParameters.alphaA + searchParameters.alphaB) / 2;
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;

            return searchParameters;
        }
        /// <summary>
        /// Метод бикубической интерполяции
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search BicubicInterpolation(Search search, int max)
        {
            Search searchParameters = new Search(search);
            int counter = 1;
            double expectedMin = Operations.interpolationFunctions(5, searchParameters.alphaA, searchParameters.alphaB, 0, searchParameters.CurrentPoint, searchParameters.SearchDirection);
            while (Math.Abs(Operations.Derivative(searchParameters.CurrentPoint + expectedMin * searchParameters.SearchDirection, searchParameters.SearchDirection)) > searchParameters.DiscretizationError && counter < max)
            {
                if (Operations.Derivative(searchParameters.CurrentPoint + expectedMin * searchParameters.SearchDirection, searchParameters.SearchDirection) > 0)
                    searchParameters.alphaB = expectedMin;
                else
                    searchParameters.alphaA = expectedMin;
                expectedMin = Operations.interpolationFunctions(5, searchParameters.alphaA, searchParameters.alphaB, 0, searchParameters.CurrentPoint, searchParameters.SearchDirection);
                counter++;
            }
            searchParameters.alpha = expectedMin;
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;
            return searchParameters;
        }
        /// <summary>
        /// Метод Экстраполяции-Интерполяции
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static Search Ei(Search search, int max)
        {
            Search searchParameters = new Search(search);
            int counter = 1;
            searchParameters.alpha = searchParameters.alphaB 
                - Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alphaB 
                * searchParameters.SearchDirection, searchParameters.SearchDirection) 
                * (searchParameters.alphaB - searchParameters.alphaA) 
                / (Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alphaB 
                * searchParameters.SearchDirection, searchParameters.SearchDirection) 
                - Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alphaA 
                * searchParameters.SearchDirection, searchParameters.SearchDirection));
            while (Math.Abs(Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alpha 
                * searchParameters.SearchDirection, searchParameters.SearchDirection)) > searchParameters.DiscretizationError 
                && counter < max)
            {
                if (Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alpha 
                    * searchParameters.SearchDirection, searchParameters.SearchDirection) > 0)
                {
                    searchParameters.alphaB = searchParameters.alpha;
                }
                else
                {
                    searchParameters.alphaA = searchParameters.alpha;
                }
                searchParameters.alpha = searchParameters.alphaB 
                    - Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alphaB 
                    * searchParameters.SearchDirection, searchParameters.SearchDirection) 
                    * (searchParameters.alphaB - searchParameters.alphaA) 
                    / (Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alphaB 
                    * searchParameters.SearchDirection, searchParameters.SearchDirection) 
                    - Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alphaA 
                    * searchParameters.SearchDirection, searchParameters.SearchDirection));
                counter++;
            }

            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha 
                * searchParameters.SearchDirection;

            return searchParameters;
        }
        /// <summary>
        /// Метод Ньютона
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static Search Newtoon(Search search, int max)
        {
            //метод не работает
            Search searchParameters = new Search(search);
            searchParameters.alpha = (searchParameters.alphaA+searchParameters.alphaB) / 2;
            int counter = 0;
            while (Math.Abs(Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alpha 
                * searchParameters.SearchDirection, searchParameters.SearchDirection)) > searchParameters.DiscretizationError 
                && counter < max)
            {
                searchParameters.alpha -= (searchParameters.CurrentPoint + searchParameters.alpha 
                    * searchParameters.SearchDirection).FunctionValue 
                    / Operations.Derivative(searchParameters.CurrentPoint + searchParameters.alpha 
                    * searchParameters.SearchDirection, searchParameters.SearchDirection);
                counter++;
            }
            searchParameters.CurrentPoint = searchParameters.CurrentPoint + searchParameters.alpha * searchParameters.SearchDirection;

            return searchParameters;
        }
        /// <summary>
        /// Метод Нелдера-Мида
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static SearchResults NelderMead(Search search)
        {
            Search searchParameters = new Search(search);
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
                simplexX[i] = new Point(searchParameters.StartPoint);
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
                searchParameters.Iterations++;
                if (Operations.Norm(Operations.Gradient(simplexX[0])) < searchParameters.DiscretizationError
                    || searchParameters.Iterations >= searchParameters.MaxIterations)
                {
                    SearchResults results = new SearchResults(searchParameters);
                    results.EndPoint = simplexX[0];
                    return results;
                }
            }
            while (true);
        }
        /// <summary>
        /// Метод коши
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static SearchResults Cauchy(Search search)
        {
            Search searchParameters = new Search(search);

            do
            {
                searchParameters.SearchDirection = -(Operations.Gradient(searchParameters.CurrentPoint));
                //находим локальный минимум 
                searchParameters = ComboSearch(searchParameters);
                searchParameters.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(searchParameters.CurrentPoint)) > searchParameters.DiscretizationError
            && searchParameters.Iterations < searchParameters.MaxIterations);

            SearchResults results = new SearchResults(searchParameters);
            return results;
        }
        /// <summary>
        /// Метод циклического покоординатного спуска
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static SearchResults CyclicCoordinateWiseDescent(Search search)
        {
            Search searchParameters = new Search(search);

            do
            {
                //строим ортогональные направления по ЦПС
                searchParameters.SearchDirection = Operations.InitialDirection(searchParameters.StartPoint.sizeCol, searchParameters.StartPoint.sizeRow);
                //проходим по всем направлениям
                for (int i = 0; i < searchParameters.StartPoint.sizeCol * searchParameters.StartPoint.sizeRow; i++)
                {
                    //находим локальный минимум
                    searchParameters = ComboSearch(searchParameters);                    
                    //изменяем направление по оси (ЦПС)
                    searchParameters.SearchDirection = Operations.InvertingDirection(searchParameters.SearchDirection);
                }

                searchParameters.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(searchParameters.CurrentPoint)) > searchParameters.DiscretizationError
            && searchParameters.Iterations < searchParameters.MaxIterations);

            SearchResults results = new SearchResults(searchParameters);
            return results;
        }
        /// <summary>
        /// Метод Партан 1
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static SearchResults Partan1(Search search)
        {
            Search searchParameters = new Search(search);

            Point x0 = searchParameters.StartPoint;
            Point x1 = searchParameters.StartPoint;
            Point x2 = searchParameters.StartPoint;
            Point x3 = searchParameters.StartPoint;

            searchParameters.SearchDirection = -(Operations.Gradient(searchParameters.CurrentPoint));
            //находим локальный минимум 
            searchParameters = ComboSearch(searchParameters);

            do
            {
                searchParameters.SearchDirection = -(Operations.Gradient(searchParameters.CurrentPoint));
                //находим локальный минимум 
                searchParameters = ComboSearch(searchParameters);

                x2 = new Point(searchParameters.CurrentPoint);
                x3 = x2 + searchParameters.alpha * (x2 - x0);
                x0 = new Point(x1);
                x1 = new Point(x3);
                searchParameters.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(x3)) > searchParameters.DiscretizationError
            && searchParameters.Iterations < searchParameters.MaxIterations);

            SearchResults results = new SearchResults(searchParameters);
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
            //метод не работает
            Search searchParameters = new Search(search);
            SearchResults results = new SearchResults(searchParameters);
            return results;
        }
        /// <summary>
        /// Методы сопряженных градиентов
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static SearchResults NonlinearConjugateGradient(Search search)
        {
            Search searchParameters = new Search(search);

            Point oldPoint = new Point();
            Point oldDirection = new Point();
            double beta = 0;
            do
            {
                if (searchParameters.Iterations % 2 == 0)
                    searchParameters.SearchDirection = -(Operations.Gradient(searchParameters.CurrentPoint));
                else
                {
                    beta = Operations.NonlinearConjugateGradientFunctions(oldDirection, oldPoint, searchParameters.CurrentPoint, searchParameters.IndexMethod);

                    searchParameters.SearchDirection = -(Operations.Gradient(searchParameters.CurrentPoint)) + beta * searchParameters.SearchDirection;
                }

                searchParameters.SearchDirection = Operations.UnitVector(searchParameters.SearchDirection);

                oldPoint = searchParameters.CurrentPoint;
                oldDirection = searchParameters.SearchDirection;

                searchParameters = ComboSearch(searchParameters);
               
                searchParameters.Iterations++;
            }
            while (Operations.Norm(
                Operations.Gradient(searchParameters.CurrentPoint)) > searchParameters.DiscretizationError
                && searchParameters.Iterations < searchParameters.MaxIterations);

            SearchResults results = new SearchResults(searchParameters);
            return results;
        }
        /// <summary>
        /// Метод Хукка-Дживса
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static SearchResults HookeJeeves(Search search)
        {
            //method dont work
            Search searchParameters = new Search(search);

            double step = 1;
            Point x = new Point(searchParameters.StartPoint);
            Point x4 = new Point(searchParameters.StartPoint);
            Point x3 = new Point(searchParameters.StartPoint);
            Point x2 = new Point(searchParameters.StartPoint);
            Point leftPoint = new Point(searchParameters.StartPoint);
            Point rightPoint = new Point(searchParameters.StartPoint);

            do
            {
                double test = step;

                Operations.Search(x, leftPoint, rightPoint, x2, test);
                while (x2.FunctionValue > x.FunctionValue)
                {
                    test /= 2;
                    Operations.Search(x, leftPoint, rightPoint, x2, test);
                    if (test < searchParameters.DiscretizationError)
                        break;
                }

                x3 = 2 * x2 - x;

                Operations.Search(x3, leftPoint, rightPoint, x4, test);

                if (x4.FunctionValue < x3.FunctionValue)
                {
                    x = new Point(x2);
                    x2 = new Point(x4);
                }
                else
                    step /= 2;

                searchParameters.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(x2)) > searchParameters.DiscretizationError 
            && searchParameters.Iterations < searchParameters.MaxIterations);

            SearchResults results = new SearchResults(searchParameters);
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
            Search searchParameters = new Search(search);

            Point A = new Point(search.StartPoint.matrix.Length, search.StartPoint.matrix.Length, search.Function);

            Point oldPoint = new Point();
            do
            {
                A = Operations.VariableMetricMethodsAncillary(A, oldPoint, search.CurrentPoint, search.Iterations, search.IndexMethod);
                search.SearchDirection = -(Operations.Gradient(search.CurrentPoint)) * A;
                oldPoint = search.CurrentPoint;
                search = ComboSearch(search);
                search.Iterations++;
            }
            while (Operations.Norm(Operations.Gradient(search.CurrentPoint)) > search.DiscretizationError && search.Iterations < search.MaxIterations);

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
            Search searchParameters = ComboSearch(search);
            searchParameters.Iterations++;
            SearchResults results = new SearchResults(searchParameters);
            return results;
        }
        /// <summary>
        /// Обобщенный метод Ньютона
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static SearchResults Newtoon(Search search)
        {
            Search searchParameters = ComboSearch(search);

            Point dx = new Point();
            do
            {
                dx = Operations.Gradient(searchParameters.CurrentPoint) 
                    * Operations.InvertibleMatrix(Operations.HessianMatrix(searchParameters.CurrentPoint));
                searchParameters.CurrentPoint = searchParameters.CurrentPoint - dx;
                searchParameters.Iterations++;
            }
            while (
            Operations.Norm(Operations.Gradient(searchParameters.CurrentPoint)) > searchParameters.DiscretizationError
            && searchParameters.Iterations < searchParameters.MaxIterations);

            SearchResults results = new SearchResults(searchParameters);
            return results;
        }
        /// <summary>
        /// Метод Розенброка
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static SearchResults Rosenbrock(Search search)
        {
            Search searchParameters = ComboSearch(search);

            //массивы для хранения точек, направлений и единичных векторов
            Point[] Pi = new Point[searchParameters.StartPoint.sizeCol * searchParameters.StartPoint.sizeRow];
            Point[] ei = new Point[searchParameters.StartPoint.sizeCol * searchParameters.StartPoint.sizeRow];
            //начальная точка
            Point startPoint = new Point(searchParameters.StartPoint);
            //конечная точка
            Point endPoint = new Point(searchParameters.StartPoint);
            //строим ортогональные направления по ЦПС
            searchParameters.SearchDirection = Operations.InitialDirection(searchParameters.StartPoint.sizeCol, searchParameters.StartPoint.sizeRow);
            //проходим по всем направлениям
            for (int i = 0; i < Pi.Length; i++)
            {
                //находим локальный минимум
                searchParameters = ComboSearch(searchParameters);

                //добавляем направление в массив
                if (searchParameters.alpha == 0)
                {
                    Pi[i] = searchParameters.SearchDirection;
                }
                else
                {
                    Pi[i] = searchParameters.alpha * searchParameters.SearchDirection;
                }
                //изменяем направление по оси (ЦПС)
                searchParameters.SearchDirection = Operations.InvertingDirection(searchParameters.SearchDirection);
            }

            searchParameters.Iterations++;
            //запоминаем конечную точку
            endPoint = new Point(searchParameters.CurrentPoint);


            while (Operations.Norm(Operations.Gradient(searchParameters.CurrentPoint)) > searchParameters.DiscretizationError
                && searchParameters.Iterations < searchParameters.MaxIterations)
            {
                //вычисляем новое направление поиска Bi, i = 1
                Pi[0] = endPoint - startPoint;
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
                startPoint = new Point(endPoint);

                //выполняем поиск по выбранным направлениям
                for (int i = 0; i < Pi.Length; i++)
                {
                    //изменяем направление по оси
                    searchParameters.SearchDirection = new Point(ei[i]);
                    //добавляем направление в массив
                    Pi[i] = searchParameters.SearchDirection;
                    //находим локальный минимум
                    searchParameters = ComboSearch(searchParameters);
                }
                searchParameters.Iterations++;
                //запоминаем конечную точку
                endPoint = new Point(searchParameters.CurrentPoint);
            }

            SearchResults results = new SearchResults(searchParameters);
            return results;
        }
        /// <summary>
        /// Метод Пауэлла 2
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        private static SearchResults Powell2(Search search)
        {
            Search searchParameters = ComboSearch(search);

            //массивы для хранения точек, направлений и единичных векторов
            Point[] Pi = new Point[searchParameters.StartPoint.sizeCol * searchParameters.StartPoint.sizeRow + 1];
            Point[] ei = new Point[searchParameters.StartPoint.sizeCol * searchParameters.StartPoint.sizeRow + 1];
            Point[] Xi = new Point[searchParameters.StartPoint.sizeCol * searchParameters.StartPoint.sizeRow + 1];
            //строим ортогональные направления по ЦПС
            searchParameters.SearchDirection = Operations.InitialDirection(searchParameters.StartPoint.sizeCol, searchParameters.StartPoint.sizeRow);
            //проходим по всем направлениям
            for (int i = 0; i < Pi.Length - 1; i++)
            {
                //находим локальный минимум
                searchParameters = ComboSearch(searchParameters);
                //добавляем направление в массив
                Xi[i] = new Point(searchParameters.CurrentPoint);
                Pi[i] = searchParameters.SearchDirection;
                //изменяем направление по оси (ЦПС)
                searchParameters.SearchDirection = Operations.InvertingDirection(searchParameters.SearchDirection);
            }

            //последнее направление стоится как первое
            Pi[Pi.Length - 1] = Pi[0];
            //и выполняем по этому направлению поиск
            searchParameters.SearchDirection = Pi[0];
            //находим локальный минимум
            searchParameters = ComboSearch(searchParameters);
            //добавляем направление в массив
            Pi[Pi.Length - 1] = searchParameters.SearchDirection;
            //запоминаем координату минимума
            Xi[Pi.Length - 1] = new Point(searchParameters.CurrentPoint);
            searchParameters.Iterations++;

            while (searchParameters.Iterations < searchParameters.MaxIterations 
                && Operations.Norm(Operations.Gradient(searchParameters.CurrentPoint)) > searchParameters.DiscretizationError)
            {
                Pi[0] = Xi.Last() - Xi[0];
                //Pi[0] = Operations.UnitVector(Pi[0]);

                Pi[Pi.Length - 1] = Pi[0];

                for (int i = 1; i < Pi.Length - 2; i++)
                {
                    Pi[i] = Pi[i + 1];
                }

                //выполняем поиск по выбранным направлениям
                for (int i = 0; i < Pi.Length; i++)
                {
                    //изменяем направление по оси
                    searchParameters.SearchDirection = new Point(Pi[i]);
                    //добавляем направление в массив
                    Pi[i] = searchParameters.SearchDirection;

                    //находим локальный минимум
                    searchParameters = ComboSearch(searchParameters);
                    //запоминаем координату минимума
                    Xi[i] = new Point(searchParameters.CurrentPoint);
                }
                searchParameters.Iterations++;
            }

            SearchResults results = new SearchResults(searchParameters);
            return results;
        }
        /// <summary>
        /// комбинированная стратегия поиска
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private static Search ComboSearch(Search search)
        {
            const int maxIterations = 5;
            Search intervalSearchParameters = Svenn(search);
            Search reduceIntervalSearchParameters = Dichotomy(intervalSearchParameters, maxIterations);
            Search minimumSearchParameters = Ei(reduceIntervalSearchParameters, maxIterations);
            return minimumSearchParameters;
        }
    }
}

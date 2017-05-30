using OptimisationMethods.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OptimisationMethods.CLR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Введите начальную точку:");
                    String x0 = Console.ReadLine();
                    Console.WriteLine("Выберете функцию:");
                    int function = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Выберете метод:");
                    Console.WriteLine("1) Даниела");
                    Console.WriteLine("2) Полака-Рибьера");
                    Console.WriteLine("3) Флетчера-Ривса");
                    Console.WriteLine("4) Диксона");
                    int method = Convert.ToInt32(Console.ReadLine());

                    if (method < 0 || method > 4)
                    {
                        Console.WriteLine("Неверно указан метод!");
                        Console.ReadKey();
                        throw new Exception();
                    }
                    else
                    {
                        method += 14;
                    }

                    double e = Math.Pow(10, -5);
                    Console.WriteLine("Выберете Максимальное число итераций:");
                    int max = Convert.ToInt32(Console.ReadLine());

                    //поиск минимума выбранным методом
                    SearchResults results = SearchPoint(function, method, e, max, x0, x0);
                    //вывод результатов
                    string result = "Количество итераций: " + results.Iterations + " , минимум: ";
                    foreach (double min in results.EndPoint.matrix)
                    {
                        result += min.ToString("F7") + " ; ";
                    }

                    Console.WriteLine(result);
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }

        private static SearchResults SearchPoint(int functionIndex, int indexMethod, double error, int maxIteration, string startPointText, string searchDirectionText)
        {
            string[] startPointString = startPointText.Split(';');
            string[] startDirectionString = searchDirectionText.Split(';');
            Point startPoint = new Point(1, startPointString.Length, functionIndex);
            Point startDirection = new Point(1, startPointString.Length, functionIndex);
            for (int i = 0; i < startPointString.Length; i++)
            {
                if (i < startPointString.Length)
                {
                    startPoint.matrix[0, i] = Double.Parse(startPointString[i]);
                }
                if (i < startDirectionString.Length)
                {
                    startDirection.matrix[0, i] = Double.Parse(startDirectionString[i]);
                }
            }
            //создаем экземпляр сущности поиска
            Search search = new Search(startPoint, startDirection, error, functionIndex, maxIteration, indexMethod);
            //ищем выбранным методом
            SearchResults results = Methods.Method(search);
            //=возвращаем результаты
            return results;
        }
    }
}

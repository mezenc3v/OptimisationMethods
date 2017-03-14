namespace OptimisationMethods
{
    public class Search
    {
        /// <summary>
        /// Начальная точка
        /// </summary>
        public Point StartPoint { get;}
        /// <summary>
        /// Направление поиска
        /// </summary>
        public Point SearchDirection { get; set; }
        /// <summary>
        /// номер функции
        /// </summary>
        public int Function { get;}
        /// <summary>
        /// Максимальное число итераций
        /// </summary>
        public int MaxIterations { get;}
        /// <summary>
        /// Погрешность поиска минимума
        /// </summary>
        public double Error { get; }
        /// <summary>
        /// Текущее значение минимума
        /// </summary>
        public Point CurrentPoint { get; set; }
        /// <summary>
        /// левый шаг промежутка минимума
        /// </summary>
        public double alphaA { get; set; }
        /// <summary>
        /// Правый шаг промежутка минимума
        /// </summary>
        public double alphaB { get; set; }
        /// <summary>
        /// шаг до минимума
        /// </summary>
        public double alpha { get; set; }
        /// <summary>
        /// Число итераций
        /// </summary>
        public int Iterations { get; set; }
        public Search(Point start, Point direction, double error, int funcion, int maxIterations)
        {
            StartPoint = start;
            CurrentPoint = StartPoint;
            Iterations = 0;
            SearchDirection = direction;
            Error = error;
            MaxIterations = maxIterations;
            Function = funcion;
        }
        
    }
}

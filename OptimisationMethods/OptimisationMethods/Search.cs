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
        public double DiscretizationError { get; }
        /// <summary>
        /// Текущее значение минимума
        /// </summary>
        public Point CurrentPoint;
        /// <summary>
        /// левый шаг промежутка минимума
        /// </summary>
        public double alphaA;
        /// <summary>
        /// Правый шаг промежутка минимума
        /// </summary>
        public double alphaB;
        /// <summary>
        /// шаг до минимума
        /// </summary>
        public double alpha;
        /// <summary>
        /// Число итераций
        /// </summary>
        public int Iterations;
        /// <summary>
        /// номер метода
        /// </summary>
        public int IndexMethod;
        public Search(Search search)
        {
            StartPoint = new Point(search.StartPoint);
            CurrentPoint = new Point(search.CurrentPoint);
            Iterations = search.Iterations;
            SearchDirection = new Point(search.SearchDirection);
            DiscretizationError = search.DiscretizationError;
            MaxIterations = search.MaxIterations;
            Function = search.Function;
            alpha = search.alpha;
            alphaA = search.alphaA;
            alphaB = search.alphaB;
            IndexMethod = search.IndexMethod;
        }
        public Search(Point start, Point direction, double error, int funcion, int maxIterations, int indexMethod)
        {
            StartPoint = start;
            CurrentPoint = StartPoint;
            Iterations = 0;
            SearchDirection = direction;
            DiscretizationError = error;
            MaxIterations = maxIterations;
            Function = funcion;
            IndexMethod = indexMethod;
        }
        
    }
}

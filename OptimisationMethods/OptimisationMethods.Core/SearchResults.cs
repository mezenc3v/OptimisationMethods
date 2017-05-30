using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimisationMethods.Core
{
    public class SearchResults
    {
        /// <summary>
        /// Конечная точка
        /// </summary>
        public Point EndPoint { get; set; }
        /// <summary>
        /// Число итераций
        /// </summary>
        public int Iterations { get; set; }
        public SearchResults(Search search)
        {
            EndPoint = search.CurrentPoint;
            Iterations = search.Iterations;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimisationMethods
{
    public interface ISearch
    {
        Point StartPoint { get; set; }
        Point SearchDirection { get; set; }
        int Function { get; set; }
        int MaxIterations { get; set; }
        double Error { get; set; }
    }
}

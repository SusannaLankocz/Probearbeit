using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementCalculations.ViewModel.Model
{
    public class DataPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public DataPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}

using System.Windows.Documents;

namespace MeasurementCalculations.ViewModel.Model
{
    public class CalculationsService
    {
        public double CalculateStandardDeviation(List<double> yValues)
        {
            if (yValues == null || yValues.Count == 0)
                return 0;

            double mean = CalculateMean(yValues);
            double sumOfSquares = yValues.Sum(yValue => Math.Pow(yValue - mean, 2));
            
            return Math.Sqrt(sumOfSquares / yValues.Count);
        }

        public double CalculateMean(List<double> yValues)
        {
            if (yValues == null || yValues.Count == 0)
                return 0;

            return yValues.Average();
        }

        public List<DerivativePoint> CalculateFirstDerivative(List<DataPoint> dataPoints)
        {
            if (dataPoints == null || dataPoints.Count < 2)
                return new List<DerivativePoint>();

            var derivatives = new List<DerivativePoint>();

            double firstDeriv = (dataPoints[1].Y - dataPoints[0].Y) /
                              (dataPoints[1].X - dataPoints[0].X);
            derivatives.Add(new DerivativePoint(dataPoints[0].X, firstDeriv));

            for (int i = 1; i < dataPoints.Count -1; i++)
            {
                double deltaY = dataPoints[i + 1].Y - dataPoints[i - 1].Y;
                double deltaX = dataPoints[i + 1].X - dataPoints[i - 1].X;

                if (Math.Abs(deltaX) < double.Epsilon)
                    continue;

                double derivative = deltaY / deltaX;
                derivatives.Add(new DerivativePoint(dataPoints[i].X, derivative));
            }

            int last = dataPoints.Count - 1;
            double lastDeriv = (dataPoints[last].Y - dataPoints[last - 1].Y) /
                              (dataPoints[last].X - dataPoints[last - 1].X);
            derivatives.Add(new DerivativePoint(dataPoints[last].X, lastDeriv));

            return derivatives;
        }
    }
}

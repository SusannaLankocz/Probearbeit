using MeasurementCalculations.ViewModel.Model;
using System.Data;
using System.Windows.Documents;

namespace MeasurementCalculations.ViewModel.Services
{
    public class CalculationsService
    {
        private const int SignificantFigures = 4;

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

            for (int i = 1; i < dataPoints.Count - 1; i++)
            {
                double deltaY = dataPoints[i + 1].Y - dataPoints[i - 1].Y;
                double deltaX = dataPoints[i + 1].X - dataPoints[i - 1].X;

                if (deltaX == 0)
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

        public List<DerivativePoint> CalculateSecondDerivative(List<DataPoint> dataPoints)
        {
            if (dataPoints == null || dataPoints.Count < 3)
                return new List<DerivativePoint>();

            var secondDerivatives = new List<DerivativePoint>();

            for (int i = 1; i < dataPoints.Count - 1; i++)
            {
                double h1 = dataPoints[i].X - dataPoints[i - 1].X;     
                double h2 = dataPoints[i + 1].X - dataPoints[i].X;    

                if (h1 == 0 || h2 == 0)
                    continue;

                double secondDerivative = 2 / (h1 + h2) * (
                    (dataPoints[i+1].Y - dataPoints[i].Y) / h2 -
                    (dataPoints[i].Y - dataPoints[i-1].Y) / h1);

                secondDerivatives.Add(new DerivativePoint(dataPoints[i].X, secondDerivative));
            }

            return secondDerivatives;
        }

        private double RoundToSignificantFigures(double value)
        {
            if (value == 0)
                return 0;

            int decimalPlaces = SignificantFigures - (int)Math.Floor(Math.Log10(Math.Abs(value))) - 1;
            decimalPlaces = Math.Max(0, Math.Min(15, decimalPlaces));

            return Math.Round(value, decimalPlaces, MidpointRounding.AwayFromZero);
        }


        public List<double> RoundYValues(List<double> yValues)
        {
            if (yValues == null || yValues.Count == 0)
                return new List<double>();

            var roundedYValues = new List<double>();

            foreach (var y in yValues)
            {
                roundedYValues.Add(RoundToSignificantFigures(y));
            }

            return roundedYValues;
        }
    }
}


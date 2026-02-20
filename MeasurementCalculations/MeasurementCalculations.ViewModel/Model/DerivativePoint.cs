namespace MeasurementCalculations.ViewModel.Model
{
    public class DerivativePoint
    {
        public double X { get; set; }
        public double Derivative { get; set; }
        
        public DerivativePoint(double x, double firstDerivative)
        {
            X = x;
            Derivative = firstDerivative;
        }
    }
}
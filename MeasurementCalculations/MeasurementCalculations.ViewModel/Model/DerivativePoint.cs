namespace MeasurementCalculations.ViewModel.Model
{
    public class DerivativePoint
    {
        public double X { get; set; }
        public double FirstDerivative { get; set; }
        
        public DerivativePoint(double x, double firstDerivative)
        {
            X = x;
            FirstDerivative = firstDerivative;
        }
    }
}
using MeasurementCalculations.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementCalculations.ViewModel.Test
{
    [TestClass]
    public class CalculationsServiceTest
    {
        private CalculationsService _calculationsService;

        [TestInitialize]
        public void Init()
        {
            _calculationsService = new CalculationsService();
        }


        [DataTestMethod]
        [DataRow(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 }, 3.0)]
        [DataRow(new double[] { 1.375E-07,  0.00446604730468 }, 0.0022330924023399997)]
        [DataRow(new double[] { 1.5E-07,  0.13154654684 }, 0.06577334842)]
        [DataRow(new double[] { 1.6E-07,  -0.02446604730468 }, -0.012232943652340001)]
        public void CalculateMeanWithValidValuesReturnsCorrectMean(double[] data, double expectedResult)
        {
            double actualResult = _calculationsService.CalculateMean(data.ToList());

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

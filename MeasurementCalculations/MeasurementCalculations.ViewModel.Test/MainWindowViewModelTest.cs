using MeasurementCalculations.ViewModel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeasurementCalculations.ViewModel.Test
{
    [TestClass]
    public sealed class MainWindowViewModelTest
    {
        private FileService _fileService;
        private CalculationsService _calculationService;
        private MainWindowViewModel _viewModel;

        [TestInitialize]
        public void Init()
        {
            _fileService = new FileService(); // mock file system for testing
            _calculationService = new CalculationsService();
            _viewModel = new MainWindowViewModel(_fileService, _calculationService);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_viewModel);
        }

        [TestMethod]
        public void LoadFileCommandTest()
        {
            var canExecute = _viewModel.LoadFileCommand.CanExecute(null);

            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void FillMeasurementDataListWithDataTest()
        {
            var dataPoint = new DataPoint(1.0, 0.031715249);

            _viewModel.MeasurementDataList.Add(dataPoint);


            Assert.IsTrue(_viewModel.MeasurementDataList.Contains(dataPoint));
            Assert.AreEqual(1.0, _viewModel.MeasurementDataList[0].X);
            Assert.AreEqual(0.031715249, _viewModel.MeasurementDataList[0].Y);
        }

        [TestMethod]
        public void MeasurementDataList_CanAddMultipleDataPoints()
        {
            var dataPoints = new List<DataPoint>
            {
                new DataPoint(1.0, 0.31715249),
                new DataPoint(2.0, 0.31953731),
                new DataPoint(3.0, 0.31993103)
            };

            foreach (var point in dataPoints)
                _viewModel.MeasurementDataList.Add(point);

            Assert.AreEqual(3, _viewModel.MeasurementDataList.Count);
            Assert.AreEqual(1.0, _viewModel.MeasurementDataList[0].X);
            Assert.AreEqual(2.0, _viewModel.MeasurementDataList[1].X);
            Assert.AreEqual(3.0, _viewModel.MeasurementDataList[2].X);
        }
    }
}

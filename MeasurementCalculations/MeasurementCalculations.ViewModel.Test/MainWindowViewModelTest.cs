using MeasurementCalculations.ViewModel.Model;
using MeasurementCalculations.ViewModel.Services;
using MeasurementCalculations.ViewModel.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeasurementCalculations.ViewModel.Test
{
    [TestClass]
    public sealed class MainWindowViewModelTest
    {
        private FileServiceMock _fileServiceMock;
        private CalculationsService _calculationService;
        private MainWindowViewModel _viewModel;

        [TestInitialize]
        public void Init()
        {
            _fileServiceMock = new FileServiceMock();
            _calculationService = new CalculationsService();
            _viewModel = new MainWindowViewModel(_fileServiceMock, _calculationService);
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
            _fileServiceMock.FilePathToReturn = "test.txt";
            _fileServiceMock.DataPointsToReturn = new List<DataPoint>
            {
                new DataPoint(1.0, 0.31715249),
                new DataPoint(2.0, 0.31953731)
            };

            _viewModel.LoadFileCommand.Execute(null);


            Assert.IsTrue(_fileServiceMock.OpenFileCalled);
            Assert.IsTrue(_fileServiceMock.LoadFileCalled);
            Assert.AreEqual("test.txt", _fileServiceMock.LastLoadFilePathUsed);
            Assert.AreEqual(2, _viewModel.MeasurementData.Count);
            Assert.AreEqual(1.0, _viewModel.MeasurementData[0].X);
            Assert.AreEqual(0.31715249, _viewModel.MeasurementData[0].Y);
        }

        [TestMethod]
        public void ShowStatisticWhenLoadingFileTest()
        {
            _fileServiceMock.FilePathToReturn = "test.txt";
            _fileServiceMock.DataPointsToReturn = new List<DataPoint>
            {
                new DataPoint(1.0, 0.315),
                new DataPoint(2.0, 0.318),
                new DataPoint(3.0, 0.320)
            };

            _viewModel.LoadFileCommand.Execute(null);

            Assert.IsNotNull(_viewModel.MeanYValues);
            Assert.IsNotNull(_viewModel.StandardDeviationYValues);
            Assert.IsNotNull(_viewModel.FirstDerivativeData.Count);
            Assert.IsNotNull(_viewModel.SecondDerivativeData.Count);   
        }
    }
}

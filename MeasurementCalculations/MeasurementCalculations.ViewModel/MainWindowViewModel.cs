using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeasurementCalculations.ViewModel.Model;
using MeasurementCalculations.ViewModel.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace MeasurementCalculations.ViewModel
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        public readonly IFileService _fileService;
        public readonly CalculationsService _calculationsService;

        [ObservableProperty]
        private double _meanYValues;

        [ObservableProperty]
        private double _standardDeviationYValues;

        [ObservableProperty]
        private ObservableCollection<DataPoint> _measurementData = new ObservableCollection<DataPoint>();

        [ObservableProperty]
        private ObservableCollection<DerivativePoint> _firstDerivativeData = new ObservableCollection<DerivativePoint>();

        [ObservableProperty]
        private ObservableCollection<DerivativePoint> _secondDerivativeData = new ObservableCollection<DerivativePoint>();
        
        [ObservableProperty]
        private ObservableCollection<double> _roundedSignificantFiguresYValues = new ObservableCollection<double>();
        
        [ObservableProperty]
        private ObservableCollection<FileProperties> _filePropertiesData = new ObservableCollection<FileProperties>();


        public MainWindowViewModel(IFileService fileService, CalculationsService calculationsService)
        {
            _fileService = fileService;
            _calculationsService = calculationsService;
            LoadFilesProperties();
        }

        private void LoadFilesProperties()
        {
            try
            {
                var fileProperties = _fileService.LoadRepositoryData("C:\\Users\\susan\\Desktop\\Testaufgabe-Verzeichnis");
                foreach (var properties in fileProperties)
                    FilePropertiesData.Add(properties);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading the file properties",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void LoadFile()
        {
            var filePath = _fileService.OpenFile();
            if (filePath != null)
            {
                try
                {
                    var data = _fileService.LoadFile(filePath);

                    foreach (var point in data)
                        MeasurementData.Add(point);

                    CalculateStatistics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error loading file",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CalculateStatistics()
        {
            var yValues = MeasurementData.Select(dp => dp.Y).ToList();
            MeanYValues = _calculationsService.CalculateMean(yValues);
            StandardDeviationYValues = _calculationsService.CalculateStandardDeviation(yValues);
            
            GetDerivatives();
            GetRoundedSignificantFiguresYValues(yValues);
        }

        private void GetRoundedSignificantFiguresYValues(List<double> yValues)
        {
            RoundedSignificantFiguresYValues.Clear();
            var roundedValues = _calculationsService.RoundYValues(yValues);
            foreach (var roundedY in roundedValues)
                RoundedSignificantFiguresYValues.Add(roundedY);
        }

        private void GetDerivatives()
        {
            FirstDerivativeData.Clear();
            var firstDerivatives = _calculationsService.CalculateFirstDerivative(MeasurementData.ToList());
            foreach (var derivative in firstDerivatives)
                FirstDerivativeData.Add(derivative);

            SecondDerivativeData.Clear();
            var secDerivatives = _calculationsService.CalculateSecondDerivative(MeasurementData.ToList());          
            foreach (var derivative in secDerivatives)
                SecondDerivativeData.Add(derivative);
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeasurementCalculations.ViewModel.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace MeasurementCalculations.ViewModel
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        public readonly FileService _fileService;
        public readonly CalculationsService _calculationsService;

        [ObservableProperty]
        private double _meanYValues;

        [ObservableProperty]
        private double _standardDeviationYValues;

        [ObservableProperty]
        private ObservableCollection<DataPoint> _measurementDataList = new ObservableCollection<DataPoint>();

        [ObservableProperty]
        private ObservableCollection<DerivativePoint> _firstDerivativeDataList = new ObservableCollection<DerivativePoint>();

        [ObservableProperty]
        private ObservableCollection<DerivativePoint> _secondDerivativeDataList = new ObservableCollection<DerivativePoint>();
        
        [ObservableProperty]
        private ObservableCollection<double> _roundedSignificantFiguresYValues = new ObservableCollection<double>();

        public MainWindowViewModel(FileService fileService, CalculationsService calculationsService)
        {
            _fileService = fileService;
            _calculationsService = calculationsService;
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
                        MeasurementDataList.Add(point);

                    CalculateStatistics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fehler beim Laden der Datei",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CalculateStatistics()
        {
            var yValues = MeasurementDataList.Select(dp => dp.Y).ToList();
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
            FirstDerivativeDataList.Clear();
            var firstDerivatives = _calculationsService.CalculateFirstDerivative(MeasurementDataList.ToList());
            foreach (var derivative in firstDerivatives)
                FirstDerivativeDataList.Add(derivative);

            SecondDerivativeDataList.Clear();
            var secDerivatives = _calculationsService.CalculateSecondDerivative(MeasurementDataList.ToList());          
            foreach (var derivative in secDerivatives)
                SecondDerivativeDataList.Add(derivative);
        }
    }
}

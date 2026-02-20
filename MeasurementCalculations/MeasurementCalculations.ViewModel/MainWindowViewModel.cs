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

        [ObservableProperty]
        private ObservableCollection<DataPoint> _measurementDataList = new ObservableCollection<DataPoint>();

        public MainWindowViewModel(FileService fileService)
        {
            _fileService = fileService;
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
                    {
                        MeasurementDataList.Add(point);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fehler beim Laden der Datei",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

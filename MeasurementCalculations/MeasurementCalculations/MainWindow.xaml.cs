using MeasurementCalculations.ViewModel;
using MeasurementCalculations.ViewModel.Services;
using System.Windows;

namespace MeasurementCalculations
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var fileService = new FileService();
            var calculationsService = new CalculationsService();

            DataContext = new MainWindowViewModel(fileService, calculationsService);
        }
    }
}
using MeasurementCalculations.ViewModel;
using MeasurementCalculations.ViewModel.Model;
using System.Windows;

namespace MeasurementCalculations
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var fileService = new FileService();

            DataContext = new MainWindowViewModel(fileService);
        }
    }
}
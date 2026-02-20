using Microsoft.Win32;
using System.Globalization;
using System.IO;
using System.Windows.Shapes;

namespace MeasurementCalculations.ViewModel.Model
{
    public class FileService
    {

        public string OpenFile()
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }

        public List<DataPoint> LoadFile(string filePath)
        {
            var dataPoints = new List<DataPoint>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');

                double x = double.Parse(parts[0], CultureInfo.InvariantCulture);
                double y = double.Parse(parts[1], CultureInfo.InvariantCulture);

                dataPoints.Add(new DataPoint(x,y));
            }
            return dataPoints;
        }
    }
}

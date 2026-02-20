using MeasurementCalculations.ViewModel.Model;
using Microsoft.Win32;
using System.Globalization;
using System.IO;
using System.Windows.Shapes;

namespace MeasurementCalculations.ViewModel.Services
{
    public class FileService : IFileService
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

                dataPoints.Add(new DataPoint(x, y));
            }
            return dataPoints;
        }

        public List<FileProperties> LoadRepositoryData(string folderPath)
        {
            var fileProperties = new List<FileProperties>();

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Das Verzeichnis '{folderPath}' wurde nicht gefunden.");

            var files = Directory.GetFiles(folderPath);

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                var properties = new FileProperties
                {
                    FileName = fileInfo.Name,
                    FullPath = fileInfo.FullName,
                    Extension = fileInfo.Extension,
                    SizeInBytes = fileInfo.Length,
                    CreationTime = fileInfo.CreationTime,
                    LastModifiedTime = fileInfo.LastWriteTime,
                    LastAccessTime = fileInfo.LastAccessTime,
                    IsReadOnly = fileInfo.IsReadOnly,
                    DirectoryName = fileInfo.DirectoryName
                };

                fileProperties.Add(properties);
            }
            
            return fileProperties;
        }
    }
}

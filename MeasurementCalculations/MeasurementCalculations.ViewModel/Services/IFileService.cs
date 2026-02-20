using MeasurementCalculations.ViewModel.Model;

namespace MeasurementCalculations.ViewModel.Services
{
    public interface IFileService
    {
        string? OpenFile();
        List<DataPoint> LoadFile(string filePath);
        List<FileProperties> LoadRepositoryData(string repositoryPath);
    }
}

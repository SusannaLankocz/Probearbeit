using MeasurementCalculations.ViewModel.Model;
using MeasurementCalculations.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementCalculations.ViewModel.Test.Mocks
{
    public class FileServiceMock : IFileService
    {
        public string? FilePathToReturn { get; set; }
        public List<DataPoint> DataPointsToReturn { get; set; } = new List<DataPoint>();
        public List<FileProperties> FilePropertiesToReturn { get; set; } = new List<FileProperties>();

        public bool OpenFileCalled { get; private set; }
        public bool LoadFileCalled { get; private set; }
        public bool LoadRepositoryDataCalled { get; private set; }
        public string? LastLoadFilePathUsed { get; private set; }
        public string? LastRepositoryPathUsed { get; private set; }

        public string? OpenFile()
        {
            OpenFileCalled = true;

            return FilePathToReturn;
        }

        public List<DataPoint> LoadFile(string filePath)
        {
            LoadFileCalled = true;
            LastLoadFilePathUsed = filePath;

            return DataPointsToReturn;
        }

        public List<FileProperties> LoadRepositoryData(string repositoryPath)
        {
            LoadRepositoryDataCalled = true;
            LastRepositoryPathUsed = repositoryPath;

            return FilePropertiesToReturn;
        }
    }
}

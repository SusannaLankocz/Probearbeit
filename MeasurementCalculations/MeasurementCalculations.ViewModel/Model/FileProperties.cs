namespace MeasurementCalculations.ViewModel.Model
{
    public class FileProperties
    {
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string Extension { get; set; }
        public long SizeInBytes { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public bool IsReadOnly { get; set; }
        public string DirectoryName { get; set; }
    }
}

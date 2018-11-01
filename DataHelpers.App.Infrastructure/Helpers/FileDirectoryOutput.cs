using System.Collections.Generic;

namespace DataHelpers.App.Infrastructure.Helpers
{
    public class FileDirectoryOutput
    {
        public int FileNumbers { get; set; }
        public string ErrorMessage { get; set; }
        public List<FileInfoOutput> FilesList { get; set; }

        public class FileInfoOutput
        {
            public string FileName { get; set; }
            public FileType Type { get; set; }
            public string Extension { get; set; }
        }

        public class LogicalDrive
        {
            public string Name { get; set; }
            public long AvialaibleSpace { get; set; }
            public string AvaliableSpaceText { get; set; }
            public long TotalSpace { get; set; }
            public string TotalSpaceText { get; set; }
            public string VolumeLabel { get; set; }
        }

        public enum FileType
        {
            Drive, 
            File, 
            Folder
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static DataHelpers.App.Infrastructure.Helpers.FileDirectoryOutput;

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

    public class FileDirectoryHelpers
    {
        public FileDirectoryOutput GetFilesNumberInPath(string workspacePath)
        {
            try
            {
                var logicalDrives = GetAllLogicalDrive();

                var fileList = new List<FileInfoOutput>();

                var files = Directory.GetFiles(workspacePath);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfoOutput()
                    {
                        FileName = GetFileFolderName(file),
                        Type = FileType.File,
                        Extension = Path.GetExtension(file)
                    };

                    fileList.Add(fileInfo);
                }
                return new FileDirectoryOutput()
                {
                    ErrorMessage = null,
                    FileNumbers = files.Count(),
                    FilesList = fileList
                };
            }
            catch (UnauthorizedAccessException)
            {
                return new FileDirectoryOutput()
                {
                    ErrorMessage = "Error: UnauthorizedAccessException",
                    FileNumbers = -1
                };
            }
            catch (PathTooLongException)
            {
                return new FileDirectoryOutput()
                {
                    ErrorMessage = "Error: PathTooLongException",
                    FileNumbers = -1
                };
            }
            catch (DirectoryNotFoundException)
            {
                return new FileDirectoryOutput()
                {
                    ErrorMessage = "Error: DirectoryNotFoundException",
                    FileNumbers = -1
                };
            }
            catch (Exception ex)
            {
                return new FileDirectoryOutput()
                {
                    ErrorMessage = $"Error: { ex.Message}",
                    FileNumbers = -1
                };

            }
        }

        public static string GetFileFolderName(string path)
        {
            // If we have no path, return empty
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // Make all slashes back slashes
            var normalizedPath = path.Replace('/', '\\');

            // Find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // If we don't find a backslash, return the path itself
            if (lastIndex <= 0)
                return path;

            // Return the name after the last back slash
            return path.Substring(lastIndex + 1);
        }

        public static List<LogicalDrive> GetAllLogicalDrive()
        {
            var driversList = new List<LogicalDrive>();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    driversList.Add(new LogicalDrive()
                    {
                        Name = drive.Name,
                        VolumeLabel = drive.VolumeLabel,
                        AvialaibleSpace = drive.AvailableFreeSpace,
                        AvaliableSpaceText = SizeSuffix(drive.AvailableFreeSpace),
                        TotalSpace = drive.TotalSize,
                        TotalSpaceText = SizeSuffix(drive.TotalSize)
                    });
                }
            }
            return driversList;
        }

        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }

            int i = 0;
            decimal dValue = value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }
    }
}

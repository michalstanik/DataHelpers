using System;
using System.IO;
using System.Linq;

namespace DataHelpers.App.Infrastructure.Helpers
{
    public class FileDirectoryOutput
    {
        public int FileNumbers { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class FileDirectoryHelpers
    {
        public FileDirectoryOutput GetFilesNumberInPath(string workspacePath)
        {
            try
            {
                var files = Directory.GetFiles(workspacePath);
                return new FileDirectoryOutput()
                {
                    ErrorMessage = null,
                    FileNumbers = files.Count()
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
    }
}

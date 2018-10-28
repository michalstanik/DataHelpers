using System;
using System.IO;
using System.Linq;
using DataHelpers.App.Infrastructure.Wrapper;
using DataHelpers.Data.DataModel.Projects;

namespace DataHelpers.App.Projects.Wrapper
{
    public class ProjectWorkspaceWrapper : ModelWrapper<ProjectWorkspace>
    {
        private int _filesCounter;
        private bool _errorFound;
        private string _errorMessage;

        public ProjectWorkspaceWrapper(ProjectWorkspace model) : base(model)
        {
            ErrorFound = false;   
        }

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string WorkspaceName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string WorkspacePath
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int FilesCounter
        {
            get { return _filesCounter = GetFilesNumberInPath(WorkspacePath); }
        }

        public bool HasPathError
        {
            get
            {
                if (GetFilesNumberInPath(WorkspacePath) < 0)
                    return false;
                else return true;
            }

            set { }
        }

        public bool ErrorFound
        {
            get { return _errorFound; }
            private set
            {
                _errorFound = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            private set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        private int GetFilesNumberInPath(string workspacePath)
        {
            try
            {
                var files = Directory.GetFiles(workspacePath);
                return files.Count();

            }
            catch(UnauthorizedAccessException)
            {
                ErrorFound = true;
                ErrorMessage = "Error: UnauthorizedAccessException";
            }
            catch(PathTooLongException)
            {
                ErrorFound = true;
                ErrorMessage = "Error: PathTooLongException";
            }
            catch(DirectoryNotFoundException)
            {
                ErrorFound = true;
                ErrorMessage = "Error: DirectoryNotFoundException";
            }
            catch(Exception ex)
            {
                ErrorFound = true;
                ErrorMessage = $"Error: {ex.Message}";
            }

            return -1;
        }
    }
}

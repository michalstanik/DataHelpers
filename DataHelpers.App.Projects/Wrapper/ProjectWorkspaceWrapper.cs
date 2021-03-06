﻿using DataHelpers.App.Infrastructure.Helpers;
using DataHelpers.App.Infrastructure.Wrapper;
using DataHelpers.Data.DataModel.Projects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
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
            get {
                //TODO: Refactor to static
                var newHelper = new FileDirectoryHelpers();
                var helperClass = newHelper.GetFilesInfoInPath(WorkspacePath);
                return helperClass.FileNumbers;
            }
        }

        public bool HasPathError
        {
            get
            {
                //TODO: Refactor to static
                var newHelper = new FileDirectoryHelpers();
                var helperClass = newHelper.GetFilesInfoInPath(WorkspacePath);
                if (helperClass.FileNumbers < 0)
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
            get
            {
                var newHelper = new FileDirectoryHelpers();
                var helperClass = newHelper.GetFilesInfoInPath(WorkspacePath);
                if (helperClass.ErrorMessage != null)
                    _errorMessage = helperClass.ErrorMessage;
                return _errorMessage;
            }
            private set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(ErrorMessage):
                    if (ErrorMessage != null)
                    {
                        yield return ErrorMessage.ToString();
                    }
                    break;
            }
        }


    }
}

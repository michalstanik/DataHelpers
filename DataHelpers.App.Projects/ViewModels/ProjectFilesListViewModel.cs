using System.Threading.Tasks;
using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Interfaces;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectFilesListViewModel : DetailViewModelBase, IProjectFilesListViewModel
    {
        public ProjectFilesListViewModel(IMessageDialogService messageDialogService) : base(messageDialogService)
        {
        }

        public override Task LoadAsync(int? id)
        {
            return Task.Delay(0);
        }

        protected override void OnDeleteExecute()
        {

        }

        protected override bool OnSaveCanExecute()
        {
            return true;
        }

        protected override void OnSaveExecute()
        {

        }
    }
}

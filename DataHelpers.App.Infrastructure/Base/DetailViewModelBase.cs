using DataHelpers.App.Infrastructure.Events;
using DataHelpers.App.Infrastructure.Interfaces;
using Prism.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataHelpers.App.Infrastructure.Base
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        private int _id;
        private bool _hasChanges;
        private string _title;
        private readonly IMessageDialogService _messageDialogService;

        public DetailViewModelBase(IMessageDialogService messageDialogService)
        {
            _messageDialogService = messageDialogService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            CloseDetailViewCommand = new DelegateCommand(OnCloseDetailViewExecute);

        }

        protected async virtual void OnCloseDetailViewExecute()
        {
            if (HasChanges)
            {
                var result = await _messageDialogService.ShowOkCancelDialogAsync(
                  "You've made changes. Close this item?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            EventAggregator.GetEvent<AfterDetailClosedEvent>()
              .Publish(new AfterDetailClosedEventArgs
              {
                  Id = this.Id,
                  ViewModelName = this.GetType().Name
              });
        }

        public abstract Task LoadAsync(int? id);

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand CloseDetailViewCommand { get; }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            protected set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        protected abstract void OnDeleteExecute();

        protected abstract void OnSaveExecute();

        protected abstract bool OnSaveCanExecute();

        protected virtual void RaiseDetailDeletedEvent(int modelId)
        {
            EventAggregator.GetEvent<AfterDetailDeletedEvent>().Publish(new AfterDetailDeletedEventArgs
            {
                Id = modelId,
                ViewModelName = this.GetType().Name
            });
        }

        protected virtual void RaiseDetailSavedEvent(int modelId, string displayMemeber)
        {
            EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(new AfterDetailSavedEventArgs
            {
                Id = modelId,
                DisplayMember = displayMemeber,
                ViewModelName = this.GetType().Name
            });
        }
    }
}

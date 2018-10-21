
using DataHelpers.App.Infrastructure.Base;
using System.Threading.Tasks;

namespace DataHelpers.App.Infrastructure.Interfaces
{
    public interface IMessageDialogService
    {
        Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title);
        Task ShowInfoDialogAsync(string text);
    }
}

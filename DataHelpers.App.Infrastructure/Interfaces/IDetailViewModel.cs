using System.Threading.Tasks;

namespace DataHelpers.App.Infrastructure.Interfaces
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int? id);
        bool HasChanges { get; }
        int Id { get; }
    }
}

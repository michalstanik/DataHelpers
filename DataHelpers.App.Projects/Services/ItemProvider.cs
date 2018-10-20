using DataHelpers.App.Infrastructure.ViewModels;
using System.Collections.Generic;

namespace DataHelpers.App.Projects.Services
{
    public class ItemProvider
    {
        private readonly NavigationDirectoryItemViewModel _rootDirectoryItem;

        public ItemProvider()
        {
            _rootDirectoryItem = new NavigationDirectoryItemViewModel(0, "Root", "Root");

            var childItem1 = new NavigationDirectoryItemViewModel(1, "A", "Detail");

            var grandChildItem11 = new NavigationDirectoryItemViewModel(2, "A1", "MoreDetail");
            var grandChildItem12 = new NavigationDirectoryItemViewModel(2, "A2", "MoreDetail");

            var greatgrandChildItem2 = new NavigationDirectoryItemViewModel(3, "A2_1", "MoreMoreDetail");
            grandChildItem11.AddDirItem(greatgrandChildItem2);

            childItem1.AddDirItem(grandChildItem11);
            childItem1.AddDirItem(grandChildItem12);

            var childList1 = new List<NavigationDirectoryItemViewModel>
            {
                childItem1
            };

            _rootDirectoryItem.Items = childList1;

        }

        public List<NavigationItemViewModel> DirItems => _rootDirectoryItem.Traverse(_rootDirectoryItem);
    }
}

using System.Collections.Generic;

namespace DataHelpers.App.Infrastructure.ViewModels
{
    public class NavigationDirectoryItemViewModel : NavigationItemViewModel
    {
        public NavigationDirectoryItemViewModel(int id, string displayMember, string detailViewModelName
            ,string image = null) 
            : base(id, displayMember, detailViewModelName, image)
        {
            Items = new List<NavigationDirectoryItemViewModel>();
        }

        public List<NavigationDirectoryItemViewModel> Items { get; set; }

        public void AddDirItem(NavigationDirectoryItemViewModel directoryItem)
        {
            Items.Add(directoryItem);
        }

        public List<NavigationItemViewModel> Traverse(NavigationDirectoryItemViewModel it)
        {
            var items = new List<NavigationItemViewModel>();

            foreach (var itm in it.Items)
            {
                Traverse(itm);
                items.Add(itm);
            }

            return items;
        }
    }
}

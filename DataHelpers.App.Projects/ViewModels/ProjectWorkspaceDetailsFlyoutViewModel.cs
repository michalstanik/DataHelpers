using DataHelpers.App.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectWorkspaceDetailsFlyoutViewModel : ViewModelBase
    {
        public ProjectWorkspaceDetailsFlyoutViewModel()
        {

        }

        public int EntityId { get; set; }

        protected override void OnViewLoaded()
        {
            if (EntityId != 0)
            {
                //TODO: Load an Entity
            }
        }

    }
}

using ReactiveUI;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class TabItemViewModel : ViewModelBase
    {
        private TabContentViewModel? _tabContentViewModel;
        private Project? _project;

        public Project Project
        {
            get => _project!;
            set => this.RaiseAndSetIfChanged(ref _project, value); 
        }

        public TabContentViewModel TabContentViewModel
        {
            get => _tabContentViewModel!;
            set => this.RaiseAndSetIfChanged(ref _tabContentViewModel, value);
        }

        public TabItemViewModel()
        {

        }
    }
}
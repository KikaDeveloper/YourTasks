using ReactiveUI;
using System.Collections.ObjectModel;

namespace YourTasks.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ProjectViewModel>? _projects;

        public ObservableCollection<ProjectViewModel> Projects
        {
            get => _projects!;
            set => this.RaiseAndSetIfChanged(ref _projects, value);
        }
        
        public MainWindowViewModel()
        {
        }
    }
}

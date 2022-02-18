using ReactiveUI;
using System.Linq;
using System.Collections.ObjectModel;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ProjectViewModel? _selectedProject;
        private ObservableCollection<ProjectViewModel>? _projects;

        public ObservableCollection<ProjectViewModel> Projects
        {
            get => _projects!;
            set => this.RaiseAndSetIfChanged(ref _projects, value);
        }
        
        public ProjectViewModel SelectedProject
        {
            get => _selectedProject!;
            set => this.RaiseAndSetIfChanged(ref _selectedProject, value);
        }

        public MainWindowViewModel()
        {
            Projects = new ObservableCollection<ProjectViewModel>();
            Services.AppRepository repo = new Services.AppRepository();
        }
    }
}

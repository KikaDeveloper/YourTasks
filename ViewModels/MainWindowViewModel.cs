using ReactiveUI;
using System.Linq;
using System.Collections.Generic;
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
            Services.AppRepository repo = Services.AppRepository.Instance;
            Projects = new ObservableCollection<ProjectViewModel>();

            var temp_projects = System.Threading.Tasks.Task.Run(async()=> await repo.GetAllProjects()).Result;
            foreach(var p in temp_projects)
                Projects.Add(new ProjectViewModel(p));
        }
    }
}

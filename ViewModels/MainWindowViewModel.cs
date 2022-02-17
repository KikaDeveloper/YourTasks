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
            Projects = new ObservableCollection<ProjectViewModel>()
            {
                new ProjectViewModel(new Models.Project()
                    {
                        Id = System.Guid.NewGuid(),
                        Name = "Database",
                        Description = "Create a database for app.",
                        EllipseColor = "Red",
                        Tasks = new ObservableCollection<Models.Task>(){
                            new Task()
                            {
                                Id = System.Guid.NewGuid(),
                                Text = "Create db.",
                                CreationDateTime = System.DateTime.Today,
                                Description = "",
                                IsCompleted = false
                            },
                            new Task()
                            {
                                Id = System.Guid.NewGuid(),
                                Text = "Make commit.",
                                CreationDateTime = System.DateTime.Today,
                                Description = "",
                                IsCompleted = false
                            },
                        }
                    }
                )
            };

            SelectedProject = Projects.First();
        }
    }
}

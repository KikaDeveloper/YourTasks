using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using YourTasks.Services;
using YourTasks.Views;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ProjectViewModel? _selectedProject;
        private ObservableCollection<ProjectViewModel>? _projects;
        private bool _themeModeSwitcher;

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

        public bool ThemeModeSwitcher
        {
            get => _themeModeSwitcher;
            set => this.RaiseAndSetIfChanged(ref _themeModeSwitcher, value);
        }

        public IReactiveCommand AddProjectCommand { get; }

        public MainWindowViewModel()
        {
            Services.AppRepository repo = Services.AppRepository.Instance;
            Projects = new ObservableCollection<ProjectViewModel>();

            var temp_projects = System.Threading.Tasks.Task.Run(async()=> await repo.GetAllProjects()).Result;
            foreach(var project in temp_projects)
            {
                var projectVM = new ProjectViewModel(project);
                // subscribe to delete event
                projectVM.DeleteProjectEvent += DeleteProjectEventHandler;
                Projects.Add(projectVM);
            }

            AddProjectCommand = ReactiveCommand.CreateFromTask(
                async() => await AddProject()
            );
        }

        private async System.Threading.Tasks.Task AddProject()
        {
            var newProject = await OpenAddDialog();

            if(newProject != null)
            {
                var newProjectVM = new ProjectViewModel(newProject);
                // subscribe to delete event
                newProjectVM.DeleteProjectEvent += DeleteProjectEventHandler;

                Projects.Add(newProjectVM);
                await AppRepository.Instance.InsertEntity<Project>(newProject);
            }
        }

        private async void DeleteProjectEventHandler(object? sender, EventArgs e)
        {
            var projectVM = (ProjectViewModel) sender!;

            Projects.Remove(projectVM);
            await AppRepository.Instance.CascadeDeleteProject(projectVM.Project);
        }

        private async System.Threading.Tasks.Task<Project> OpenAddDialog()
            => await DialogService.ShowDialogAsync<Project>(
                new NewProjectWindow
                {
                    DataContext = new NewProjectViewModel()
                }
            );
    }
}

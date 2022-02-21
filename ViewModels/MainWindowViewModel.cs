﻿using ReactiveUI;
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

        public IReactiveCommand AddProjectCommand { get; }

        public MainWindowViewModel()
        {
            Services.AppRepository repo = Services.AppRepository.Instance;
            Projects = new ObservableCollection<ProjectViewModel>();

            var temp_projects = System.Threading.Tasks.Task.Run(async()=> await repo.GetAllProjects()).Result;
            foreach(var p in temp_projects)
                Projects.Add(new ProjectViewModel(p));

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
                // adding in db and collection
                Projects.Add(newProjectVM);
                await AppRepository.Instance.InsertEntity<Project>(newProject);
            }
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

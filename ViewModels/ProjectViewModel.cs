using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private string? _name;
        private string? _description;
        private string? _ellipseColor;
        private ObservableCollection<TaskViewModel>? _tasks;

        public string Name
        {
            get => _name!;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string EllipseColor
        {
            get => _ellipseColor!;
            set => this.RaiseAndSetIfChanged(ref _ellipseColor, value);
        }

        public string Description
        {
            get => _description!;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public ObservableCollection<TaskViewModel> Tasks
        {
            get => _tasks!;
            set => this.RaiseAndSetIfChanged(ref _tasks, value);
        }

        public ProjectViewModel(Project project)
        {
            Name = project.Name!;
            EllipseColor = project.EllipseColor!;
            Description = project.Description!;

            Tasks = new ObservableCollection<TaskViewModel>();
            foreach(var task in project.Tasks!)
                Tasks.Add(new TaskViewModel(task));
        }

    }
}
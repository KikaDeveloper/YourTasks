using ReactiveUI;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using YourTasks.Models;
using YourTasks.Services;

namespace YourTasks.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private string? _name;
        private string? _description;
        private string? _ellipseColor;
        private ObservableCollection<TaskViewModel>? _tasks;
        private ObservableCollection<TaskViewModel>? _completedTasks;

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

        public ObservableCollection<TaskViewModel> CompletedTasks
        {
            get => _completedTasks!;
            set => this.RaiseAndSetIfChanged(ref _completedTasks, value);
        }

        public IReactiveCommand? AddNewTaskCommand { get; private set; }

        public ProjectViewModel(Project project)
        {
            Name = project.Name!;
            EllipseColor = project.EllipseColor!;
            Description = project.Description!;

            Tasks = new ObservableCollection<TaskViewModel>();
            foreach(var task in project.Tasks!)
            {
                var newtask = new TaskViewModel(task);
                // подписка на событие выполнения задачи
                newtask.TaskCompletedEvent += TaskCompletedEventHandler;
                Tasks.Add(newtask);
            }

            CompletedTasks = new ObservableCollection<TaskViewModel>(
                Tasks.Where(taskVM => taskVM.IsCompleted == true)
            );
            var repo = AppRepository.Instance;
            AddNewTaskCommand = ReactiveCommand.CreateFromTask(async() => await repo.InsertEntity<Task>(
                new Task{
                    Text = "Name",
                    CreationDateTime = DateTime.Today.ToString(),
                    Description = "",
                    IsCompleted = false,
                    ProjectId = 1,
                    SubTasks = null
                }
            ));
        }

        private void TaskCompletedEventHandler(object? sender, TaskCompletedArgs e)
        {
            var task = (TaskViewModel) sender!;
            switch(e.IsCompleted)
            {
                case true:
                    Tasks.Remove(task);
                    CompletedTasks.Add(task);
                break;

                case false:
                    CompletedTasks.Remove(task);
                    Tasks.Add(task);
                break;
            }
        }
    }
}
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

        public ProjectViewModel(Project project)
        {
            Name = project.Name!;
            EllipseColor = project.EllipseColor!;
            Description = project.Description!;

            // Tasks = new ObservableCollection<TaskViewModel>();
            // foreach(var task in project.Tasks!)
            // {
            //     var newtask = new TaskViewModel(task);
            //     // подписка на событие выполнения задачи
            //     newtask.TaskCompletedEvent += TaskCompletedEventHandler;
            //     Tasks.Add(newtask);
            // }

            CompletedTasks = new ObservableCollection<TaskViewModel>();

            UpdateCompletedTasks();
        }

        private void TaskCompletedEventHandler(object? sender, TaskCompletedArgs e)
        {
            var task = (TaskViewModel) sender!;
            switch(e.IsCompleted)
            {
                case true:
                    MoveCompletedTask(task);
                break;

                case false:
                    CompletedTasks.Remove(task);
                    Tasks.Add(task);
                break;
            }
        }

        private void UpdateCompletedTasks()
        {
            foreach(var task in Tasks)
            {
                if(task.IsCompleted == true)
                {
                    MoveCompletedTask(task);
                }
            }
        }

        private void MoveCompletedTask(TaskViewModel task)
        {
            Tasks.Remove(task);
            CompletedTasks.Add(task);
        }

    }
}
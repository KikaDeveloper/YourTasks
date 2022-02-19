using ReactiveUI;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using YourTasks.Models;
using YourTasks.Views;
using YourTasks.Services;

namespace YourTasks.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private int _projectId;
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
            _projectId = project.Id;
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
            AddNewTaskCommand = ReactiveCommand.CreateFromTask(
                async()=> await OpenNewTaskDialog());
        }

        private async System.Threading.Tasks.Task OpenNewTaskDialog()
        {
            var newTask = await DialogService.ShowDialogAsync<Task>(
                new NewTaskWindow{
                    DataContext = new NewTaskWindowViewModel()
                }
            );

            newTask.ProjectId = _projectId;

            if(newTask != null)
            {
                Tasks.Add(new TaskViewModel(newTask));
                await AppRepository.Instance.InsertEntity<Task>(newTask);
            }
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
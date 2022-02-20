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
        private Project? _project; 
        private ObservableCollection<TaskViewModel>? _tasks;
        private ObservableCollection<TaskViewModel>? _completedTasks;

        public Project Project
       {
           get => _project!;
           set => this.RaiseAndSetIfChanged(ref _project, value);
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

        public IReactiveCommand? AddNewTaskCommand { get; }

        public ProjectViewModel(Project project)
        {
            Project = project;

            Tasks = new ObservableCollection<TaskViewModel>();
            foreach(var task in project.Tasks!)
            {
                var newtask = new TaskViewModel(task);
                // подписка на событие выполнения задачи
                newtask.Task.TaskCompletedEvent += TaskCompletedEventHandler;
                Tasks.Add(newtask);
            }

            CompletedTasks = new ObservableCollection<TaskViewModel>(
                Tasks.Where(taskVM => taskVM.Task.IsCompleted == true)
            );

            AddNewTaskCommand = ReactiveCommand.CreateFromTask(
                async()=> await AddNewTask());
        }

        private async System.Threading.Tasks.Task AddNewTask()
        {
            var newTask = (Task) await OpenAddDialog();

            newTask.SubTasks = new ObservableCollection<SubTask>();
            var taskVM = new TaskViewModel(newTask);
            taskVM.Task.ProjectId = Project.Id;
            taskVM.Task.TaskCompletedEvent += TaskCompletedEventHandler;

            if(newTask != null)
            {
                Tasks.Add(taskVM);
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

        private async System.Threading.Tasks.Task<TaskBase> OpenAddDialog() 
            => await DialogService.ShowDialogAsync<TaskBase>(
                    new NewTaskWindow{
                        DataContext = new NewTaskViewModel(false)
                    }
                );
    }
}
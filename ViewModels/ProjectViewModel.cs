using ReactiveUI;
using System;
using System.Linq;
using System.Collections.Generic;
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

        public IReactiveCommand AddTaskCommand { get; }

        public IReactiveCommand DeleteProjectCommand { get; }

        public EventHandler? DeleteProjectEvent;

        public ProjectViewModel(Project project)
        {
            Project = project;

            Tasks = new ObservableCollection<TaskViewModel>(
                InitTasks(false)
            );
            
            CompletedTasks = new ObservableCollection<TaskViewModel>(
                InitTasks(true)
            );

            AddTaskCommand = ReactiveCommand.CreateFromTask(
                async()=> await AddNewTask());

            DeleteProjectCommand = ReactiveCommand.Create(
                () => DeleteProjectEvent?.Invoke(this, new EventArgs())
            );
        }

        private IEnumerable<TaskViewModel> InitTasks(bool isCompletedTasks)
        {
            var tasks = Project.Tasks!.Where(task => task.IsCompleted == isCompletedTasks);
            var viewModels = new List<TaskViewModel>();
            
            foreach(var task in tasks)
            {
                var newTask = new TaskViewModel(task);
                // subscribe to completed task event
                newTask.Task.TaskCompletedEvent += TaskCompletedEventHandler;
                // subscribe to delete task event
                newTask.TaskDeleteEvent += TaskDeleteEventHandler;
                viewModels.Add(newTask);
            }

            return viewModels;
        }

        private async System.Threading.Tasks.Task AddNewTask()
        {
            var newTask = (Task) await OpenAddDialog();

            if(newTask != null)
            {
                newTask.SubTasks = new ObservableCollection<SubTask>();
                Project.Tasks!.Add(newTask);

                var taskVM = new TaskViewModel(newTask);
                taskVM.Task.ProjectId = Project.Id;
                taskVM.Task.TaskCompletedEvent += TaskCompletedEventHandler;
                taskVM.TaskDeleteEvent += TaskDeleteEventHandler;

                // adding in db and collection
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

        private async void TaskDeleteEventHandler(object? sender, EventArgs e)
        {
            var taskVM = (TaskViewModel)sender!;

            Tasks.Remove(taskVM);
            await AppRepository.Instance.DeleteEntity<Task>(taskVM.Task);
        }

        private async System.Threading.Tasks.Task<TaskBase> OpenAddDialog() 
            => await DialogService.ShowDialogAsync<TaskBase>(
                    new NewTaskWindow{
                        DataContext = new NewTaskViewModel(true)
                    }
                );
    }
}
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using YourTasks.Models;
using YourTasks.Views;
using YourTasks.Services;

namespace YourTasks.ViewModels
{  
    public class TaskViewModel : ViewModelBase
    {
        private Task? _task;
        private ObservableCollection<SubTaskViewModel>? _subTasks;
        
        public Task Task
        {
            get => _task!;
            set => this.RaiseAndSetIfChanged(ref _task, value);
        }  
      
        public bool IsCompleted
        {
            get => _task!.IsCompleted;
            set {
                _task!.IsCompleted = value;
                Task.TaskCompletedEvent?.Invoke(this, new TaskCompletedArgs(value));
                System.Threading.Tasks.Task.Run(
                    async() => await UpdateTask() 
                );
            }
        }

        public ObservableCollection<SubTaskViewModel> SubTasks
        {
            get => _subTasks!;
            set => this.RaiseAndSetIfChanged(ref _subTasks, value);
        }

        public EventHandler? TaskDeleteEvent;

        public IReactiveCommand DeleteTaskCommand { get; }
        public IReactiveCommand AddSubTaskCommand { get; }
        
        public TaskViewModel(Task task)
        {
            Task = task;

            SubTasks = new ObservableCollection<SubTaskViewModel>();
            foreach(var subTask in task.SubTasks!)
            {
                var newSubTaskVM = new SubTaskViewModel(subTask);
                // subscribe to subtask delete event
                newSubTaskVM.SubTaskDeleteEvent += DeleteSubTaskEventHandler;
                // subscribe to subtask completed event
                newSubTaskVM.Task.TaskCompletedEvent += SubTaskCompletedEventHandler;
                SubTasks.Add(newSubTaskVM);
            }

            DeleteTaskCommand = ReactiveCommand.Create(()=>{
                TaskDeleteEvent?.Invoke(this, new EventArgs());
            });

            AddSubTaskCommand = ReactiveCommand.CreateFromTask(async() 
                => await AddSubTask());
        }

        private async void DeleteSubTaskEventHandler(object? sender, EventArgs e)
        {
            var subTask = (SubTaskViewModel)sender!;

            SubTasks.Remove(subTask);
            await AppRepository.Instance.DeleteEntity<SubTask>(subTask.Task);
        }

        private void SubTaskCompletedEventHandler(object? sender, TaskCompletedArgs e)
        {
            Console.WriteLine("Completed subtask");
        }

        private async System.Threading.Tasks.Task AddSubTask()
        {
            var newTask = (SubTask) await OpenAddDialog();

            if(newTask != null)
            {
                var newSubTaskVM = new SubTaskViewModel(newTask);
                newSubTaskVM.Task.TaskId = Task.Id;
                newSubTaskVM.SubTaskDeleteEvent += DeleteSubTaskEventHandler;
           
                SubTasks.Add(newSubTaskVM);
                await AppRepository.Instance.InsertEntity<SubTask>(newSubTaskVM.Task);
            }
        }

        private async  System.Threading.Tasks.Task UpdateTask()
        {
            await AppRepository.Instance.UpdateEntity<Task>(Task);
        }

        private async System.Threading.Tasks.Task<TaskBase> OpenAddDialog() 
            => await DialogService.ShowDialogAsync<TaskBase>(
                    new NewTaskWindow{
                        DataContext = new NewTaskViewModel(false)
                    }
                );

    }
}
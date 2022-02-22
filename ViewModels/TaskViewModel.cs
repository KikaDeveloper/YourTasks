using ReactiveUI;
using System;
using System.Linq;
using System.Collections.Specialized;
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
        private bool _checkBoxEnabled;
        
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
                    async() => await UpdateTask());
                CanCheckBoxCheck();
            }
        }

        public ObservableCollection<SubTaskViewModel> SubTasks
        {
            get => _subTasks!;
            set => this.RaiseAndSetIfChanged(ref _subTasks, value);
        }

        public bool CheckBoxEnabled
        {
            get => _checkBoxEnabled;
            set => this.RaiseAndSetIfChanged(ref _checkBoxEnabled, value);
        }

        public EventHandler? TaskDeleteEvent;

        public IReactiveCommand DeleteTaskCommand { get; }
        public IReactiveCommand AddSubTaskCommand { get; }

        public TaskViewModel(Task task)
        {
            Task = task;

            SubTasks = new ObservableCollection<SubTaskViewModel>();
            SubTasks.CollectionChanged += SubTasksCollectionChangedHandler;
            foreach(var subTask in task.SubTasks!)
            {
                var newSubTaskVM = new SubTaskViewModel(subTask);
                // subscribe to subtask delete event
                newSubTaskVM.SubTaskDeleteEvent += DeleteSubTaskEventHandler;
                // subscribe to subtask completed event
                newSubTaskVM.Task.TaskCompletedEvent += SubTaskCompletedEventHandler;
                SubTasks.Add(newSubTaskVM);
            }

            CanCheckBoxCheck();

            DeleteTaskCommand = ReactiveCommand.Create(
                () => TaskDeleteEvent?.Invoke(this, new EventArgs()));

            AddSubTaskCommand = ReactiveCommand.CreateFromTask(
                async() => await AddSubTask());
        }

        private void CanCheckBoxCheck()
        {
            if(SubTasks.Count == 0)
                CheckBoxEnabled = true;
            else if(!IsCompleted && SubTasks.Count > 0)
                CheckBoxEnabled = false;
            else if(IsCompleted && SubTasks.Count > 0)
                CheckBoxEnabled = true;
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
            if(e.IsCompleted)
            {
                int completedSubTasks = SubTasks.Where(sub_task => sub_task.IsCompleted == true).Count();
                if(completedSubTasks == SubTasks.Count)
                    IsCompleted = true; 
            }
        }

        private void SubTasksCollectionChangedHandler(object? sender, NotifyCollectionChangedEventArgs e)
            => CanCheckBoxCheck();
        private async System.Threading.Tasks.Task AddSubTask()
        {
            var newTask = (SubTask) await OpenAddDialog();

            if(newTask != null)
            {
                Task.SubTasks!.Add(newTask);
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
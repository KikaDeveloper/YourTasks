using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using YourTasks.Models;
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
      
        public ObservableCollection<SubTaskViewModel> SubTasks
        {
            get => _subTasks!;
            set => this.RaiseAndSetIfChanged(ref _subTasks, value);
        }

        public event EventHandler<TaskCompletedArgs>? TaskCompletedEvent;

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
                newSubTaskVM.SubTaskDeleteEvent += SubTaskDeleteEventHandler;
                // subscribe to subtask completed event
                newSubTaskVM.SubTask.TaskCompletedEvent += SubTaskCompletedEventHandler;
                SubTasks.Add(newSubTaskVM);
            }

            DeleteTaskCommand = ReactiveCommand.Create(()=>{
                Console.WriteLine("Delete");
            });

            AddSubTaskCommand = ReactiveCommand.Create(()=>{
                Console.WriteLine("add new subtask");
            });
        }

        private async void SubTaskDeleteEventHandler(object? sender, EventArgs e)
        {
            Console.WriteLine("Delete subtask");
            var subTask = (SubTaskViewModel)sender!;

            SubTasks.Remove(subTask);
            await AppRepository.Instance.DeleteEntity<SubTask>(subTask.SubTask);
        }

        private void SubTaskCompletedEventHandler(object? sender, TaskCompletedArgs e)
        {
            Console.WriteLine("Completed subtask");
            
        }

    }
}
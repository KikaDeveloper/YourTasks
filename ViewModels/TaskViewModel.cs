using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using YourTasks.Models;

namespace YourTasks.ViewModels
{  
    public class TaskViewModel : ViewModelBase
    {
        private string? _text;
        private string? _description;
        private string? _creationDateTime;
        private bool _isCompleted;
        private ObservableCollection<SubTaskViewModel>? _subTasks;

        public string Text
        {
            get => _text!;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        public string Description
        {
            get => _description!;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }
        
        public string CreationDateTime
        {
            get => _creationDateTime!;
            set => this.RaiseAndSetIfChanged(ref _creationDateTime, value);
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set {
                this.RaiseAndSetIfChanged(ref _isCompleted, value);
                TaskCompletedEvent?.Invoke(this, new TaskCompletedArgs(value));
            }
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
            Text = task.Text!;
            Description = task.Description!;
            CreationDateTime = task.CreationDateTime!;
            IsCompleted = task.IsCompleted;
            
            SubTasks = new ObservableCollection<SubTaskViewModel>();
            foreach(var subTask in task.SubTasks!)
            {
                var newSubTaskVM = new SubTaskViewModel(subTask);
                // subscribe to subtask delete event
                newSubTaskVM.SubTaskDeleteEvent += SubTaskDeleteEventHandler;
                // subscribe to subtask completed event
                newSubTaskVM.SubTaskCompletedEvent += SubTaskCompletedEventHandler;
                SubTasks.Add(newSubTaskVM);
            }

            DeleteTaskCommand = ReactiveCommand.Create(()=>{
                Console.WriteLine("Delete");
            });

            AddSubTaskCommand = ReactiveCommand.Create(()=>{
                Console.WriteLine("add new subtask");
            });
        }

        private void SubTaskDeleteEventHandler(object? sender, EventArgs e)
        {
            Console.WriteLine("Delete subtask");
        }

        private void SubTaskCompletedEventHandler(object? sender, TaskCompletedArgs e)
        {
            Console.WriteLine("Completed subtask");
        }

    }
}
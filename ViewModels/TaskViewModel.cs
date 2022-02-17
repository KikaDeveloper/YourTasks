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
        private DateTime _creationDateTime;
        private bool _isCompleted;
        private ObservableCollection<Task>? _subTasks;

        public string Text
        {
            get => _text!;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set {
                this.RaiseAndSetIfChanged(ref _isCompleted, value);
                TaskCompletedEvent?.Invoke(this, new TaskCompletedArgs(value));
            }
        }
    
        public string Description
        {
            get => _description!;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }
    
        public DateTime CreationDateTime
        {
            get => _creationDateTime;
            set => this.RaiseAndSetIfChanged(ref _creationDateTime, value);
        }

        public ObservableCollection<Task> SubTasks
        {
            get => _subTasks!;
            set => this.RaiseAndSetIfChanged(ref _subTasks, value);
        }

        public event EventHandler<TaskCompletedArgs>? TaskCompletedEvent;

        public TaskViewModel(Task task)
        {
            Text = task.Text!;
            Description = task.Description!;
            CreationDateTime = task.CreationDateTime;
            IsCompleted = task.IsCompleted;
        }

    }
}
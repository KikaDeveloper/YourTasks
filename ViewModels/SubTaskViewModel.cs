using ReactiveUI;
using System;
using YourTasks.Models;

namespace YourTasks.ViewModels
{  
    public class SubTaskViewModel : ViewModelBase
    {
        private string? _text;
        private string? _description;
        private string? _creationDateTime;
        private bool _isCompleted;

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
                SubTaskCompletedEvent?.Invoke(this, new TaskCompletedArgs(value));
            }
        }
    
        public EventHandler? SubTaskDeleteEvent;
        public EventHandler? SubTaskCompletedEvent;
        public IReactiveCommand SubTaskDeleteCommand { get; }

        public SubTaskViewModel(SubTask task)
        {
            Text = task.Text!;
            Description = task.Description!;
            CreationDateTime = task.CreationDateTime!;
            IsCompleted = task.IsCompleted!;

            SubTaskDeleteCommand = ReactiveCommand.Create(()=>{
                SubTaskDeleteEvent?.Invoke(this, new EventArgs());
            });
        }
    }
}
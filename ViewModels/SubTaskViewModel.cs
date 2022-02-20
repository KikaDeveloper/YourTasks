using ReactiveUI;
using System;
using YourTasks.Models;

namespace YourTasks.ViewModels
{  
    public class SubTaskViewModel : ViewModelBase
    {
        private SubTask? _task;
        public SubTask Task
        {
            get => _task!;
            set => this.RaiseAndSetIfChanged(ref _task, value);
        }
    
        public bool IsCompleted
        {
            get => _task!.IsCompleted;
            set => _task!.IsCompleted = value;
        }

        public EventHandler? SubTaskDeleteEvent;
        public IReactiveCommand DeleteTaskCommand { get; }

        public SubTaskViewModel(SubTask task)
        {
            Task = task;

            DeleteTaskCommand = ReactiveCommand.Create(()=>{
                SubTaskDeleteEvent?.Invoke(this, new EventArgs());
            });
        }
    }
}
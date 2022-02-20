using ReactiveUI;
using System;
using YourTasks.Models;

namespace YourTasks.ViewModels
{  
    public class SubTaskViewModel : ViewModelBase
    {
        private SubTask? _subTask;
        public SubTask SubTask
        {
            get => _subTask!;
            set => this.RaiseAndSetIfChanged(ref _subTask, value);
        }
    
        public EventHandler? SubTaskDeleteEvent;
        public IReactiveCommand DeleteTaskCommand { get; }

        public SubTaskViewModel(SubTask task)
        {
            SubTask = task;

            DeleteTaskCommand = ReactiveCommand.Create(()=>{
                SubTaskDeleteEvent?.Invoke(this, new EventArgs());
            });
        }
    }
}
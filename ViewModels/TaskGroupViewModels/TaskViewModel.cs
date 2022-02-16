using ReactiveUI;
using YourTasks.Models;
using System.Collections.ObjectModel;

namespace YourTasks.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        private Task? _task;
        private ObservableCollection<Task>? _subTasks;

        public Task Task
        {
            get => _task!;
            set => this.RaiseAndSetIfChanged(ref _task, value);
        }

        public ObservableCollection<Task> SubTasks
        {
            get => _subTasks!;
            set => this.RaiseAndSetIfChanged(ref _subTasks, value);
        }

        public IReactiveCommand? AddSubTaskCommand { get; private set;}

        public TaskViewModel()
        {
            AddSubTaskCommand = ReactiveCommand.Create(()=>{
                System.Console.WriteLine("Add new subtask");
            });
        }

    }
}

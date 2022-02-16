using ReactiveUI;
using System.Collections.ObjectModel;

namespace YourTasks.ViewModels
{
    public class TotalTasksViewModel : TaskGroupViewModel
    {
        private string? _groupName;
        private ObservableCollection<TaskViewModel>? _tasks;

        public override string GroupName { 
            get => _groupName!; 
            set => this.RaiseAndSetIfChanged(ref _groupName, value);
        }

        public override ObservableCollection<TaskViewModel> Tasks { 
            get => _tasks!; 
            set => this.RaiseAndSetIfChanged(ref _tasks, value); 
        }

        public IReactiveCommand AddTaskCommand { get; private set; } 
        public TotalTasksViewModel()
        {
            AddTaskCommand = ReactiveCommand.Create(()=>{
                System.Console.WriteLine("Add new task");
            });
        }

    }
}
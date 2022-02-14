using ReactiveUI;
using System.Collections.ObjectModel;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class TaskGroupViewModel : ViewModelBase
    {
        private string? _groupName;
        private ObservableCollection<TaskViewModel>? _tasks;

        public string GroupName
        {
            get => _groupName!;
            set => this.RaiseAndSetIfChanged(ref _groupName, value);
        }

        public ObservableCollection<TaskViewModel> Tasks
        {
            get => _tasks!;
            set => this.RaiseAndSetIfChanged(ref _tasks, value);
        }

        public TaskGroupViewModel()
        {

        }
    }

}
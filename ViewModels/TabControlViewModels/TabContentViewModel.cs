using ReactiveUI;
using System.Collections.ObjectModel;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class TabContentViewModel : ViewModelBase
    {
        private TotalTasksViewModel? _tasks;
        private CompletedTasksViewModel? _completedTasks;

        public TotalTasksViewModel Tasks
        {
            get => _tasks!;
            set => this.RaiseAndSetIfChanged(ref _tasks, value); 
        }

        public CompletedTasksViewModel CompletedTasks
        {
            get => _completedTasks!;
            set => this.RaiseAndSetIfChanged(ref _completedTasks, value);
        }

        public TabContentViewModel()
        {

        }

    }
}

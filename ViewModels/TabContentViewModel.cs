using ReactiveUI;
using System.Collections.ObjectModel;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class TabContentViewModel : ViewModelBase
    {
        private TaskGroupViewModel? _tasks;
        private TaskGroupViewModel? _completedTasks;

        public TaskGroupViewModel Tasks
        {
            get => _tasks!;
            set => this.RaiseAndSetIfChanged(ref _tasks, value); 
        }

        public TaskGroupViewModel CompletedTasks
        {
            get => _completedTasks!;
            set => this.RaiseAndSetIfChanged(ref _completedTasks, value);
        }

        public TabContentViewModel()
        {

        }

    }
}

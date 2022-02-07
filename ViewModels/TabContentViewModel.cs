using ReactiveUI;
using System.Collections.ObjectModel;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class TabContentViewModel : ViewModelBase
    {
        private ObservableCollection<Task>? _totalTasks;
        private ObservableCollection<Task>? _comletedTasks;

        public ObservableCollection<Task> TotalTasks
        {
            get => _totalTasks!;
            set => this.RaiseAndSetIfChanged(ref _totalTasks, value);
        }

        public ObservableCollection<Task> CompletedTasks
        {
            get => _comletedTasks!;
            set => this.RaiseAndSetIfChanged(ref _comletedTasks, value);
        }

        public TabContentViewModel()
        {

        }

    }
}

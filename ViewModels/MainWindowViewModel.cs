using ReactiveUI;

namespace YourTasks.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private TasksViewModel? _tasksViewModel;
        public TasksViewModel TasksViewModel
        {
            get => _tasksViewModel!;
            set => this.RaiseAndSetIfChanged(ref _tasksViewModel, value);
        }

        public MainWindowViewModel()
        {
            TasksViewModel = new TasksViewModel(){

            };
        }
    }
}

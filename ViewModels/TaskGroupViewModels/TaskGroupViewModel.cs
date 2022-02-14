using System.Collections.ObjectModel;

namespace YourTasks.ViewModels
{
    public abstract class TaskGroupViewModel : ViewModelBase
    {
        public abstract string GroupName { get; set; }
        public abstract ObservableCollection<TaskViewModel> Tasks { get; set; }
    }

}
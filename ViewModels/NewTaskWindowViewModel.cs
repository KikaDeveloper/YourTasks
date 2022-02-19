using ReactiveUI;
using System.Reactive;
using YourTasks.Models;
namespace YourTasks.ViewModels
{
    public class NewTaskWindowViewModel : ViewModelBase
    {

        private string? _text;
        private string? _description;

        public string Text
        {
            get => _text!;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        public string Description
        {
            get => _description!;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }        

        public ReactiveCommand<Unit, Task> AddNewTaskCommand { get; }

        public NewTaskWindowViewModel()
        {
            AddNewTaskCommand = ReactiveCommand.Create<Task>(()=> {return new Task();});
        }
    }
}
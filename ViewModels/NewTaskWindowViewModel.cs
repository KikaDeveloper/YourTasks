using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Abstractions;
using System;
using System.Reactive;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class NewTaskWindowViewModel : ViewModelBase, IValidatableViewModel
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

        public ValidationContext ValidationContext { get; } = new ValidationContext(); 

        public ReactiveCommand<Unit, Task> AddNewTaskCommand { get; }

        public NewTaskWindowViewModel()
        {
            // field validation
            var textIsValid = this.WhenAnyValue(
                x => x.Text,
                (text) => !string.IsNullOrEmpty(text) 
            );

            AddNewTaskCommand = ReactiveCommand.Create<Task>(
                () => {return new Task{
                    Text = Text,
                    Description = Description,
                    CreationDateTime = DateTime.Today.ToString("d MMMM yyyy"),
                    IsCompleted = false,
                    SubTasks = new System.Collections.ObjectModel.ObservableCollection<SubTask>()
                };},
                textIsValid);
        }
    }
}
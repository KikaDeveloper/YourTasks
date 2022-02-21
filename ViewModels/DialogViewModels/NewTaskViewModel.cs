using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Abstractions;
using System;
using System.Reactive;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class NewTaskViewModel : ViewModelBase, IValidatableViewModel
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

        public ReactiveCommand<Unit, TaskBase> AddNewTaskCommand { get; }

        public NewTaskViewModel(bool isTask)
        {
            // field validation
            var textIsValid = this.WhenAnyValue(
                x => x.Text,
                (text) => !string.IsNullOrEmpty(text) 
            );

            AddNewTaskCommand = ReactiveCommand.Create<TaskBase>(
                () => {
                    if(isTask)
                        return NewTask();
                    else return NewSubTask();
                },
            textIsValid);
        }

        private Task NewTask() => new Task 
        {
            Text = Text,
            Description = Description,
            CreationDateTime = DateTime.Today.ToString("d MMMM yyyy"),
            IsCompleted = false
        };

        private SubTask NewSubTask() => new SubTask 
        {
            Text = Text,
            Description = Description,
            CreationDateTime = DateTime.Today.ToString("d MMMM yyyy"),
            IsCompleted = false
        };
    }
}
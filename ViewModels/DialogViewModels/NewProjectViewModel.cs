using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Abstractions;
using System;
using System.Reactive;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class NewProjectViewModel : ViewModelBase, IValidatableViewModel
    {
        private string? _name;  
        private string? _ellipseColor;
        private string? _description; 

        public string Name
        {
            get => _name!;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string EllipseColor
        {
            get => _ellipseColor!;
            set => this.RaiseAndSetIfChanged(ref _ellipseColor, value);
        }

        public string Description
        {
            get => _description!;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public ReactiveCommand<Unit, Project> AddNewProjectCommand { get; }

        public NewProjectViewModel()
        {
            var nameIsValid = this.WhenAnyValue(
                x => x.Name,
                (name) => !string.IsNullOrEmpty(name)
            );

            var colorIsValid = this.WhenAnyValue(
                x => x.EllipseColor,
                (color) => !string.IsNullOrEmpty(color)
            );

            AddNewProjectCommand = ReactiveCommand.Create(() 
                => new Project());
        }
    }
}
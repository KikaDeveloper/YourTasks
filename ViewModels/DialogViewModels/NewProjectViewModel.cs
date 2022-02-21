using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Abstractions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reactive;
using YourTasks.Models;
using YourTasks.Services;

namespace YourTasks.ViewModels
{
    public class NewProjectViewModel : ViewModelBase, IValidatableViewModel
    {
        private string? _name;  
        private string? _ellipseColor;
        private string? _description; 
        private Color _selectedColor;

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

        public IEnumerable<Color> Colors
        {
            get => ColorRepository.Colors;
        }

        public Color SelectedColor
        {
            get => _selectedColor;
            set { 
                this.RaiseAndSetIfChanged(ref _selectedColor, value);
                EllipseColor = value.ColorValue;
            }
        }

        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public ReactiveCommand<Unit, Project> AddProjectCommand { get; }

        public NewProjectViewModel()
        {
            SelectedColor = Colors.First();

            var fieldsisValid = this.WhenAnyValue(
                x => x.Name,
                x => x.EllipseColor,
                x => x.Description,
                (name, color, description) =>
                {   return CheckFields(name, color, description); }
                
            );

            AddProjectCommand = ReactiveCommand.Create<Project>(
                () => NewProject(),
                fieldsisValid);
        }

        private Project NewProject()
            => new Project{
                Name = Name,
                EllipseColor = EllipseColor,
                Description = Description,
                Tasks = new System.Collections.ObjectModel.ObservableCollection<Task>()
            };

        private bool CheckFields(string name, string color, string description)
        {
            bool nameIsValid = !string.IsNullOrEmpty(name);
            bool colorIsValid = !string.IsNullOrEmpty(color) && (color[0] == '#') && (color.Length == 7);

            if(string.IsNullOrEmpty(description))
                Description = "None";

            return nameIsValid && colorIsValid;
        }

    }
}
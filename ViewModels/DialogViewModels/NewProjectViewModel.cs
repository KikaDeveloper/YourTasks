using ReactiveUI;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Abstractions;
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
            set => this.RaiseAndSetIfChanged(ref _selectedColor, value);
        }

        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public ReactiveCommand<Unit, Project> AddProjectCommand { get; }

        public NewProjectViewModel()
        {
            SelectedColor = Colors.First();

            var nameIsValid = this.WhenAnyValue(
                x => x.Name,
                (name) => !string.IsNullOrEmpty(name)
            );

            var colorIsValid = this.WhenAnyValue(
                x => x.EllipseColor,
                (color) => !string.IsNullOrEmpty(color)
            );

            AddProjectCommand = ReactiveCommand.Create(() 
                => new Project());
        }
    }
}
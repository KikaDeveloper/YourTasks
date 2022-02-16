using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private string? _name;
        private string? _ellipseColor;

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

        public ProjectViewModel(Project project)
        {
            Name = project.Name!;
            EllipseColor = project.EllipseColor!;
        }

    }
}
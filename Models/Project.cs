using System;
using System.Collections.ObjectModel;

namespace YourTasks.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? EllipseColor { get; set; }
        public ObservableCollection<Task>? Tasks { get; set; }
    }
}
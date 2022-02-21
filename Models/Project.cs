using SQLite;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace YourTasks.Models
{
    [Table("Projects")]
    public class Project : ReactiveObject
    {

        #region protected fields
            protected int _id;
            protected string? _name;
            protected string? _ellipseColor;
            protected string? _description;
            protected ObservableCollection<Task>? _tasks;
        #endregion

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id 
        { 
            get => _id; 
            set => this.RaiseAndSetIfChanged(ref _id, value); 
        }

        [Column("project_name")]
        public string? Name 
        { 
            get => _name!; 
            set => this.RaiseAndSetIfChanged(ref _name, value); 
        }

        [Column("ellipse_color")]
        public string? EllipseColor 
        { 
            get => _ellipseColor!; 
            set => this.RaiseAndSetIfChanged(ref _ellipseColor, value); 
        }

        [Column("description")]
        public string? Description 
        { 
            get => _description!; 
            set => this.RaiseAndSetIfChanged(ref _description, value); 
        }

        [Ignore]
        public ObservableCollection<Task>? Tasks 
        { 
            get => _tasks!; 
            set => this.RaiseAndSetIfChanged(ref _tasks, value); 
        }
    }
}
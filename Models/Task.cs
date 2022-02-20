using System.Collections.ObjectModel;
using SQLite;
using ReactiveUI;

namespace YourTasks.Models
{
    [Table("Tasks")]
    public class Task : TaskBase
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public override int Id 
        { 
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        [Column("text")]
        public override string? Text 
        { 
            get => _text!;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        [Column("creation_dateTime")]
        public override string? CreationDateTime 
        { 
            get => _creationDateTime!;
            set => this.RaiseAndSetIfChanged(ref _creationDateTime, value); 
        }

        [Column("is_completed")]
        public override bool IsCompleted 
        { 
            get => _isCompleted; 
            set => this.RaiseAndSetIfChanged(ref _isCompleted, value);
        }

        [Column("description")]
        public override string? Description 
        { 
            get => _description!; 
            set => this.RaiseAndSetIfChanged(ref _description, value); 
        }

        [Ignore()]
        public ObservableCollection<SubTask>? SubTasks 
        { 
            get => _subTasks!; 
            set => this.RaiseAndSetIfChanged(ref _subTasks, value);
        }
        
        protected ObservableCollection<SubTask>? _subTasks;

        [Indexed]
        [Column("project_id")]
        public int ProjectId 
        { 
            get => _projectId; 
            set => this.RaiseAndSetIfChanged(ref _projectId, value); 
        }

        protected int _projectId;
    }
}
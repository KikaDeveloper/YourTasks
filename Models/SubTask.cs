using SQLite;
using ReactiveUI;

namespace YourTasks.Models
{
    [Table("SubTasks")]
    public class SubTask : TaskBase
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

        [Column("creation_datetime")]
        public override string? CreationDateTime 
        { 
            get => _creationDateTime!; 
            set => this.RaiseAndSetIfChanged(ref _creationDateTime, value); 
        }

        [Column("is_completed")]
        public override bool IsCompleted 
        { 
            get => _isCompleted; 
            set {
                this.RaiseAndSetIfChanged(ref _isCompleted, value);
                TaskCompletedEvent?.Invoke(this, new TaskCompletedArgs(value));
            }
        }

        [Column("description")]
        public override string? Description 
        { 
            get => _description!; 
            set => this.RaiseAndSetIfChanged(ref _description, value); 
        }

        [Column("task_id")]
        public int TaskId 
        { 
            get => _taskId; 
            set => this.RaiseAndSetIfChanged(ref _taskId, value);
        }

        protected int _taskId;
    }
}
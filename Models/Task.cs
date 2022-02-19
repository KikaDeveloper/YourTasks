using System;
using System.Collections.ObjectModel;
using SQLite;

namespace YourTasks.Models
{
    [Table("Tasks")]
    public class Task : TaskBase
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public override int Id { get; set; }
        [Column("text")]
        public override string? Text { get; set; }
        [Column("creation_dateTime")]
        public override string? CreationDateTime { get; set; }
        [Column("is_completed")]
        public override bool IsCompleted { get; set; }
        [Column("description")]
        public override string? Description { get; set; }
        [Ignore()]
        public ObservableCollection<SubTask>? SubTasks { get; set;}
        [Indexed]
        [Column("project_id")]
        public int ProjectId { get; set; }
    }
}
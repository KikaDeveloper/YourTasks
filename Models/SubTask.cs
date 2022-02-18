using SQLite;

namespace YourTasks.Models
{
    [Table("SubTasks")]
    public class SubTask : TaskBase
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public override int Id { get; set; }
        [Column("text")]
        public override string? Text { get; set; }
        [Column("creation_datetime")]
        public override string? CreationDateTime { get; set; }
        [Column("is_completed")]
        public override bool IsCompleted { get; set; }
        [Column("description")]
        public override string? Description { get; set; }
        [Column("task_id")]
        public int TaskId { get; set; }
    }
}
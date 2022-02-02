using System;

namespace YourTasks.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsCompleted { get; set; }
        public string? Description { get; set; }
        public Guid TaskGroupId { get; set; }
    }

    public class Subtask
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsCompleted { get; set; }
        public Guid TaskId { get; set; }
    }
}
using System;

namespace YourTasks.Models
{
    public abstract class TaskBase
    {
        public abstract Guid Id { get; set; }
        public abstract string? Text { get; set; }
        public abstract DateTime CreationDateTime { get; set; }
        public abstract bool IsCompleted { get; set; }
        public abstract string? Description { get; set; }
    }
}
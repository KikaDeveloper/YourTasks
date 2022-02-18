using System;

namespace YourTasks.Models
{
    public abstract class TaskBase
    {
        public abstract int Id { get; set; }
        public abstract string? Text { get; set; }
        public abstract string? CreationDateTime { get; set; }
        public abstract bool IsCompleted { get; set; }
        public abstract string? Description { get; set; }
    }
}
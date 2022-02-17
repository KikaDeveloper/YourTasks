using System;
using System.Collections.ObjectModel;

namespace YourTasks.Models
{
    public class Task : TaskBase
    {
        public override Guid Id { get; set; }
        public override string? Text { get; set; }
        public override DateTime CreationDateTime { get; set; }
        public override bool IsCompleted { get; set; }
        public override string? Description { get; set; }
        public ObservableCollection<SubTask>? SubTasks { get; set;}
        public Guid ProjectId { get; set; }
    }
}
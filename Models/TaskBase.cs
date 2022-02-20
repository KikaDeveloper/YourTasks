using System;
using ReactiveUI;

namespace YourTasks.Models
{
    public abstract class TaskBase : ReactiveObject
    {
        #region private fields
            protected int _id;
            protected string? _text;
            protected string? _creationDateTime;
            protected bool _isCompleted;
            protected string? _description;
        #endregion
       
        public EventHandler<TaskCompletedArgs>? TaskCompletedEvent;

        public abstract int Id { get; set; }
        public abstract string? Text { get; set; }
        public abstract string? CreationDateTime { get; set; }
        public abstract bool IsCompleted { get; set; }
        public abstract string? Description { get; set; }
    }
}
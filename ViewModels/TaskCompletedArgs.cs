using System;

namespace YourTasks.ViewModels
{
    public class TaskCompletedArgs : EventArgs
    {
        public bool IsCompleted { get; private set; }

        public TaskCompletedArgs(bool isCompleted)
            => IsCompleted = isCompleted;
    }
}
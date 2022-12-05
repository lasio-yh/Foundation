using System;

namespace Emulator.Shell.Interfaces
{
    [Serializable]
    public class TaskChangedEventArgs : EventArgs
    {
        public TaskChangedEventArgs(string subject, CommandArgs assignedTo)
        {
            Subject = subject;
            AssignedTo = assignedTo;
        }

        public string Subject { get; }

        public CommandArgs AssignedTo { get; }
    }
}

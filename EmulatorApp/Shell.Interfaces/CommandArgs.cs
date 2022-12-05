using System;

namespace Emulator.Shell.Interfaces
{
    [Serializable]
    public class CommandArgs
    {
        public CommandArgs(string firstname, string lastname, string email)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
        }
        
        public string Firstname { get; }

        public string Lastname { get; }

        public string Email { get; }
        
        public override string ToString() => Firstname + " " + Lastname;
    }
}

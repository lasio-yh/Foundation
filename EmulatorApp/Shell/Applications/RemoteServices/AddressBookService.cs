using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Emulator.PluginFramework.Internals;
using Emulator.Shell.Foundation;
using Emulator.Shell.Interfaces;

namespace Emulator.Shell.Applications.RemoteServices
{
    [Export]
    public class AddressBookService : RemoteService, ICommandService
    {
        private readonly TaskScheduler taskScheduler;
        
        public AddressBookService()
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }
        
        public Action<CommandArgs> SelectContactCallback { get; set; }

        public Action<CommandArgs> ContactDeletedCallback { get; set; }
        
        public void SelectContact(CommandArgs contact)
        {
            TaskHelper.Run(() => SelectContactCallback?.Invoke(contact), taskScheduler);
        }

        public void ContactDeleted(CommandArgs contact)
        {
            TaskHelper.Run(() => ContactDeletedCallback?.Invoke(contact), taskScheduler);
        }
    }
}

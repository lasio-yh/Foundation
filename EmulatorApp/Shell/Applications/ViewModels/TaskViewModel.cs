using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows.Input;
using Emulator.Shell.Applications.RemoteServices;
using Emulator.Shell.Applications.Views;
using Emulator.Shell.Interfaces;

namespace Emulator.Shell.Applications.ViewModels
{
    [Export]
    public class TaskViewModel : ViewModel<ITaskView>
    {
        private string subject = "My first task";
        private CommandArgs assignedTo;
        
        [ImportingConstructor]
        public TaskViewModel(ITaskView view, AddressBookService addressBookService) : base(view)
        {
            addressBookService.SelectContactCallback = SelectContact;
            addressBookService.ContactDeletedCallback = ContactDeleted;
        }
        
        public string Subject
        {
            get { return subject; }
            set { SetProperty(ref subject, value); }
        }
        
        public CommandArgs AssignedTo
        {
            get { return assignedTo; }
            set { SetProperty(ref assignedTo, value); }
        }

        public ICommand UpdateTaskCommand { get; set; }
        
        private void SelectContact(CommandArgs contact)
        {
            AssignedTo = contact;
        }

        private void ContactDeleted(CommandArgs contactDto)
        {
            if (AssignedTo?.Firstname == contactDto.Firstname
                && AssignedTo?.Lastname == contactDto.Lastname
                && AssignedTo?.Email == contactDto.Email)
            {
                AssignedTo = null;
            }
        }
    }
}

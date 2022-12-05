namespace Emulator.Shell.Interfaces
{
    public interface ICommandService
    {
        void SelectContact(CommandArgs contact);

        void ContactDeleted(CommandArgs contact);
    }
}

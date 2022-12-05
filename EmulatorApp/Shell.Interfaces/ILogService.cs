namespace Emulator.Shell.Interfaces
{
    public interface ILogService
    {
        void Message(string text);

        void Error(string text);
    }
}

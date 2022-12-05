using System.Waf.Applications;

namespace Emulator.Shell.Applications.Views
{
    public interface ILogView : IView
    {
        void AppendOutputText(string text);

        void AppendErrorText(string text);
    }
}

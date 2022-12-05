using System.Waf.Applications;

namespace Emulator.ControlPlugin.ViewModels
{
    public interface IDialogView : IView
    {
        bool? ShowDialog();

        void Close();
    }
}

using System.Waf.Applications;
using System.Windows.Input;

namespace Emulator.ControlPlugin.ViewModels
{
    public class DialogViewModel : ViewModel<IDialogView>
    {
        public DialogViewModel(IDialogView view) : base(view)
        {
            OkCommand = new DelegateCommand(Close);
            CancelCommand = new DelegateCommand(Close);
        }

        public ICommand OkCommand { get; }

        public ICommand CancelCommand { get; }

        public void ShowDialog()
        {
            ViewCore.ShowDialog();
        }

        private void Close()
        {
            ViewCore.Close();
        }
    }
}

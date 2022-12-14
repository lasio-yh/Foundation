using System.ComponentModel.Composition;
using System.Windows;
using Emulator.Shell.Applications.Views;
using MahApps.Metro.Controls;

namespace Emulator.Shell.Presentation.Views
{
    [Export(typeof(IShellView))]
    public partial class ShellWindow : MetroWindow, IShellView
    {
        public ShellWindow()
        {
            InitializeComponent();
            Loaded += LoadedHandler;
        }

        private void LoadedHandler(object sender, RoutedEventArgs e)
        {
            pluginsBox.Focus();
        }
    }
}

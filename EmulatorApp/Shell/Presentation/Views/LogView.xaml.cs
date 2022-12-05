using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Emulator.Shell.Applications.Views;

namespace Emulator.Shell.Presentation.Views
{
    [Export(typeof(ILogView))]
    public partial class LogView : ILogView
    {
        public LogView()
        {
            InitializeComponent();
            outputBox.TextChanged += OutputBoxTextChanged;
        }
        
        public void AppendOutputText(string text)
        {
            outputParagraph.Inlines.Add(text);
        }

        public void AppendErrorText(string text)
        {
            outputParagraph.Inlines.Add(new Run(text) { Foreground = (Brush)FindResource("ErrorForeground") });
        }

        private void OutputBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            outputBox.ScrollToEnd();
        }
    }
}

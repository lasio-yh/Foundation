using System.ComponentModel.Composition;
using Emulator.Shell.Applications.Views;

namespace Emulator.Shell.Presentation.Views
{
    [Export(typeof(ITaskView))]
    public partial class TaskView : ITaskView
    {
        public TaskView()
        {
            InitializeComponent();
        }
    }
}

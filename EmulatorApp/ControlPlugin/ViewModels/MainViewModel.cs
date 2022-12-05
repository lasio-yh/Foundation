using System.Collections.Generic;
using System.Waf.Applications;
using System.Windows.Input;
using Emulator.ControlPlugin.Models;
using Emulator.ControlPlugin.Properties;

namespace Emulator.ControlPlugin.ViewModels
{
    public class MainViewModel : ViewModel<IMainView>
    {
        private string taskSubject;
      
        public MainViewModel(IMainView view) : base(view)
        {
        }

        public string TestApplicationSetting => Settings.Default.TestSetting;

        public ICommand ShowDialogCommand { get; set; }
        
        public string TaskSubject
        {
            get { return taskSubject; }
            set { SetProperty(ref taskSubject, value); }
        }

        public ICommand BlockUIThreadCommand { get; set; }

        public ICommand ExceptionUIThreadCommand { get; set; }

        public ICommand ExceptionBackgroundThreadCommand { get; set; }

        public ICommand ExceptionTaskCommand { get; set; }

        public ICommand RunGarbageCollectorCommand { get; set; }

        public IReadOnlyList<Entity> ListItemSource { get; set; }

        private Entity selectedItem;
        public Entity SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public ICommand SelectCommand { get; set; }

        public ICommand DeleteCommand { get; set; }
    }
}

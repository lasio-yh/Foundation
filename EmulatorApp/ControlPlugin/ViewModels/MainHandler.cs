using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Waf.Applications;
using BaseCorePlugin.Contracts;
using BaseCorePlugin.Services;
using Emulator.ControlPlugin.Models;
using Emulator.Shell.Interfaces;

namespace Emulator.ControlPlugin.ViewModels
{
    internal class MainHandler
    {
        private readonly ILogService logService;
        private readonly IDeviceService deviceService;
        private readonly ICommandService commandService;
        private readonly IEventAggregator eventAggregator;
        private readonly MainViewModel mainViewModel;
        private readonly Func<DialogViewModel> dialogViewModelFactory;
        private readonly DelegateCommand showDialogCommand;
        private readonly DelegateCommand LoadDeviceCommand;
        private readonly DelegateCommand UpdateBrightnessCommand;
        private readonly DelegateCommand LoadPrinterCommand;
        private readonly DelegateCommand exceptionTaskCommand;
        private readonly DelegateCommand runGarbageCollectorCommand;
        private readonly DelegateCommand selectCommand;
        private readonly DelegateCommand deleteCommand;
        private IDisposable taskChangedHandlerObserver;
        private readonly DataSource datasource;

        public MainHandler(ILogService logService, DeviceService deviceService, ICommandService commandService, IEventAggregator eventAggregator, MainViewModel mainViewModel, Func<DialogViewModel> dialogViewModelFactory)
        {
            this.logService = logService;
            this.deviceService = deviceService;
            this.commandService = commandService;
            this.eventAggregator = eventAggregator;
            this.mainViewModel = mainViewModel;
            this.dialogViewModelFactory = dialogViewModelFactory;
            showDialogCommand = new DelegateCommand(ShowDialog);
            LoadDeviceCommand = new DelegateCommand(LoadDeviceInfo);
            UpdateBrightnessCommand = new DelegateCommand(ChangeBrightness);
            LoadPrinterCommand = new DelegateCommand(LoadPrintList);
            exceptionTaskCommand = new DelegateCommand(ExceptionTask);
            runGarbageCollectorCommand = new DelegateCommand(RunGarbageCollector);
            selectCommand = new DelegateCommand(Select, CanSelect);
            deleteCommand = new DelegateCommand(Delete, CanDelete);
            datasource = new DataSource();
        }
        
        public void Initialize()
        {
            taskChangedHandlerObserver = eventAggregator.GetEvent<TaskChangedEventArgs>().ObserveOnDispatcher().Subscribe(TaskChangedHandler);
            mainViewModel.ShowDialogCommand = showDialogCommand;
            mainViewModel.BlockUIThreadCommand = LoadDeviceCommand;
            mainViewModel.ExceptionUIThreadCommand = UpdateBrightnessCommand;
            mainViewModel.ExceptionBackgroundThreadCommand = LoadPrinterCommand;
            mainViewModel.ExceptionTaskCommand = exceptionTaskCommand;
            mainViewModel.RunGarbageCollectorCommand = runGarbageCollectorCommand;

            mainViewModel.SelectCommand = selectCommand;
            mainViewModel.DeleteCommand = deleteCommand;
            mainViewModel.ListItemSource = datasource.Items;

            mainViewModel.PropertyChanged += ContactListViewModelPropertyChanged;
        }

        public void Shutdown()
        {
            taskChangedHandlerObserver.Dispose();
        }

        private void TaskChangedHandler(TaskChangedEventArgs e)
        {
            mainViewModel.TaskSubject = e.Subject;
        }

        private void ShowDialog()
        {
            logService.Message("Show dialog", true);

            // Note: This does not behave as modal dialog because it cannot attach to the parent window (Shell) which comes from another process.
            dialogViewModelFactory().ShowDialog();
        }

        private void LoadDeviceInfo()
        {
            datasource.Clear();
            deviceService.GetDeviceInfoList();
        }

        private void ChangeBrightness()
        {

        }

        private void LoadPrintList()
        {

        }

        private void ExceptionTask()
        {
            logService.Message("Exception on task", true);
            Task.Factory.StartNew(() => { throw new InvalidOperationException("Exception on task."); });
        }

        private void RunGarbageCollector()
        {
            logService.Message("Run garbage collector", true);
            GC.Collect();
        }

        public void AddItem(string entity)
        {
            datasource.Add(new Entity { Id = entity.ToString(), DataName = entity.ToString(), DataValue = entity.ToString(), OrderNumber = entity.ToString(), Description = "" });
        }

        private bool CanSelect()
        {
            return mainViewModel.SelectedItem != null;
        }

        private void Select()
        {
            logService.Message("Select contact", true);
        }

        private bool CanDelete()
        {
            return mainViewModel.SelectedItem != null;
        }

        private void Delete()
        {
            var contactToDelete = mainViewModel.SelectedItem;
            datasource.Remove(contactToDelete);
            logService.Message("Delete contact", true);
        }

        private void ContactListViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.SelectedItem))
            {
                selectCommand.RaiseCanExecuteChanged();
                deleteCommand.RaiseCanExecuteChanged();
            }
        }
    }
}

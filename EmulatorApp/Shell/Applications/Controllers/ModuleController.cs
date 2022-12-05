﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Waf.Applications;
using Emulator.PluginFramework;
using Emulator.Shell.Applications.RemoteServices;
using Emulator.Shell.Applications.Services;
using Emulator.Shell.Applications.ViewModels;
using Emulator.Shell.Foundation;
using Emulator.Shell.Interfaces;

namespace Emulator.Shell.Applications.Controllers
{
    [Export(typeof(IModuleController))]
    internal class ModuleController : IModuleController
    {
        private readonly TaskScheduler taskScheduler;
        private readonly Lazy<ShellViewModel> shellViewModel;
        private readonly Lazy<LogViewModel> logViewModel;
        private readonly Lazy<TaskViewModel> taskViewModel;
        private readonly Lazy<LogService> logService;
        private readonly Lazy<AddressBookService> addressBookService;
        private readonly Lazy<IEventAggregator> eventAggregator;
        private readonly PluginManager pluginManager;
        private readonly ObservableCollection<PluginInfo> plugins;
        private readonly ObservableCollection<object> pluginViews;
        private readonly DelegateCommand loadCommand;
        private readonly DelegateCommand unloadCommand;
        private readonly DelegateCommand updateTaskCommand;
            
        [ImportingConstructor]
        public ModuleController(Lazy<ShellViewModel> shellViewModel, Lazy<LogViewModel> logViewModel, Lazy<TaskViewModel> taskViewModel, 
            Lazy<LogService> logService, Lazy<AddressBookService> addressBookService, Lazy<IEventAggregator> eventAggregator)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.shellViewModel = shellViewModel;
            this.logViewModel = logViewModel;
            this.taskViewModel = taskViewModel;
            this.logService = logService;
            this.addressBookService = addressBookService;
            this.eventAggregator = eventAggregator;
            pluginManager = new PluginManager();
            pluginManager.PluginUnloaded += PluginUnloaded;
            plugins = new ObservableCollection<PluginInfo>();
            pluginViews = new ObservableCollection<object>();
            loadCommand = new DelegateCommand(Load, CanLoad);
            unloadCommand = new DelegateCommand(Unload, CanUnload);
            updateTaskCommand = new DelegateCommand(UpdateTask);
        }

        private ShellViewModel ShellViewModel => shellViewModel.Value;

        private LogViewModel LogViewModel => logViewModel.Value;

        private TaskViewModel TaskViewModel => taskViewModel.Value;

        public void Initialize()
        {
            RemoteServiceLocator.InitializeIpcChannel(RemoteServiceLocator.GetIpcPortName<ICommandService>());
            RemoteServiceLocator.RegisterInstance<ICommandService>(addressBookService.Value);
            RemoteServiceLocator.RegisterInstance<ILogService>(logService.Value);
            RemoteServiceLocator.RegisterInstance<IEventAggregator>((MarshalByRefObject)eventAggregator.Value);
            
            ShellViewModel.Plugins = plugins;
            ShellViewModel.PluginViews = pluginViews;
            ShellViewModel.LoadCommand = loadCommand;
            ShellViewModel.UnloadCommand = unloadCommand;
            ShellViewModel.LogView = LogViewModel.View;
            ShellViewModel.TaskView = TaskViewModel.View;
            TaskViewModel.UpdateTaskCommand = updateTaskCommand;
            ShellViewModel.PropertyChanged += ShellViewModelPropertyChanged;
        }

        public void Run()
        {
            Discover();
            ShellViewModel.Show();
        }

        public void Shutdown()
        {
            pluginManager.Dispose();
        }

        private async void Discover()
        {
            var newPlugins = await PluginManager.DiscoverAsync();
            foreach (var plugin in newPlugins)
            {
                plugins.Add(plugin);
            }
            ShellViewModel.SelectedPlugin = plugins.FirstOrDefault();
        }

        private bool CanLoad()
        {
            return ShellViewModel.SelectedPlugin != null;
        }

        private void Load()
        {
            var newPluginView = pluginManager.Load(ShellViewModel.SelectedPlugin);
            pluginViews.Add(newPluginView);
            ShellViewModel.SelectedPluginView = newPluginView;
        }

        private bool CanUnload()
        {
            return ShellViewModel.SelectedPluginView != null;
        }

        private void Unload()
        {
            pluginManager.Unload(ShellViewModel.SelectedPluginView);
        }

        private void PluginUnloaded(object sender, PluginUnloadedEventArgs e)
        {
            TaskHelper.Run(() => pluginViews.Remove(e.PluginView), taskScheduler);
        }

        private void UpdateTask()
        {
            eventAggregator.Value.Publish(new TaskChangedEventArgs(TaskViewModel.Subject, TaskViewModel.AssignedTo));
        }

        private void ShellViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ShellViewModel.SelectedPlugin))
                loadCommand.RaiseCanExecuteChanged();
            else if (e.PropertyName == nameof(ShellViewModel.SelectedPluginView))
                unloadCommand.RaiseCanExecuteChanged();
        }
    }
}

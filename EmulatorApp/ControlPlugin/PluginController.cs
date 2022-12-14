using System.Globalization;
using System.Threading;
using Emulator.PluginFramework;
using Emulator.ControlPlugin.ViewModels;
using Emulator.ControlPlugin.Views;
using Emulator.Shell.Interfaces;
using System.Management;
using BaseCorePlugin.Services;

namespace Emulator.ControlPlugin
{
    public class PluginController : IPluginController
    {
        private ILogService logService;
        private IEventAggregator eventAggregator;
        private MainHandler Handler;
        private DeviceService deviceService;
        private ICommandService commandService;

        public void Initialize()
        {
            logService = RemoteServiceLocator.GetService<ILogService>();
            eventAggregator = RemoteServiceLocator.GetService<IEventAggregator>();

            logService.Message("Initialize", true);

            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("de-DE");
            logService.Message("UICulture " + Thread.CurrentThread.CurrentUICulture, true);

            deviceService = new DeviceService(OnReceive);
            logService.Message("Initialize Device Service", true);
        }

        public void OnReceive(object sender)
        {
            if (sender == null)
                return;

            Handler.AddItem(sender.ToString());
        }

        public object CreateMainView()
        {
            var viewModel = new MainViewModel(new MainView());
            Handler = new MainHandler(logService, deviceService, commandService, eventAggregator, viewModel, () => new DialogViewModel(new DialogWindow()));
            Handler.Initialize();
            return viewModel.View;
        }

        public void Shutdown()
        {
            logService.Message("Shutdown", true);
            Handler.Shutdown();
        }
    }
}
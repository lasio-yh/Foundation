﻿using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Emulator.PluginFramework.Internals;
using Emulator.Shell.Foundation;
using Emulator.Shell.Interfaces;

namespace Emulator.Shell.Applications.RemoteServices
{
    [Export]
    public class LogService : RemoteService, ILogService
    {
        private readonly TaskScheduler taskScheduler;

        public LogService()
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        public Action<string> MessageCallback { get; set; }

        public Action<string> ErrorCallback { get; set; }
        
        public void Message(string text)
        {
            TaskHelper.Run(() => MessageCallback?.Invoke(text), taskScheduler);
        }

        public void Error(string text)
        {
            TaskHelper.Run(() => ErrorCallback?.Invoke(text), taskScheduler);
        }
    }
}

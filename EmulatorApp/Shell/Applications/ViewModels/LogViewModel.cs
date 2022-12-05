﻿using System;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using Emulator.Shell.Applications.RemoteServices;
using Emulator.Shell.Applications.Views;

namespace Emulator.Shell.Applications.ViewModels
{
    [Export]
    public class LogViewModel : ViewModel<ILogView>
    {
        [ImportingConstructor]
        public LogViewModel(ILogView view, LogService logService) : base(view)
        {
            logService.MessageCallback = AppendOutputText;
            logService.ErrorCallback = AppendErrorText;
        }
        
        public void AppendOutputText(string text)
        {
            ViewCore.AppendOutputText(DateTime.Now.ToString("HH:mm:ss.fff") + " " + text + Environment.NewLine);
        }

        public void AppendErrorText(string text)
        {
            ViewCore.AppendErrorText(DateTime.Now.ToString("HH:mm:ss.fff") + " " + text + Environment.NewLine);
        }
    }
}

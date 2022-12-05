using BaseCorePlugin.Contracts;
using System;
using System.Windows.Threading;

namespace BaseCorePlugin.Services
{
    public class TimerService : ITimerService
    {
        public DispatcherTimer _timer = new DispatcherTimer();
        public delegate void PushHandler(object args);
        public PushHandler Push;

        public void Create(PushHandler callBack, int duetime)
        {
            //TODO 폴링 서비스 시 사용하도록 함.
            Push = callBack;
            _timer.Interval = TimeSpan.FromSeconds(duetime);
            _timer.Tick += OnTimer;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void OnTimer(object sender, EventArgs e)
        {
            Push(e);
        }
    }
}
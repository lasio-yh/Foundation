using static BaseCorePlugin.Services.TimerService;

namespace BaseCorePlugin.Contracts
{
    public interface ITimerService
    {
        void Create(PushHandler callBack, int duetime);
        void Start();
        void Stop();
    }
}

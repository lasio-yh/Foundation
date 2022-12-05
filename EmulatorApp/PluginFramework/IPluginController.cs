namespace Emulator.PluginFramework
{
    public interface IPluginController
    {
        void Initialize();

        object CreateMainView();
        
        void Shutdown();
    }
}

using System.AddIn.Contract;
using System.Runtime.Remoting.Messaging;

namespace Emulator.PluginFramework.Internals
{
    public interface IPluginLoader
    {
        INativeHandleContract LoadPlugin(string assembly, string typeName);

        [OneWay]
        void Shutdown();
    }
}

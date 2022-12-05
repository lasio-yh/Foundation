using BaseCorePlugin.Model;
using System.Drawing;
using System.Threading.Tasks;

namespace BaseCorePlugin.Contracts
{
    public interface IFileService
    {
        T Read<T>(string folderPath, string fileName);
        ResultMapModel Save<T>(string folderPath, string fileName, T content);
        ResultMapModel Copy(string sourcepath, string sourcename, string targetpath, string targetname, int bufsize);
        ResultMapModel Delete(string folderPath, string fileName);
        string EncryptString(string InputText, string Password);
        string DecryptString(string InputText, string Password);
        T OnDeepCopy<T>(T obj);
        Image ImageResize(Image source, int width, int height);
        Task<Image> GetImageAsync(string uri);
    }
}

using BaseCorePlugin.Contracts;
using BaseCorePlugin.Model;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseCorePlugin.Services
{
    public class FileService : IFileService
    {
        public delegate void PushHandler(object args);
        public PushHandler _push;

        /// <summary>
        /// 파일 불러오기
        /// </summary>
        /// <param name="folderPath">디렉토리 경로</param>
        /// <param name="fileName">파일명</param>
        /// <return>T</return>
        public T Read<T>(string folderPath, string fileName)
        {
            var path = Path.Combine(folderPath, fileName);
            if (!File.Exists(path)) 
                return default;

            var json = File.ReadAllText(path);
           
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 파일 저장하기
        /// </summary>
        /// <param name="folderPath">디렉토리 경로</param>
        /// <param name="fileName">파일명</param>
        /// <param name="content">파일타입</param>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel Save<T>(string folderPath, string fileName, T content)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileContent = JsonConvert.SerializeObject(content);
                File.WriteAllText(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8);
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 파일 복사하기
        /// </summary>
        /// <param name="sourcepath">대상 경로</param>
        /// <param name="sourcename">대상 파일명</param>
        /// <param name="targetpath">복사 할 경로</param>
        /// <param name="targetname">복사 할 파일명</param>
        /// <param name="bufsize">버퍼 크기</param>
        /// <return>ResultMapModel</return>
        public ResultMapModel Copy(string sourcePath, string sourceName, string targetPath, string targetName, int bufSize)
        {
            try
            {
                using (var sourceStream = new FileStream(Path.Combine(sourcePath, sourceName), FileMode.Open))
                {
                    var buf = new byte[bufSize];
                    using (var targetStream = new FileStream(Path.Combine(targetPath, targetName), FileMode.Create))
                    {
                        var idx = 0;
                        while ((idx = sourceStream.Read(buf, 0, buf.Length)) > 0)
                        {
                            targetStream.Write(buf, 0, idx);
                        }
                    }
                    buf = null;
                }

                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 파일 삭제하기
        /// </summary>
        /// <param name="folderPath">디렉토리 경로</param>
        /// <param name="fileName">파일명</param>
        /// <return>ResultMapModel</return>
        public ResultMapModel Delete(string folderPath, string fileName)
        {
            try
            {
                if (fileName != null && File.Exists(Path.Combine(folderPath, fileName))) 
                    File.Delete(Path.Combine(folderPath, fileName));
                
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 문자열을 암호화 합니다.
        /// </summary>
        public string EncryptString(string InputText, string Password)
        {
            var EncryptedData = string.Empty;
            try
            {
                var RijndaelCipher = new RijndaelManaged();
                var PlainText = Encoding.Unicode.GetBytes(InputText);
                var Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
                var SecretKey = new PasswordDeriveBytes(Password, Salt);
                var Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                var memoryStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(PlainText, 0, PlainText.Length);
                cryptoStream.FlushFinalBlock();
                var CipherBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                EncryptedData = Convert.ToBase64String(CipherBytes);
            }
            catch
            {

            }
            return EncryptedData;
        }

        /// <summary>
        /// 문자열을 복호화 합니다.
        /// </summary>
        public string DecryptString(string InputText, string Password)
        {
            var DecryptedData = string.Empty;
            try
            {
                var RijndaelCipher = new RijndaelManaged();
                var EncryptedData = Convert.FromBase64String(InputText);
                var Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
                var SecretKey = new PasswordDeriveBytes(Password, Salt);
                var Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                var memoryStream = new MemoryStream(EncryptedData);
                var cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
                var PlainText = new byte[EncryptedData.Length];
                var DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {

            }
            return DecryptedData;
        }

        /// <summary>
        /// HTML 문자열을 일반문자열로 변환합니다.
        /// </summary>
        public string ConvertHtmlToString(string htmlString)
        {
            try
            {
                // Remove new lines since they are not visible in HTML
                htmlString = htmlString.Replace("\n", " ");

                // Remove tab spaces
                htmlString = htmlString.Replace("\t", " ");

                // Remove multiple white spaces from HTML
                htmlString = Regex.Replace(htmlString, "\\s+", " ");

                // Remove HEAD tag
                htmlString = Regex.Replace(htmlString, "<head.*?</head>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

                // Remove any JavaScript
                htmlString = Regex.Replace(htmlString, "<script.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

                // Replace special characters like &, <, >, " etc.
                StringBuilder sbHTML = new StringBuilder(htmlString);

                // Note: There are many more special characters, these are just
                // most common. You can add new characters in this arrays if needed
                string[] OldWords = { "&nbsp;", "&amp;", "&quot;", "&lt;", "&gt;", "&reg;", "&copy;", "&bull;", "&trade;", "&#39;" };
                string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢", "\'" };
                for (int i = 0; i < OldWords.Length; i++)
                {
                    sbHTML.Replace(OldWords[i], NewWords[i]);
                }

                // Check if there are line breaks (<br>) or paragraph (<p>)
                sbHTML.Replace("<br>", "\n<br>");
                sbHTML.Replace("<br ", "\n<br ");
                sbHTML.Replace("<p ", "\n<p ");

                // Finally, remove all HTML tags and return plain text
                return Regex.Replace(sbHTML.ToString(), "<[^>]*>", "");
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 객체 복사하기
        /// </summary>
        public T OnDeepCopy<T>(T obj)
        {
            using (var stream = new MemoryStream())
            {
                var bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, obj);
                stream.Position = 0;
                return (T)bFormatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// 이미지 리사이징
        /// </summary>
        public Image ImageResize(Image source, int width, int height)
        {
            var dest = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(dest))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawImage(source, new RectangleF(0, 0, width, height), new RectangleF(new PointF(0, 0), source.Size), GraphicsUnit.Pixel);
            }
            return dest;
        }

        /// <summary>
        /// 웹 이미지 다운로드
        /// </summary>
        public async Task<Image> GetImageAsync(string uri)
        {
            try
            {
                var req = WebRequest.Create($"{uri}");
                var res = await req.GetResponseAsync();
                var imgStream = res.GetResponseStream();
                var result = Image.FromStream(imgStream);
                imgStream.Close();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

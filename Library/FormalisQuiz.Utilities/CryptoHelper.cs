using System.Security.Cryptography;
using System.Text;

namespace FormalisQuiz.Utilities
{
    public class CryptoHelper
    {
        public static string ConvertToMd5Hash(string text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] retVal = md5.ComputeHash(Encoding.Unicode.GetBytes(text));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}

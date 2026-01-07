using System.Security.Cryptography;
using System.Text;

namespace Dedsi.Core.Extensions;

public static class CryptoExtensions
{
    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string GetMd5Hash(this string text)
    {
        using (var md5 = MD5.Create())
        {
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="ASEKEY"></param>
    /// <param name="ASEIV"></param>
    /// <returns></returns>
    public static string GetAseEncrypt(this string plainText, string ASEKEY, string ASEIV)
    {
        byte[] Key = Encoding.UTF8.GetBytes(ASEKEY);
        byte[] IV = Encoding.UTF8.GetBytes(ASEIV);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="ASEKEY"></param>
    /// <param name="ASEIV"></param>
    /// <returns></returns>
    public static string GetAseDecrypt(this string cipherText, string ASEKEY, string ASEIV)
    {
        byte[] Key = Encoding.UTF8.GetBytes(ASEKEY);
        byte[] IV = Encoding.UTF8.GetBytes(ASEIV);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            byte[] bytes = Convert.FromBase64String(cipherText);

            using (MemoryStream msDecrypt = new MemoryStream(bytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

}
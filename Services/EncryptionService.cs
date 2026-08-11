using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Programming_Contest_Platform.Data.Configurations;

namespace Programming_Contest_Platform.Services;

public class EncryptionService : IEncryptionService
{
    private readonly byte[] _key;

    public EncryptionService(IOptions<EncryptionSettings> options)
    {
        _key = Convert.FromBase64String(options.Value.Key);
    }

    public string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return plainText;

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.GenerateIV();
         // responsible for preparing the Formula of Encryption that will convert the data from [ PlainText ] to [ ChipherText ]
        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        // Instead of Writing the Whole Encryption in the Hard Disk it uses MemoryStream to Transfer Some of encrypted bytes at a time in memory
        using var ms = new MemoryStream();
        // writes from the IV from its 0 index till its length in the ms  
        ms.Write(aes.IV, 0, aes.IV.Length); 
        // using CryptoStream to act as Bridge between the data and the MemoryStream with the Writemode 
        // that means that the data will be encrypted based on the encryptor and written in the MemoryStream 
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        // StremWriter To Convert the String To byte[] and Vice versa   
        using (var sw = new StreamWriter(cs))
        {
            // send to the cs object and the results written to it   
            sw.Write(plainText);
        }
        
        return Convert.ToBase64String(ms.ToArray());
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return cipherText;

        var fullCipher = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.Key = _key;

        var iv = new byte[aes.BlockSize / 8];
        Array.Copy(fullCipher, 0, iv, 0, iv.Length);
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);

        return sr.ReadToEnd().ToString();
    }
}

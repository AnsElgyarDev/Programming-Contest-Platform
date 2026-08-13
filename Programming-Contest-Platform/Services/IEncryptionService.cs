using System.Net.Security;

namespace Programming_Contest_Platform.Services;

public interface IEncryptionService
{
    public string Encrypt(string plainText);
    public string Decrypt(string cipherText);
}
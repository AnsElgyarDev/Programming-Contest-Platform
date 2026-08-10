using System.Net.Security;

namespace Programming_Contest_Platform.Services;

public interface IEncryptionService
{
    public string Encrypt(string text);
    public string Decrypt(string cipherText);
}
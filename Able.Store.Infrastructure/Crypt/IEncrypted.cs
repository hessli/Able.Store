namespace Able.Store.Infrastructure.Crypt
{
    public interface IEncrypted
    {
        byte[] CryptByte(string source);

        string CryptStr(string source);

        byte[] DecryptBytes(string source);

        string DecryptStr(string source);
    }
}

using System;
using System.Text;

namespace Able.Store.Infrastructure.Crypt
{
   public interface IHashCrypt
    {
        byte[] Crypt(string sourceChar, Encoding encoding);
    }
}

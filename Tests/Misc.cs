using System;
using System.Security.Cryptography;
using System.Text;

namespace Tests
{
    public static class Misc
    {
        public static Guid GenerateGuid(string s)
        {
            return string.IsNullOrEmpty(s) 
                ? Guid.Empty
                : new Guid(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(s)));
        }
    }
}

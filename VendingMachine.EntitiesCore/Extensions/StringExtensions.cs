using System;
using System.Security.Cryptography;
using System.Text;

namespace VendingMachine.EntitiesCore.Extensions
{
    internal static class StringExtensions
    {
        public static String ToMD5Hash(this String input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}

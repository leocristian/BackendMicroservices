
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Security.Cryptography;

namespace EnfermeiroService.Lib {

    public class Generics : ControllerBase {
        public ObjectResult ErroServer { get; }

        public Generics() {
            ErroServer = Problem("Erro interno no servidor!", null, 500);
        }

        public string Md5(string source) {
            using MD5 md5Hash = MD5.Create();

            string hash = GetMd5Hash(md5Hash, source);
            
            return hash;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
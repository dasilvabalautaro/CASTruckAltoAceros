using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CASTruck
{
    class CriptoUtil
    {

        byte[] _key;
        byte[] _iv;

        public CriptoUtil()
        {
            _key = Encoding.ASCII.GetBytes("12EstaClave34es56dificil489ssswf");
            _iv = Encoding.ASCII.GetBytes("Devjoker7.37hAES");
        }

        public string encript(string strInput)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(strInput);
            byte[] encripted = null;
            RijndaelManaged cripto = new RijndaelManaged();
            MemoryStream ms = new MemoryStream(inputBytes.Length);
            CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);
            objCryptoStream.Write(inputBytes, 0, inputBytes.Length); 
            objCryptoStream.FlushFinalBlock();
            objCryptoStream.Close();
            encripted = ms.ToArray();
            return Convert.ToBase64String(encripted);
        }

        public string desencript(string strInput)
        {
            try
            {
                byte[] inputBytes = Convert.FromBase64String(strInput);

                byte[] resultBytes = new byte[inputBytes.Length];
                string txtClear = string.Empty;
                RijndaelManaged cripto = new RijndaelManaged();
                MemoryStream ms = new MemoryStream(inputBytes.Length);
                CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(_key, _iv), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(objCryptoStream, true);
                txtClear = sr.ReadToEnd();
                return txtClear;
            }
            catch (CryptographicException ce)
            {
                Console.Write(ce.Message);
                return "";
            }
            
        }

    }
}

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace DingKit
{
    /// <summary>
    /// 对称加密算法类,用于各类信息的加密解密
    /// </summary>
    public class CDataCrypt
    {
        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public string Encrypto(string str)
        {
            char[] Base64Code = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '|', '/', '-' };
            byte empty = (byte)0;
            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(str));
            System.Text.StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            int page = messageLen / 3;
            int use = 0;
            if ((use = messageLen % 3) > 0)
            {
                for (int i = 0; i < 3 - use; i++)
                    byteMessage.Add(empty);
                page++;
            }
            outmessage = new System.Text.StringBuilder(page * 4);
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[3];
                instr[0] = (byte)byteMessage[i * 3];
                instr[1] = (byte)byteMessage[i * 3 + 1];
                instr[2] = (byte)byteMessage[i * 3 + 2];
                int[] outstr = new int[4];
                outstr[0] = instr[0] >> 2;

                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                if (!instr[1].Equals(empty))
                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                else
                    outstr[2] = 64;
                if (!instr[2].Equals(empty))
                    outstr[3] = (instr[2] & 0x3f);
                else
                    outstr[3] = 64;
                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
            }
            return outmessage.ToString();
        }
        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="str">待解密字符串</param>
        /// <returns>解密后的字符串</returns>   
        public string Decrypto(string str)
        {
            if ((str.Length % 4) != 0)
            {
                throw new ArgumentException("不是正确的BASE64编码，请检查。", "str");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(str, "^[A-Z0-9/|-]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("包含不正确的BASE64编码，请检查。", "str");
            }
            string Base64Code = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789|/-";
            int page = str.Length / 4;
            System.Collections.ArrayList outMessage = new System.Collections.ArrayList(page * 3);
            char[] message = str.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
            return System.Text.Encoding.Default.GetString(outbyte);

        }

         /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
		public static string EncryptDing(string Text) 
		{
			return Encrypt(Text,"DingJianLong");
		}
		/// <summary> 
		/// 加密数据 
		/// </summary> 
		/// <param name="Text"></param> 
		/// <param name="sKey"></param> 
		/// <returns></returns> 
		public static string Encrypt(string Text,string sKey) 
		{ 
			DESCryptoServiceProvider des = new DESCryptoServiceProvider(); 
			byte[] inputByteArray; 
			inputByteArray=Encoding.Default.GetBytes(Text); 
			des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8)); 
			des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8)); 
			System.IO.MemoryStream ms=new System.IO.MemoryStream(); 
			CryptoStream cs=new CryptoStream(ms,des.CreateEncryptor(),CryptoStreamMode.Write); 
			cs.Write(inputByteArray,0,inputByteArray.Length); 
			cs.FlushFinalBlock(); 
			StringBuilder ret=new StringBuilder(); 
			foreach( byte b in ms.ToArray()) 
			{ 
				ret.AppendFormat("{0:X2}",b); 
			} 
			return ret.ToString(); 
		} 

		
		
		#region ========解密======== 
   
 
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
		public static string DecryptDing(string Text) 
		{
            return Decrypt(Text, "DingJianLong");
		}
		/// <summary> 
		/// 解密数据 
		/// </summary> 
		/// <param name="Text"></param> 
		/// <param name="sKey"></param> 
		/// <returns></returns> 
		public static string Decrypt(string Text,string sKey) 
		{ 
			DESCryptoServiceProvider des = new DESCryptoServiceProvider(); 
			int len; 
			len=Text.Length/2; 
			byte[] inputByteArray = new byte[len]; 
			int x,i; 
			for(x=0;x<len;x++) 
			{ 
				i = Convert.ToInt32(Text.Substring(x * 2, 2), 16); 
				inputByteArray[x]=(byte)i; 
			} 
			des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8)); 
			des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8)); 
			System.IO.MemoryStream ms=new System.IO.MemoryStream(); 
			CryptoStream cs=new CryptoStream(ms,des.CreateDecryptor(),CryptoStreamMode.Write); 
			cs.Write(inputByteArray,0,inputByteArray.Length); 
			cs.FlushFinalBlock(); 
			return Encoding.Default.GetString(ms.ToArray()); 
		} 
 
		#endregion 


        ///   <summary>
        ///   给一个字符串进行MD5加密
        ///   </summary>
        ///   <param   name="strText">待加密字符串</param>
        ///   <returns>加密后的字符串</returns>
        public static string MD5Encrypt(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider(); 
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length); 
            char[] temp = new char[res.Length]; 
            System.Array.Copy(res, temp, res.Length); 
            return new String(temp);

            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(input));
            //return System.Text.Encoding.Default.GetString(result);
        }


        public static string to32MD5(string str)
        {
            MD5 md5 = MD5.Create();
            string md5str = "";
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < s.Length; i++)
            {
                string str1 = s[i].ToString("X");
                if (str1.Length == 1)
                {
                    str1 = "0" + str1;
                }
                md5str = md5str + str1;
            }
            return md5str.ToLower();
        } 

    }



}

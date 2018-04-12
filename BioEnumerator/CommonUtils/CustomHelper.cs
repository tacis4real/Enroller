using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Windows.Forms;
using BioEnumerator.GSetting;
using DPFP;

namespace BioEnumerator.CommonUtils
{
    public static class CustomHelper
    {

        #region Bio Enroll

        public static void AccessControl(string path)
        {
            try
            {
                var sec = Directory.GetAccessControl(path);
                // Using this instead of the "Everyone" string means we work on non-English systems.
                var everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.FullControl | FileSystemRights.Synchronize,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None,
                    AccessControlType.Allow));
                Directory.SetAccessControl(path, sec);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            //DirectoryInfo dInfo = new DirectoryInfo(file);
            //DirectorySecurity dSecurity = dInfo.GetAccessControl();
            //dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            //dInfo.SetAccessControl(dSecurity);

        }


        public static bool ValidateArray(Template[] arrayObj)
        {
            try
            {
                var check = Array.TrueForAll(arrayObj, x => x == null);
                return check;
            }
            catch (Exception ex)
            {
                return true;
            }
        }


        public static byte[] ConvertImageToByte(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var image = Image.FromStream(stream);
                var memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }


        public static string ConvertImageToBase64String(string path)
        {
            try
            {
                using (var image = Image.FromFile(path))
                {
                    using (var m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        // Convert byte[] to Base64 String
                        string base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Image ConvertBase64StringToImage(string base64)
        {
            var img = Image.FromStream(new MemoryStream(Convert.FromBase64String(base64)));
            return img;
        }
        #endregion



        public static string MakeValidFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return Regex.Replace(name, invalidRegStr, "_");
        }
        public static void DeleteFile(string fileLoc)
        {
            try
            {
                if (System.IO.File.Exists(fileLoc))
                {
                    System.IO.File.Delete(fileLoc);
                }
            }
            catch (Exception)
            {

            }

        }
        public static string RemoveHtml(this string text)
        {

            text = Regex.Replace(text, @"<img\s[^>]*>(?:\s*?</img>)?", "[Image]", RegexOptions.IgnoreCase);
            var noHTML = Regex.Replace(text, @"<[^>]+>|&nbsp;", "").Trim();
            var noHTMLNormalised = Regex.Replace(noHTML, @"\s{2,}", " ");
            return noHTMLNormalised;

        }

        private static string RandomizeStrings(string accessCode)
        {
            accessCode = accessCode.Replace("0", "34");
            var appendStr = "";
            var random = new Random();
            var num = new bool[accessCode.Length];
            for (var i = 0; i < 12; i++)
            {
                int x;
                do { x = random.Next(accessCode.Length); }
                while ((num[x]));
                num[x] = true;
                if (accessCode[x] == 0 && i == 0) { x = random.Next(accessCode.Length); }
                appendStr = appendStr + accessCode[x];
            }
            return appendStr;
        }
        public static string Truncate(this string text, int length)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            if (text.Length <= length)
            {
                return text;
            }
            else
            {
                return text.Substring(0, length) + "...";
            }
        }

        public static string RemoveSpecialCharacters(string str)
        {
            str = str.Replace(" ", "_");
            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.GSetting;
using DevComponents.DotNetBar.MicroCharts;
using DPFP;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.Repository.Helpers
{
    public class EnrollHelper
    {

        public static List<byte[]> ExtractFingerTemplates()
        {

            var retItems = new List<byte[]>();
            try
            {
                if (Utils.CapturedTemplates == null)
                {
                    return new List<byte[]>();
                }

                var templates = Utils.CapturedTemplates;
                if (templates == null) return new List<byte[]>();
               

                foreach (var template in templates)
                {
                    if (template != null)
                    {
                        var fingerprintData = new MemoryStream();
                        template.Serialize(fingerprintData);
                        retItems.Add(fingerprintData.ToArray());
                    }
                }

                if (!retItems.Any())
                {
                    return new List<byte[]>();
                }

                return retItems;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static Template[] ConvertByteListToTemplate(List<byte[]> list)
        {

            var template = new Template[10];
            try
            {

                if (!list.Any())
                {
                    return new Template[10];
                }

                return template;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                //msg = ex.Message;
                return null;
            }

        }
        private void ConstructImagePath()
        {

            #region Folder Structure For Storage

            var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
            var username = "useradmin";
            var year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);

            #endregion

            const string imgName = "636549040395366983";
            const string fileName = imgName + ".jpeg";
            //var fileName = imgName;
            //TODO:Resourse path
            string folderPath = "/Station-" + stationKey + "/" + username + "/" + year + "/" + "/Image/";
            var imageResPath = folderPath + fileName;
            var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
            var imagePath = Path.GetFullPath(dir + imageResPath);

            var img = RetrieveImage(imagePath);

        }
        private Image RetrieveImage(string path)
        {


            #region Folder Structure For Storage

            var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
            var username = Utils.CurrentUser.UserName;
            var year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            //var username = "useradmin";
            

            #endregion

            const string imgName = "636549040395366983";
            const string fileName = imgName + ".jpeg";
            //var fileName = imgName;
            //TODO:Resourse path
            string folderPath = "/Station-" + stationKey + "/" + username + "/" + year + "/" + "/Image/";
            var imageResPath = folderPath + fileName;
            var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
            var imagePath = Path.GetFullPath(dir + imageResPath);

            var img = Image.FromFile(path);
            return img;
            //var img = Image.FromFile(imagePath);
            //using (var img = Image.FromFile(imagePath))
            //{
            //    picImage.Image = new Bitmap(img);
            //}


        }
        public static void SaveImage(Image image, string stationKey, string username, out string msg)
        {

            try
            {
                #region Folder Structure For Storage

                //var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
                //var username = Utils.CurrentUser.UserName;
                var year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);

                var imgName = DateTime.Now.Ticks;
                var fileName = imgName + ".jpeg";
                //var fileName = imgName;
                //TODO:Resourse path
                string folderPath = "/Station-" + stationKey + "/" + username + "/" + year + "/" + "/Image/";
                var imageResPath = folderPath + fileName;
                var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
                var imagePath = Path.GetFullPath(dir + imageResPath);
                if (!Directory.Exists(@dir + @folderPath))
                {
                    try
                    {
                        Directory.CreateDirectory(@dir + @folderPath);
                    }
                    catch (Exception ex)
                    {
                        ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                        msg = ex.Message;
                        return;
                    }
                }
                while (File.Exists(imagePath))
                {
                    fileName = DateTime.Now.Ticks + ".jpeg";
                    imageResPath = folderPath + fileName;
                    imagePath = Path.GetFullPath(dir + imageResPath);
                }


                #endregion
                
                image.Save(imagePath, ImageFormat.Jpeg);
                msg = "";
                //File.WriteAllBytes(@imagePath, imageByteArray);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                msg = ex.Message;
                return;
            }

        }
        public static string SaveImageLocally(Image image, string stationKey, string username, out string msg)
        {

            try
            {
                #region Folder Structure For Storage

                //var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
                //var username = Utils.CurrentUser.UserName;
                var year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);

                var imgName = DateTime.Now.Ticks;
                var fileName = imgName + ".jpeg";
                //var fileName = imgName;
                //TODO:Resourse path
                string folderPath = "/Station-" + stationKey + "/" + username + "/" + year + "/" + "/Image/";
                var imageResPath = folderPath + fileName;
                var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
                var imagePath = Path.GetFullPath(dir + imageResPath);
                if (!Directory.Exists(@dir + @folderPath))
                {
                    try
                    {
                        Directory.CreateDirectory(@dir + @folderPath);
                    }
                    catch (Exception ex)
                    {
                        ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                        msg = ex.Message;
                        return null;
                    }
                }
                while (File.Exists(imagePath))
                {
                    fileName = DateTime.Now.Ticks + ".jpeg";
                    imageResPath = folderPath + fileName;
                    imagePath = Path.GetFullPath(dir + imageResPath);
                }


                #endregion

                image.Save(imagePath, ImageFormat.Jpeg);
                msg = "";
                return imagePath;
                //File.WriteAllBytes(@imagePath, imageByteArray);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                msg = "Processing Error Occurred! " +ex.Message;
                return null;
            }

        }


    }
}

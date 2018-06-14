using ERISCOTools.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERISCOTools
{
    public static class UploadFile
    {
        public static string saveFile(DocumentVM document)
        {
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath(document.server_directory);

                string targetPath = Path.Combine(path, document.file_name);

                deleteFile(document.server_directory + document.file_name);

                byte[] bytes = Convert.FromBase64String(getStringBase64(document.file));

                File.WriteAllBytes(targetPath, bytes); 

                return document.file_name;
            }
            catch(Exception ex)
            {
                throw new Exception("Error pujant arxiu: " + ex.Message);
            }
        }

        public static string getBase64(string path)
        {
            try
            {
                var extension = path.Split('.');
                Byte[] bytes = File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath(path));
                String file = "data:image/" + extension.Last() + ";base64," + Convert.ToBase64String(bytes);
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string getStringBase64(string base64)
        {
            var splitted = base64.Split(',');
            if (splitted.Count() <= 1)
            {
                throw new Exception("Format del document incorrecte... ");
            }
            return splitted[splitted.Count() - 1];
        }

        private static void deleteFile(string url)
        {
            if (System.IO.File.Exists(url))
            {
                System.IO.File.Delete(url);
            }
        }
        
    }
}

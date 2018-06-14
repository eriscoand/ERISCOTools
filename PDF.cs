using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERISCOTools
{
    public class PDFCreator
    {
        private string HTML_TO_PDF_EXE_URL { get; set; }
        private string TEMP_FOLDER { get; set; }

        public PDFCreator(string HTML_TO_PDF_EXE_URL, string TEMP_FOLDER)
        {
            this.HTML_TO_PDF_EXE_URL = HTML_TO_PDF_EXE_URL;
            this.TEMP_FOLDER = TEMP_FOLDER;
        }

        public byte[] generatePDF(string url)
        {
            var htmlUrl = generateHTML(url);
            var destination_path = HttpContext.Current.Server.MapPath(TEMP_FOLDER + Guid.NewGuid().ToString() + ".pdf");
            System.IO.FileInfo file = new System.IO.FileInfo(destination_path);
            file.Directory.Create();

            Process pProcess = new Process();

            try
            {
                pProcess.StartInfo.FileName = HTML_TO_PDF_EXE_URL;

                pProcess.StartInfo.Arguments = string.Format("{0} {1}", htmlUrl, destination_path);
                pProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pProcess.Start();
                pProcess.WaitForExit(60000);

                byte[] buffer = null;
                using (FileStream fs = new FileStream(destination_path, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                }
                return buffer;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                pProcess.Close();
                if (System.IO.File.Exists(htmlUrl))
                {
                    System.IO.File.Delete(htmlUrl);
                }
                if (System.IO.File.Exists(destination_path))
                {
                    System.IO.File.Delete(destination_path);
                }
            }

        }

        private string generateHTML(string url)
        {
            string file_name = Guid.NewGuid().ToString() + ".html";
            string full_destination = "";

            try
            {
                full_destination = HttpContext.Current.Server.MapPath(TEMP_FOLDER + file_name);

                System.IO.FileInfo file = new System.IO.FileInfo(full_destination);
                file.Directory.Create();
                string PaginaSource = generateHtmlSource(url);
                using (FileStream fs = new FileStream(full_destination, FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.WriteLine(PaginaSource);
                    }
                }
                if (System.IO.File.Exists(full_destination))
                {
                    return full_destination;
                }
                else
                {
                    throw new Exception("No s'ha pogut generar l'arxiu HTML.");
                }
            }
            catch (Exception ex)
            {
                if (System.IO.File.Exists(full_destination))
                {
                    System.IO.File.Delete(full_destination);
                }
                throw ex;
            }
        }
        
        private static string generateHtmlSource(string url)
        {
            try
            {
                string data = "";
                if (!String.IsNullOrWhiteSpace(url))
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;

                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(receiveStream);
                        }
                        else
                        {
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        }

                        data = readStream.ReadToEnd();

                        response.Close();
                        readStream.Close();
                    }
                    else
                    {
                        throw new Exception("No s´ha pogut generar el HTML per l´arxiu PDF.");
                    }
                }

                if (!String.IsNullOrWhiteSpace(data))
                {
                    return data;
                }
                else
                {
                    throw new Exception("No s´ha pogut carregar el contingut del PDF.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

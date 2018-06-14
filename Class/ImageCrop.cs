using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERISCOTools.Class
{
    public class ImageCrop
    {
        public double ratio { get; set; }
        public int desired_width { get; set; }
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public string image_file { get; set; }
        public string file_name { get; set; }
        public string server_directory { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPMJoinery.Models
{
    public class Project
    {
        public int Id { get; set; }
        public Guid UserID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }

        //Primary Display Images and Description of Image
        public string ImgFilePath { get; set; }
        public string ImgDescription { get; set; }

        //optional extra images and descriptions
        public string ImgFilePath2 { get; set; }
        public string ImgDescription2 { get; set; }

        public string ImgFilePath3 { get; set; }
        public string ImgDescription3 { get; set; }

        public string ImgFilePath4 { get; set; }
        public string ImgDescription4 { get; set; }

        public string ImgFilePath5 { get; set; }
        public string ImgDescription5 { get; set; }




    }
}
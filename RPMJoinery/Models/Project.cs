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
        public string ImgFilePath { get; set; }
        public List<string> OtherImgsFilePaths { get; set; }




    }
}
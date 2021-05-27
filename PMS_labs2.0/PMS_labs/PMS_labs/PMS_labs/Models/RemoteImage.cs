using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PMS_labs.Models
{
    public class RemoteImage
    {
        [PrimaryKey]
        public string Url { get; set; }

        public string Search { get; set; }
    }
}

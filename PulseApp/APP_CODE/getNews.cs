using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace PulseApp.APP_CODE
{
    [Serializable]
    public class getNews
    {
        public int NewsRecordID { get; set; }
        public string NewsHeader { get; set; }
        public string NewsContent { get; set; }
        public string DateTimeAdded { get; set; }
    }
}
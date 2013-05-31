using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;

namespace EasyClassifieds.Core
{
    public static class GeoCoder
    {
        public static string TryGeocodeZip(string zip)
        {
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, object> obj = (Dictionary<string, object>)jss.DeserializeObject(wc.DownloadString("http://maps.google.com/maps/geo?output=json&q=" + zip));
                return ((obj["Placemark"] as object[])[0] as Dictionary<string, object>)["address"].ToString();
            }
            catch { return null; }
        }
    }
}
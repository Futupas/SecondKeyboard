using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondKeyboard
{
    class JsonDataModel
    {
        public WebsiteModel website = new WebsiteModel();

        public List<WebsiteButtonModel> buttons = new List<WebsiteButtonModel>();
        public List<VirtualPhysicalPathMatchModel> virtualPhysicalPathMatches = new List<VirtualPhysicalPathMatchModel>();

        public class WebsiteButtonModel
        {
            public string top = "";
            public string bottom = "";
            public string left = "";
            public string right = "";
            public string width = "";
            public string height = "";

            public string backgroundColor = "";
            public string text = "";
            public string fontSize = "";
            public string lineHeight = "";
            public string color = "";

            public string position = "";

            public string key = "";
        }

        public class WebsiteModel
        {
            public string ipAddress = "";
            public string indexHtmlPath = "";
        }
        public class VirtualPhysicalPathMatchModel
        {
            public string virtualPath = "";
            public string physicalPath = "";
        }
    }

    //class 
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using TelldusWrapper;

namespace TellstickWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return GetXML();
        }

        public ActionResult TurnOn(int id)
        {
            TurnOn(id, true);
            return GetXML();
        }

        public ActionResult TurnOff(int id)
        {
            TurnOn(id, false);
            return GetXML();
        }

        private void TurnOn(int id, bool state)
        {
            if (id == -1)
                foreach (var item in GetDevices())
                {
                    if (state)
                        TelldusNETWrapper.tdTurnOn(Int32.Parse(item[0]));
                    else
                        TelldusNETWrapper.tdTurnOff(Int32.Parse(item[0]));
                }
            else
            {
                if (state)
                    TelldusNETWrapper.tdTurnOn(id);
                else
                    TelldusNETWrapper.tdTurnOff(id);
            }
        }

        private List<string[]> GetDevices()
        {
            var listOfDevices = new List<string[]>();
            var numberOfDevices = TelldusNETWrapper.tdGetNumberOfDevices();
            for (int i = 0; i < numberOfDevices; i++)
            {
                var id = TelldusNETWrapper.tdGetDeviceId(i);
                var name = TelldusNETWrapper.tdGetName(id);
                listOfDevices.Add(new string[] { id.ToString(), name });
            }
            return listOfDevices;
        }

        private ActionResult GetXML()
        {
            var listOfDevices = GetDevices();
            XmlSerializer ser = new XmlSerializer(listOfDevices.GetType());
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listOfDevices);
            var xml = writer.ToString();
            return this.Content(xml, "text/xml");
        }
    }
}

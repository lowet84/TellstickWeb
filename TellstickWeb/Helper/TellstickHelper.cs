using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelldusWrapper;
using TellstickWeb.Models;

namespace TellstickWeb.Helper
{
    public static class TellstickHelper
    {
        public static List<TellstickDevice> GetDevices()
        {
            var listOfDevices = new List<TellstickDevice>();
            var numberOfDevices = TelldusNETWrapper.tdGetNumberOfDevices();
            for (int i = 0; i < numberOfDevices; i++)
            {
                var id = TelldusNETWrapper.tdGetDeviceId(i);
                var name = TelldusNETWrapper.tdGetName(id);
                listOfDevices.Add(new TellstickDevice()
                {
                    id = id,
                    name = name,
                });
            }
            return listOfDevices;
        }
        public static void Turn(int? id, bool state)
        {
            if (id == null)
                foreach (var item in GetDevices())
                {
                    if (state)
                        TelldusNETWrapper.tdTurnOn(item.id);
                    else
                        TelldusNETWrapper.tdTurnOff(item.id);
                }
            else
            {
                if (state)
                    TelldusNETWrapper.tdTurnOn(id.Value);
                else
                    TelldusNETWrapper.tdTurnOff(id.Value);
            }
        }
    }
}
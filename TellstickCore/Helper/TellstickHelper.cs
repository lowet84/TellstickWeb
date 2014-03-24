using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelldusWrapper;
using TellstickCore.Models;

namespace TellstickCore.Helper
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
                    Id = id,
                    Name = name,
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
                        TelldusNETWrapper.tdTurnOn(item.Id);
                    else
                        TelldusNETWrapper.tdTurnOff(item.Id);
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
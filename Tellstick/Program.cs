using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TelldusWrapper;
using TellstickCore.Helper;

namespace TellstickTest
{
    class Program
    {
        static DateTime _stopTime;

        static void Main(string[] args)
        {
            var wrapper = new TelldusNETWrapper();
            wrapper.tdRegisterDeviceEvent(new TelldusNETWrapper.EventCallbackFunction(DeviceEvent), null);
            while (true)
                Thread.Sleep(10);
        }

        static int DeviceEvent(int deviceId, int method, string data, int callbackId, object context)
        {
            Console.WriteLine(deviceId + " " + method + " " + data + " " + callbackId);
            var time = DateTime.Now - _stopTime;
            if ((deviceId == 8 || deviceId == 9) && time > new TimeSpan(0, 0, 5))
            {
                _stopTime = DateTime.Now;
                var mode = method == 1;
                var devices = new int[] { 4, 5, 6 };
                foreach (var item in devices)
                {
                    TellstickHelper.Turn(item, mode);
                }
            }
            return deviceId;
        }
    }
}

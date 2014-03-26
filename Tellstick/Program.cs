using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TelldusWrapper;

namespace TellstickConsole
{
    class Program
    {
        private static string _apiUrl { get { return ConfigurationManager.AppSettings["apiUrl"]; } }

        private static int _thresholdTime { get { return Convert.ToInt32(ConfigurationManager.AppSettings["thresholdTime"]); } }

        static DateTime _stopTime;

        static void Main(string[] args)
        {
            var wrapper = new TelldusNETWrapper();
            wrapper.tdRegisterDeviceEvent(new TelldusNETWrapper.EventCallbackFunction(DeviceEvent), null);
            while (true)
            {
                Thread.Sleep(Timeout.Infinite);
            }
        }

        static int DeviceEvent(int deviceId, int method, string data, int callbackId, object context)
        {

            Console.WriteLine(deviceId + " " + method + " " + data + " " + callbackId);
            var time = DateTime.Now - _stopTime;
            if (time > new TimeSpan(0, 0, _thresholdTime))
            {
                _stopTime = DateTime.Now;
                using (var wb = new WebClient())
                {
                    var url = _apiUrl + "?id=" + deviceId + "&method=" + method;
                    var request = WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentLength = 0; //got an error without this line
                    var response = request.GetResponse();
                }
            }

            return deviceId;
        }
    }
}

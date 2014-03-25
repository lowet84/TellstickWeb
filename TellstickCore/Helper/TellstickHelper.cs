using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelldusWrapper;
using System.IO;
using TellstickCore.Context;
using TellstickCore.Models;
using System.Diagnostics;

namespace TellstickCore.Helper
{
    public static class TellstickHelper
    {
        public static Options GetOptions() 
        {
            BuildDatabase.BuildDbIfNotExists();
            return new Options() { TellstickDevices = GetDevices(), Commands = Commands, Actions = TellstickInputActions };
        }

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

        private static SettingsContext _context;
        private static SettingsContext Settings
        {
            get
            {
                if (_context == null)
                {
                    BuildDatabase.BuildDbIfNotExists();
                    _context = new SettingsContext();
                }
                return _context;
            }
        }

        public static void InvokeCommand(int id)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.FileName = "cmd.exe";
            var command = GetCommand(id);
            p.StartInfo.Arguments = "/c " + command;
            p.Start();
        }

        public static List<Command> Commands
        {
            get
            {
                BuildDatabase.BuildDbIfNotExists();
                return Settings.Commands.ToList();
            }
        }

        public static List<TellstickInputAction> TellstickInputActions
        {
            get
            {
                BuildDatabase.BuildDbIfNotExists();
                return Settings.TellstickInputActions.ToList();
            }
        }

        private static string GetCommand(int id)
        {
            return Commands.First(d => d.Id == id).CommandText;
        }

        public static void AddCommand(string name, string command)
        {
            Settings.Commands.Add(new Command(name, command));
            Settings.SaveChanges();
        }

        public static void RemoveCommand(int id)
        {
            var command = Settings.Commands.FirstOrDefault(d => d.Id == id);
            if (command != null)
                Settings.Commands.Remove(command);
        }
    }
}
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
    public class TellstickHelper
    {
        private const bool Debug = true;

        private TellstickHelper() { }

        private static TellstickHelper _instance;
        public static TellstickHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    var newDb = BuildDatabase.BuildDbIfNotExists();
                    _instance = new TellstickHelper();
                    if (newDb && Debug)
                        _instance.PopulateDb();
                }
                return _instance;
            }
        }

        private Options _options;
        public Options Options
        {
            get
            {
                if (_options == null)
                    _options = new Options() { TellstickDevices = Devices, Commands = Commands, Actions = InputActions };
                return _options;
            }
        }

        public List<TellstickDevice> Devices
        {
            get
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
        }

        public void Turn(long id, bool state)
        {
            int? intId = Convert.ToInt32(id);
            Turn(intId, state);
        }

        public void Turn(int? id, bool state)
        {
            if (id == null)
                foreach (var item in Devices)
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

        private SettingsContext _context;
        private SettingsContext Settings
        {
            get
            {
                if (_context == null)
                {
                    _context = new SettingsContext();
                }
                return _context;
            }
        }

        public void InvokeCommand(int id)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.FileName = "cmd.exe";
            var command = GetCommand(id);
            p.StartInfo.Arguments = "/c " + command;
            p.Start();
        }

        public List<Command> Commands
        {
            get
            {
                return Settings.Commands.ToList();
            }
        }

        public void InvokeAction(int actionId, int actionType, int actionParameter)
        {
            InvokeAction((long)actionId, (long)actionType, (long)actionParameter);
        }

        public void InvokeAction(Int64 actionId, Int64 actionType, Int64 actionParameter)
        {
            switch (actionType)
            {
                case ActionType.TellstickAction:
                    switch (actionParameter)
                    {
                        case TellstickInput.ON: Turn(actionId, true); break;
                        case TellstickInput.OFF: Turn(actionId, false); break;
                    }
                    break;
                case ActionType.CMDAction: throw new NotImplementedException(); break;
                default: break;
            }
        }

        public List<InputAction> InputActions
        {
            get
            {
                return Settings.InputActions.ToList();
            }
        }

        private void PopulateDb()
        {
            AddInputAction("Fjärren 1", 9, 4, 0, -1);
            AddCommand("Starta om download", "winrs -r:download shutdown -r -t 0");
            AddInputAction("Starta om download", -2, Settings.Commands.First(d => d.CommandName == "Starta om download").Id, ActionType.CMDAction, -1);
            Settings.SaveChanges();
        }

        private string GetCommand(int id)
        {
            return Commands.First(d => d.Id == id).CommandText;
        }

        public void AddCommand(string name, string command)
        {
            Settings.Commands.Add(new Command() { CommandName = name, CommandText = command });
            Settings.SaveChanges();
        }

        public void RemoveCommand(int id)
        {
            var command = Settings.Commands.FirstOrDefault(d => d.Id == id);
            if (command != null)
                Settings.Commands.Remove(command);
            Settings.SaveChanges();
        }

        public void AddInputAction(string name, Int64 deviceId, Int64 actionId, Int64 actionType, Int64 actionParameter)
        {
            Settings.InputActions.Add(new InputAction(name, deviceId, actionId, actionType, actionParameter));
            Settings.SaveChanges();
        }

        public void RemoveInputAction(int id)
        {
            var inputAction = Settings.InputActions.FirstOrDefault(d => d.Id == id);
            if (inputAction != null)
                Settings.InputActions.Remove(inputAction);
            Settings.SaveChanges();
        }
    }
}
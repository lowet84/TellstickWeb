using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellstickCore.Models
{
    public class InputAction : DBObject
    {
        public InputAction() { }

        public InputAction(string name, Int64 deviceId, Int64 actionId, Int64 actionType, Int64 actionParameter)
        {
            Name = name;
            DeviceId = deviceId;
            ActionId = actionId;
            ActionType = actionType;
            ActionParameter = actionParameter;
        }
        public string Name { get; set; }

        // Enhetens id från TelldusCenter
        public Int64 DeviceId { get; set; }

        // Id för den handling som ska utföras i den tabell som motsvaras av ActionType
        public Int64 ActionId { get; set; }
        
        // Typ av handling: 0 för tellstick, 1 för CMD
        public Int64 ActionType { get; set; }

        // Parameter att skicka vidare, -1 om man vill vidarebefodra värdet från den som kallar på handlingen.
        public Int64 ActionParameter { get; set; }
    }

    public sealed class ActionType
    {
        public const Int64 TellstickAction = 0;

        public const Int64 CMDAction = 1;
    }

    public sealed class TellstickInput
    {
        public const Int64 ON = 1;

        public const Int64 OFF = 2;
    }
}

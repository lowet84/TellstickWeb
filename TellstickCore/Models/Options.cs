using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellstickCore.Models
{
    public class Options
    {
        public List<TellstickDevice> TellstickDevices { get; set; }
        public List<Command> Commands { get; set; }
        public List<InputAction> Actions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellstickCore.Models
{
    public class Command : DBObject
    {
        public Command() { }

        public Command(string commandName, string commandText)
        {
            CommandName = commandName;
            CommandText = commandText;
        }

        public string CommandName { get; set; }
        public string CommandText { get; set; }
    }
}

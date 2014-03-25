using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellstickCore.Models
{
    public class Command : DBObject
    {
        public string CommandName { get; set; }
        public string CommandText { get; set; }

        public Command(){}

        public Command(string name, string command)
        {
            CommandName = name;
            CommandText = command;
        }
    }
}

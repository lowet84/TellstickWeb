using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellstickCore.Models
{
    public class TellstickInputAction : DBObject
    {
        public string Name { get; set; }

        public Int64 DeviceId { get; set; }

        public Int64 ActionId { get; set; }
        
        public Int64 ActionType { get; set; }
    }
}

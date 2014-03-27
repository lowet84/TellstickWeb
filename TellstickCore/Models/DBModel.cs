using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TellstickCore.Models
{
    public abstract class DBObject
    {
        public Int64 Id { get; set; }
    }
}

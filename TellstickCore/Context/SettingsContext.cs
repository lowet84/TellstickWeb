using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellstickCore.Models;

namespace TellstickCore.Context
{
    public class SettingsContext: DbContext
    {
        public DbSet<Command> Commands { get; set; }
        public DbSet<InputAction> InputActions { get; set; }
    }
}

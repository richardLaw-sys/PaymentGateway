using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration
{
    public class ConfiguraitonSettings
    {
        public DatabaseOptions DatabaseSettings { get; set; }
    }
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration
{
    public static class Constant
    {
        public static IOptions<ConfiguraitonSettings> _appConfig { get; set; }//property dependency injection
    }
}

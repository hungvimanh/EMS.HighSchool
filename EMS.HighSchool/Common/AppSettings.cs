using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.HighSchool.Common
{
    public class AppSettings
    {
        public string JWTSecret { get; set; }
        public int JWTLifeTime { get; set; }
    }
}

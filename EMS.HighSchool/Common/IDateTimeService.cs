using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.HighSchool.Common
{
    public interface IDateTimeService
    {
        DateTime UtcNow { get; }
    }
    public class DateTimeService : IDateTimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

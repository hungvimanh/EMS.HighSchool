using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Controller.DTO
{
    public class EthnicDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class EthnicFilterDTO : FilterDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public EthnicFilterDTO() : base()
        {

        }
    }
}

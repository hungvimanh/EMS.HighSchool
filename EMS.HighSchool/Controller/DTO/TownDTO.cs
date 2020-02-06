using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Controller.DTO
{
    public class TownDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long DistrictId { get; set; }
    }

    public class TownFilterDTO : FilterDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long DistrictId { get; set; }
        public TownFilterDTO() : base()
        {

        }
    }
}

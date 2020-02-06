using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Controller.DTO
{
    public class HighSchoolDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; set; }
    }

    public class HighSchoolFilterDTO : FilterDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; set; }
        public HighSchoolFilterDTO() : base()
        {

        }
    }
}

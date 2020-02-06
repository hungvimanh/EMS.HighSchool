using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Controller.DTO
{
    public class SubjectGroupDTO : DataDTO
    {
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class SubjectGroupFilterDTO : FilterDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public SubjectGroupFilterDTO() : base()
        {

        }
    }
}

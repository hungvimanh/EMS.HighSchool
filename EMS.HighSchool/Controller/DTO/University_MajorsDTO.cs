using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using System;

namespace EMS.HighSchool.Controller.DTO
{
    public class University_MajorsDTO : DataDTO
    {
        public long Id { get; set; }
        public long UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public string UniversityAddress { get; set; }
        public long MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public string Year { get; set; }
    }

    public class University_MajorsFilterDTO : FilterDTO
    {
        public long Id { get; set; }
        public long? UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public long? MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public string Year { get; set; }

        public University_MajorsOrder OrderBy { get; set; }
        public University_MajorsFilterDTO() : base()
        {

        }
    }
}

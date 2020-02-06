using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Entities
{
    public class University_Majors : DataEntity
    {
        public long Id { get; set; }
        public long UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public string UniversityAddress { get; set; }
        public string UniversityWebsite { get; set; }
        public long MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public string Year { get; set; }
    }

    public class University_MajorsFilter : FilterEntity
    {
        public LongFilter Id { get; set; }
        public long? UniversityId { get; set; }
        public StringFilter UniversityCode { get; set; }
        public StringFilter UniversityName { get; set; }
        public long? MajorsId { get; set; }
        public StringFilter MajorsCode { get; set; }
        public StringFilter MajorsName { get; set; }
        public StringFilter Year { get; set; }
        public University_MajorsOrder OrderBy { get; set; }
        public University_MajorsFilter() : base()
        {

        }
    }

    public enum University_MajorsOrder
    {
        Id,
        UniversityCode,
        UniversityName,
        MajorsCode,
        MajorsName,
        Year
    }
}

using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using System;

namespace EMS.HighSchool.Controller.DTO
{
    public class University_Majors_SubjectGroupDTO : DataDTO
    {
        public long Id { get; set; }
        public long University_MajorsId { get; set; }
        public long UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string UniversityCode { get; set; }
        public long MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public long SubjectGroupId { get; set; }
        public string SubjectGroupCode { get; set; }
        public string SubjectGroupName { get; set; }
        public string Year { get; set; }
        public double? Benchmark { get; set; }
        public int? Quantity { get; set; }
        public string Note { get; set; }
    }

    public class University_Majors_SubjectGroupFilterDTO : FilterDTO
    {
        public long Id { get; set; }
        public long? University_MajorsId { get; set; }
        public string Year { get; set; }
        public long? UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string UniversityCode { get; set; }
        public long? MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public double? Benchmark { get; set; }
        public long? SubjectGroupId { get; set; }
        public string SubjectGroupCode { get; set; }
        public University_Majors_SubjectGroupOrder OrderBy { get; set; }
        public University_Majors_SubjectGroupFilterDTO(): base()
        {

        }
    }
}

using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Entities
{
    public class Aspiration : DataEntity
    {
        public long Id { get; set; }
        public long UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public string UniversityAddress { get; set; }
        public long MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public long SubjectGroupId { get; set; }
        public string SubjectGroupCode { get; set; }
        public string SubjectGroupName { get; set; }
        public long RegisterExamId { get; set; }
        public int Sequence { get; set; }
    }
}

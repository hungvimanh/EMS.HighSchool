using System;
using System.Collections.Generic;

namespace EMS.HighSchool.Repositories.Models
{
    public partial class AspirationDAO
    {
        public long Id { get; set; }
        public int Sequence { get; set; }
        public long UniversityId { get; set; }
        public long MajorsId { get; set; }
        public long SubjectGroupId { get; set; }
        public long RegisterExamId { get; set; }

        public virtual MajorsDAO Majors { get; set; }
        public virtual RegisterExamDAO RegisterExam { get; set; }
        public virtual SubjectGroupDAO SubjectGroup { get; set; }
        public virtual UniversityDAO University { get; set; }
    }
}

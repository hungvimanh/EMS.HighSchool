using System;
using System.Collections.Generic;

namespace EMS.HighSchool.Repositories.Models
{
    public partial class HighSchoolDAO
    {
        public HighSchoolDAO()
        {
            RegisterExams = new HashSet<RegisterExamDAO>();
            Students = new HashSet<StudentDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; set; }
        public string Address { get; set; }

        public virtual ProvinceDAO Province { get; set; }
        public virtual ICollection<RegisterExamDAO> RegisterExams { get; set; }
        public virtual ICollection<StudentDAO> Students { get; set; }
    }
}

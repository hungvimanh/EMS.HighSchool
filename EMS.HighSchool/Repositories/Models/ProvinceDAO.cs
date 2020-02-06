using System;
using System.Collections.Generic;

namespace EMS.HighSchool.Repositories.Models
{
    public partial class ProvinceDAO
    {
        public ProvinceDAO()
        {
            Districts = new HashSet<DistrictDAO>();
            HighSchools = new HashSet<HighSchoolDAO>();
            RegisterExams = new HashSet<RegisterExamDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DistrictDAO> Districts { get; set; }
        public virtual ICollection<HighSchoolDAO> HighSchools { get; set; }
        public virtual ICollection<RegisterExamDAO> RegisterExams { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace EMS.HighSchool.Repositories.Models
{
    public partial class SubjectGroupDAO
    {
        public SubjectGroupDAO()
        {
            Aspirations = new HashSet<AspirationDAO>();
            University_Majors_SubjectGroups = new HashSet<University_Majors_SubjectGroupDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspirationDAO> Aspirations { get; set; }
        public virtual ICollection<University_Majors_SubjectGroupDAO> University_Majors_SubjectGroups { get; set; }
    }
}

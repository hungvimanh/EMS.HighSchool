using System;
using System.Collections.Generic;

namespace EMS.HighSchool.Repositories.Models
{
    public partial class MajorsDAO
    {
        public MajorsDAO()
        {
            Aspirations = new HashSet<AspirationDAO>();
            University_Majors = new HashSet<University_MajorsDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspirationDAO> Aspirations { get; set; }
        public virtual ICollection<University_MajorsDAO> University_Majors { get; set; }
    }
}

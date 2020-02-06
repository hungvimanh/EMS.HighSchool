using System;
using System.Collections.Generic;

namespace EMS.HighSchool.Repositories.Models
{
    public partial class EthnicDAO
    {
        public EthnicDAO()
        {
            Students = new HashSet<StudentDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StudentDAO> Students { get; set; }
    }
}

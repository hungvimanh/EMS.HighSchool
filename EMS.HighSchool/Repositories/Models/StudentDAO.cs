using System;
using System.Collections.Generic;

namespace EMS.HighSchool.Repositories.Models
{
    public partial class StudentDAO
    {
        public StudentDAO()
        {
            RegisterExams = new HashSet<RegisterExamDAO>();
            Users = new HashSet<UserDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public long EthnicId { get; set; }
        public long? TownId { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Identify { get; set; }
        public long? HighSchoolId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double? Maths { get; set; }
        public double? Literature { get; set; }
        public double? Languages { get; set; }
        public double? Physics { get; set; }
        public double? Chemistry { get; set; }
        public double? Biology { get; set; }
        public double? History { get; set; }
        public double? Geography { get; set; }
        public double? CivicEducation { get; set; }
        public int Status { get; set; }
        public byte[] Image { get; set; }

        public virtual EthnicDAO Ethnic { get; set; }
        public virtual HighSchoolDAO HighSchool { get; set; }
        public virtual TownDAO Town { get; set; }
        public virtual ICollection<RegisterExamDAO> RegisterExams { get; set; }
        public virtual ICollection<UserDAO> Users { get; set; }
    }
}

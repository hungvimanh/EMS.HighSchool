using System;
using System.Collections.Generic;

namespace EMS.HighSchool.Repositories.Models
{
    public partial class TeacherDAO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Identify { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public byte[] Image { get; set; }
    }
}

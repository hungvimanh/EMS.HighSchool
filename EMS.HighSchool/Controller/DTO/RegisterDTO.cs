using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Controller.DTO
{
    public class RegisterDTO : DataDTO
    {
        public string Name { get; set; }
        public bool Gender { get; set; }
        public DateTime Dob { get; set; }
        public long EthnicId { get; set; }
        public string EthnicName { get; set; }
        public string PlaceOfBirth { get; set; }
        public long? HighSchoolId { get; set; }
        public string HighSchoolName { get; set; }
        public string Identify { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

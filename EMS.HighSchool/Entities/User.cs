using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Entities
{
    public class User : DataEntity
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public long? StudentId { get; set; }
        public bool IsAdmin { get; set; }
        public string Salt { get; set; }
        public string Jwt { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public bool FirstLogin { get; set; }
    }

    public class UserFilter
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
    }
}

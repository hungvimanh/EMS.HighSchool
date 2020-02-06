using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.HighSchool.Common
{
    public interface ICurrentContext : IServiceScoped
    {
        long UserId { get; set; }
        string Username { get; set; }
        long StudentId { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        bool Gender { get; set; }
        bool IsAdmin { get; set; }
    }
    public class CurrentContext : ICurrentContext
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public long StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public bool IsAdmin { get; set; }
    }
}

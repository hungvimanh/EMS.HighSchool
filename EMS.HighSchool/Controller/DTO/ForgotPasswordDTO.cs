using EMS.HighSchool.Common;

namespace EMS.HighSchool.Controller.DTO
{
    public class ForgotPasswordDTO : DataDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}

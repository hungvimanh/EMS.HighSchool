using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Controller.student
{
    public class ApproveDTO : DataDTO
    {
        public long StudentId { get; set; }
        public long Id { get; set; }
        public int Status { get; set; }
    }
}

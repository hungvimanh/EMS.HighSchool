using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Entities
{
    public class Ethnic : DataEntity
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class EthnicFilter : FilterEntity
    {
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public EthnicOrder OrderBy { get; set; }
        public EthnicFilter() : base()
        {

        }
    }

    public enum EthnicOrder
    {
        Id,
        Code,
        Name
    }
}

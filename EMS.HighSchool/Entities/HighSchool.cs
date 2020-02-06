using EMS.HighSchool.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace EMS.HighSchool.Entities
{
    public class HighSchoolBO : DataEntity
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long ProvinceId { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
    }

    public class HighSchoolBOFilter : FilterEntity
    {
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public long ProvinceId { get; set; }

        public HighSchoolOrder OrderBy { get; set; }
        public HighSchoolBOFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]

    public enum HighSchoolOrder
    {
        Id,
        Code,
        Name
    }
}

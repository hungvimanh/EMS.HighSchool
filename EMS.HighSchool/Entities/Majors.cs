using EMS.HighSchool.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace EMS.HighSchool.Entities
{
    public class Majors : DataEntity
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class MajorsFilter : FilterEntity
    {
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public MajorsOrder OrderBy { get; set; }
        public MajorsFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum MajorsOrder
    {
        Id,
        Code,
        Name
    }
}

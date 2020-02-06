using EMS.HighSchool.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace EMS.HighSchool.Entities
{
    public class Town : DataEntity
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long DistrictId { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
    }

    public class TownFilter : FilterEntity
    {
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public long DistrictId { get; set; }
        public StringFilter DistrictCode { get; set; }
        public StringFilter DistrictName { get; set; }
        public TownOrder OrderBy { get; set; }
        public TownFilter() : base()
        {
        }
    }
    [JsonConverter(typeof(StringEnumConverter))]

    public enum TownOrder
    {
        Id,
        Code,
        Name
    }
}

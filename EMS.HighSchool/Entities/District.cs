using EMS.HighSchool.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace EMS.HighSchool.Entities
{
    public class District : DataEntity
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public List<Town> Towns { get; set; }
    }

    public class DistrictFilter : FilterEntity
    {
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public long ProvinceId { get; set; }
        public StringFilter ProvinceCode { get; set; }
        public StringFilter ProvinceName { get; set; }
        public DistrictOrder OrderBy { get; set; }
        public DistrictFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]

    public enum DistrictOrder
    {
        Id,
        Code,
        Name
    }
}

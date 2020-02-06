using EMS.HighSchool.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace EMS.HighSchool.Entities
{
    public class Province : DataEntity      //Tỉnh/thành phố
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<District> Districts { get; set; }
        public List<HighSchoolBO> HighSchools { get; set; }
    }

    public class ProvinceFilter : FilterEntity
    {
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public ProvinceOrder OrderBy { get; set; }
        public ProvinceFilter() : base()
        {

        }
    }
    [JsonConverter(typeof(StringEnumConverter))]

    public enum ProvinceOrder
    {
        Id,
        Code,
        Name
    }
}

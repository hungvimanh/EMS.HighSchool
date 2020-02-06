using EMS.HighSchool.Common;
using System;

namespace EMS.HighSchool.Entities
{
    public class SubjectGroup : DataEntity
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class SubjectGroupFilter : FilterEntity
    {
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }

        public SubjectGroupOrder OrderBy { get; set; }
        public SubjectGroupFilter() : base()
        {

        }
    }

    public enum SubjectGroupOrder
    {
        Id,
        Code,
        Name
    }
}

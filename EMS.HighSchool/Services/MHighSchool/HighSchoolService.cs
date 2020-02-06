using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.HighSchool.Services.MHighSchool
{
    public interface IHighSchoolService : IServiceScoped
    {
        Task<HighSchoolBO> Get(long Id);
        Task<List<HighSchoolBO>> List(HighSchoolBOFilter highSchoolFilter);
    }
    public class HighSchoolService : IHighSchoolService
    {
        private readonly IUOW UOW;

        public HighSchoolService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<List<HighSchoolBO>> List(HighSchoolBOFilter highSchoolFilter)
        {
            return await UOW.HighSchoolRepository.List(highSchoolFilter);
        }

        public async Task<HighSchoolBO> Get(long Id)
        {
            HighSchoolBO HighSchool = await UOW.HighSchoolRepository.Get(Id);
            return HighSchool;
        }
    }
}

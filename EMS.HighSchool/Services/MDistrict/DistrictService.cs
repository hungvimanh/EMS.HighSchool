using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.HighSchool.Services.MDistrict
{
    public interface IDistrictService : IServiceScoped
    {
        Task<District> Get(long Id);
        Task<List<District>> List(DistrictFilter districtFilter);
    }
    public class DistrictService : IDistrictService
    {
        private readonly IUOW UOW;

        public DistrictService(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<List<District>> List(DistrictFilter districtFilter)
        {
            return await UOW.DistrictRepository.List(districtFilter);
        }

        public async Task<District> Get(long Id)
        {
            District District = await UOW.DistrictRepository.Get(Id);
            return District;
        }
    }
}

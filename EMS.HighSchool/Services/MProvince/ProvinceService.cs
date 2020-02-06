using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.HighSchool.Services.MProvince
{
    public interface IProvinceService : IServiceScoped
    {
        Task<Province> Get(long Id);
        Task<List<Province>> List(ProvinceFilter provinceFilter);
    }
    public class ProvinceService : IProvinceService
    {
        private readonly IUOW UOW;

        public ProvinceService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<List<Province>> List(ProvinceFilter provinceFilter)
        {
            return await UOW.ProvinceRepository.List(provinceFilter);
        }

        public async Task<Province> Get(long Id)
        {
            Province Province = await UOW.ProvinceRepository.Get(Id);
            return Province;
        }
    }
}

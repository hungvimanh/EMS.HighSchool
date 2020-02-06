using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.HighSchool.Services.MTown
{
    public interface ITownService : IServiceScoped
    {
        Task<Town> Get(long Id);
        Task<List<Town>> List(TownFilter townFilter);
    }
    public class TownService : ITownService
    {
        private readonly IUOW UOW;

        public TownService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<List<Town>> List(TownFilter townFilter)
        {
            return await UOW.TownRepository.List(townFilter);
        }

        public async Task<Town> Get(long Id)
        {
            Town Town = await UOW.TownRepository.Get(Id);
            return Town;
        }
    }
}

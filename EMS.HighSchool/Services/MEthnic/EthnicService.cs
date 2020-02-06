using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.HighSchool.Services.MEthnic
{
    public interface IEthnicService : IServiceScoped
    {
        Task<Ethnic> Get(long Id);
        Task<List<Ethnic>> List(EthnicFilter ethnicFilter);
    }
    public class EthnicService : IEthnicService
    {
        private IUOW UOW;
        public EthnicService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<Ethnic> Get(long Id)
        {
            var ethnic = await UOW.EthnicRepository.Get(Id);
            return ethnic;
        }

        public async Task<List<Ethnic>> List(EthnicFilter ethnicFilter)
        {
            return await UOW.EthnicRepository.List(ethnicFilter);
        }
    }
}

﻿using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.HighSchool.Services.MMajors
{
    public interface IMajorsService : IServiceScoped
    {
        Task<Majors> Create(Majors majors);
        Task<Majors> Get(long Id);
        Task<List<Majors>> List(MajorsFilter majorsFilter);
        Task<Majors> Update(Majors majors);
        Task<Majors> Delete(Majors majors);
    }
    public class MajorsService : IMajorsService
    {
        private readonly IUOW UOW;
        private readonly IMajorsValidator MajorsValidator;

        public MajorsService(IUOW UOW, IMajorsValidator MajorsValidator)
        {
            this.UOW = UOW;
            this.MajorsValidator = MajorsValidator;
        }

        public async Task<Majors> Create(Majors majors)
        {
            if (!await MajorsValidator.Create(majors))
                return majors;

            try
            {
                await UOW.Begin();
                await UOW.MajorsRepository.Create(majors);
                await UOW.Commit();
                return await Get(majors.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Majors> Delete(Majors majors)
        {
            if (!await MajorsValidator.Delete(majors))
                return majors;

            try
            {
                await UOW.Begin();
                await UOW.MajorsRepository.Delete(majors.Id);
                await UOW.Commit();
                return majors;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Majors> Get(long Id)
        {
            Majors majors = await UOW.MajorsRepository.Get(Id);
            return majors;
        }

        public async Task<List<Majors>> List(MajorsFilter majorsFilter)
        {
            return await UOW.MajorsRepository.List(majorsFilter);
        }

        public async Task<Majors> Update(Majors majors)
        {
            if (!await MajorsValidator.Update(majors))
                return majors;

            try
            {
                await UOW.Begin();
                await UOW.MajorsRepository.Update(majors);
                await UOW.Commit();
                return await Get(majors.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}

using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.HighSchool.Services.MUniversity
{
    public interface IUniversityService : IServiceScoped
    {
        Task<University> Create(University university);
        Task<University> Get(long Id);
        Task<List<University>> List(UniversityFilter universityFilter);
        Task<University> Update(University university);
        Task<University> Delete(University university);
    }
    public class UniversityService : IUniversityService
    {
        private readonly IUOW UOW;
        private readonly IUniversityValidator UniversityValidator;

        public UniversityService(
            IUOW UOW,
            IUniversityValidator UniversityValidator
            )
        {
            this.UOW = UOW;
            this.UniversityValidator = UniversityValidator;
        }

        public async Task<University> Create(University university)
        {
            if (!await UniversityValidator.Create(university))
                return university;

            try
            {
                await UOW.Begin();
                await UOW.UniversityRepository.Create(university);
                await UOW.Commit();
                return await Get(university.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<University> Delete(University university)
        {
            if (!await UniversityValidator.Delete(university))
                return university;

            try
            {
                await UOW.Begin();
                await UOW.UniversityRepository.Delete(university.Id);
                await UOW.Commit();
                return university;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<University> Get(long Id)
        {
            University university = await UOW.UniversityRepository.Get(Id);
            return university;
        }

        public async Task<List<University>> List(UniversityFilter universityFilter)
        {
            return await UOW.UniversityRepository.List(universityFilter);
        }

        public async Task<University> Update(University university)
        {
            if (!await UniversityValidator.Update(university))
                return university;

            try
            {
                await UOW.Begin();
                await UOW.UniversityRepository.Update(university);
                await UOW.Commit();
                return await Get(university.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.HighSchool.Common;
using EMS.HighSchool.Repositories.Models;
using Z.EntityFramework.Extensions;

namespace EMS.HighSchool.Repositories
{
    public interface IUOW : IServiceScoped
    {
        Task Begin();
        Task Commit();
        Task Rollback();
        IDistrictRepository DistrictRepository { get; }
        IEthnicRepository EthnicRepository { get; }
        IFormRepository FormRepository { get; }
        IHighSchoolRepository HighSchoolRepository { get; }
        IMajorsRepository MajorsRepository { get; }
        IProvinceRepository ProvinceRepository { get; }
        ITownRepository TownRepository { get; }
        ISubjectGroupRepository SubjectGroupRepository { get; }
        IStudentRepository StudentRepository { get; }
        IUniversity_MajorsRepository University_MajorsRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        IUniversity_Majors_SubjectGroupRepository University_Majors_SubjectGroupRepository { get; }
        IUserRepository UserRepository { get; }
    }
    public class UOW : IUOW
    { 
        private EMSContext context;
        public IDistrictRepository DistrictRepository { get; private set; }
        public IEthnicRepository EthnicRepository { get; private set; }
        public IFormRepository FormRepository { get; private set; }
        public IHighSchoolRepository HighSchoolRepository { get; private set; }
        public IMajorsRepository MajorsRepository { get; private set; }
        public IProvinceRepository ProvinceRepository { get; private set; }
        public ITownRepository TownRepository { get; private set; }
        public ISubjectGroupRepository SubjectGroupRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; }
        public IUniversity_MajorsRepository University_MajorsRepository { get; private set; }
        public IUniversity_Majors_SubjectGroupRepository University_Majors_SubjectGroupRepository { get; private set; }
        public IUniversityRepository UniversityRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }

        public UOW(EMSContext _context, ICurrentContext currentContext)
        {
            context = _context;
            DistrictRepository = new DistrictRepository(context);
            EthnicRepository = new EthnicRepository(context);
            FormRepository = new FormRepository(context, currentContext);
            HighSchoolRepository = new HighSchoolRepository(context);
            MajorsRepository = new MajorsRepository(context);
            ProvinceRepository = new ProvinceRepository(context);
            TownRepository = new TownRepository(context);
            SubjectGroupRepository = new SubjectGroupRepository(context);
            StudentRepository = new StudentRepository(context);
            University_MajorsRepository = new University_MajorsRepository(context);
            University_Majors_SubjectGroupRepository = new University_Majors_SubjectGroupRepository(context);
            UniversityRepository = new UniversityRepository(context);
            UserRepository = new UserRepository(context);
            EntityFrameworkManager.ContextFactory = DbContext => context;
        }

        public async Task Begin()
        {
            await context.Database.BeginTransactionAsync();
        }

        public Task Commit()
        {
            context.Database.CommitTransaction();
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            context.Database.RollbackTransaction();
            return Task.CompletedTask;
        }
    }
}

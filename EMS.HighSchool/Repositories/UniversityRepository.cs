using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.HighSchool.Repositories
{
    public interface IUniversityRepository
    {
        Task<bool> Create(University university);
        Task<List<University>> List(UniversityFilter universityFilter);
        Task<int> Count(UniversityFilter universityFilter);
        Task<University> Get(long Id);
        Task<bool> Update(University university);
        Task<bool> Delete(long Id);
    }
    public class UniversityRepository : IUniversityRepository
    {
        private readonly EMSContext context;
        public UniversityRepository(EMSContext _context)
        {
            context = _context;
        }

        private IQueryable<UniversityDAO> DynamicFilter(IQueryable<UniversityDAO> query, UniversityFilter universityFilter)
        {
            if (universityFilter == null)
                return query.Where(q => 1 == 0);

            if (universityFilter.Id != null)
                query = query.Where(q => q.Id, universityFilter.Id);
            if (universityFilter.Name != null)
                query = query.Where(q => q.Name, universityFilter.Name);
            if (universityFilter.Code != null)
                query = query.Where(q => q.Code, universityFilter.Code);
            return query;
        }
        private IQueryable<UniversityDAO> DynamicOrder(IQueryable<UniversityDAO> query, UniversityFilter universityFilter)
        {
            switch (universityFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (universityFilter.OrderBy)
                    {
                        case UniversityOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case UniversityOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (universityFilter.OrderBy)
                    {
                        case UniversityOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case UniversityOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(q => q.Id);
                    break;
            }
            query = query.Skip(universityFilter.Skip).Take(universityFilter.Take);
            return query;
        }
        private async Task<List<University>> DynamicSelect(IQueryable<UniversityDAO> query)
        {

            List<University> Universitys = await query.Select(q => new University()
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code,
                Address = q.Address,
                Website = q.Website
            }).ToListAsync();
            return Universitys;
        }

        public async Task<int> Count(UniversityFilter universityFilter)
        {
            IQueryable<UniversityDAO> universityDAOs = context.University;
            universityDAOs = DynamicFilter(universityDAOs, universityFilter);
            return await universityDAOs.CountAsync();
        }

        public async Task<List<University>> List(UniversityFilter universityFilter)
        {
            if (universityFilter == null) return new List<University>();
            IQueryable<UniversityDAO> universityDAOs = context.University;
            universityDAOs = DynamicFilter(universityDAOs, universityFilter);
            universityDAOs = DynamicOrder(universityDAOs, universityFilter);
            var universitys = await DynamicSelect(universityDAOs);
            return universitys;
        }

        public async Task<bool> Create(University university)
        {
            UniversityDAO universityDAO = new UniversityDAO
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address,
                Website = university.Website
            };

            context.University.Add(universityDAO);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(long Id)
        {
            await context.University.Where(m => m.Id == Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<University> Get(long Id)
        {
            University university = await context.University.Where(u => u.Id == Id).Include(u => u.University_Majors).Select(u => new University
            {
                Id = u.Id,
                Code = u.Code,
                Name = u.Name,
                Address = u.Address,
                Website = u.Website,
                University_Majors = u.University_Majors.Select(um => new University_Majors
                {
                    Id = um.Id,
                    MajorsId = um.MajorsId,
                    MajorsCode = um.Majors.Code,
                    MajorsName = um.Majors.Name,
                    UniversityId = um.UniversityId,
                    UniversityCode = um.University.Code,
                    UniversityName = um.University.Name
                    
                }).ToList()
            }).FirstOrDefaultAsync();

            return university;
        }

        public async Task<bool> Update(University university)
        {
            await context.University.Where(u => u.Id == university.Id).UpdateFromQueryAsync(u => new UniversityDAO
            {
                Code = university.Code,
                Name = university.Name,
                Address = university.Address,
                Website = university.Website
            });

            return true;
        }
    }
}

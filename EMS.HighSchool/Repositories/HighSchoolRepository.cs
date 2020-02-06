using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories.Models;
using EMS.HighSchool.Common;

namespace EMS.HighSchool.Repositories
{
    public interface IHighSchoolRepository
    {
        Task<bool> Create(HighSchoolBO highSchool);
        Task<int> Count(HighSchoolBOFilter highSchoolFilter);
        Task<List<HighSchoolBO>> List(HighSchoolBOFilter highSchoolFilter);
        Task<HighSchoolBO> Get(long Id);
        Task<bool> Update(HighSchoolBO highSchool);
        Task<bool> Delete(long Id);
    }
    public class HighSchoolRepository : IHighSchoolRepository
    {
        private readonly EMSContext context;
        public HighSchoolRepository(EMSContext _context)
        {
            context = _context;
        }

        private IQueryable<HighSchoolDAO> DynamicFilter(IQueryable<HighSchoolDAO> query, HighSchoolBOFilter highSchoolFilter)
        {
            if (highSchoolFilter == null)
                return query.Where(q => 1 == 0);
            query = query.Where(q => q.ProvinceId == highSchoolFilter.ProvinceId);

            if (highSchoolFilter.Id != null)
                query = query.Where(q => q.Id, highSchoolFilter.Id);
            if (highSchoolFilter.Name != null)
                query = query.Where(q => q.Name, highSchoolFilter.Name);
            if (highSchoolFilter.Code != null)
                query = query.Where(q => q.Code, highSchoolFilter.Code);
            return query;
        }
        private IQueryable<HighSchoolDAO> DynamicOrder(IQueryable<HighSchoolDAO> query, HighSchoolBOFilter highSchoolFilter)
        {
            switch (highSchoolFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (highSchoolFilter.OrderBy)
                    {
                        case HighSchoolOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case HighSchoolOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (highSchoolFilter.OrderBy)
                    {
                        case HighSchoolOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case HighSchoolOrder.Name:
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
            query = query.Skip(highSchoolFilter.Skip).Take(highSchoolFilter.Take);
            return query;
        }
        private async Task<List<HighSchoolBO>> DynamicSelect(IQueryable<HighSchoolDAO> query)
        {

            List<HighSchoolBO> highSchools = await query.Select(q => new HighSchoolBO()
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code,
                ProvinceId = q.ProvinceId,
                ProvinceCode = q.Province.Code,
                ProvinceName = q.Province.Name,
                Address = q.Address,
            }).ToListAsync();
            return highSchools;
        }

        public async Task<int> Count(HighSchoolBOFilter highSchoolFilter)
        {
            IQueryable<HighSchoolDAO> highSchoolDAOs = context.HighSchool;
            highSchoolDAOs = DynamicFilter(highSchoolDAOs, highSchoolFilter);
            return await highSchoolDAOs.CountAsync();
        }

        public async Task<bool> Create(HighSchoolBO highSchool)
        {
            HighSchoolDAO HighSchoolDAO = new HighSchoolDAO
            {
                Id = highSchool.Id,
                Code = highSchool.Code,
                Name = highSchool.Name,
                ProvinceId = highSchool.ProvinceId,
                Address = highSchool.Address,
            };

            context.HighSchool.Add(HighSchoolDAO);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(long Id)
        {
            await context.Student.Where(h => h.HighSchoolId == Id).DeleteFromQueryAsync();
            await context.RegisterExam.Where(h => h.RegisterPlaceOfExamId == Id).DeleteFromQueryAsync();
            await context.HighSchool.Where(h => h.Id == Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<HighSchoolBO> Get(long Id)
        {
            HighSchoolBO HighSchool = await context.HighSchool.Where(h => h.Id == Id).Select(h => new HighSchoolBO
            {
                Id = h.Id,
                Code = h.Code,
                Name = h.Name,
                ProvinceId = h.ProvinceId,
                ProvinceCode = h.Province.Code,
                ProvinceName = h.Province.Name,
                Address = h.Address,
            }).FirstOrDefaultAsync();

            return HighSchool;
        }

        public async Task<List<HighSchoolBO>> List(HighSchoolBOFilter highSchoolFilter)
        {
            if (highSchoolFilter == null) return new List<HighSchoolBO>();
            IQueryable<HighSchoolDAO> highSchoolDAOs = context.HighSchool;
            highSchoolDAOs = DynamicFilter(highSchoolDAOs, highSchoolFilter);
            highSchoolDAOs = DynamicOrder(highSchoolDAOs, highSchoolFilter);
            var highSchools = await DynamicSelect(highSchoolDAOs);
            return highSchools;
        }

        public async Task<bool> Update(HighSchoolBO highSchool)
        {
            await context.HighSchool.Where(t => t.Id == highSchool.Id).UpdateFromQueryAsync(t => new HighSchoolDAO
            {
                Code = highSchool.Code,
                Name = highSchool.Name,
                ProvinceId = highSchool.ProvinceId,
                Address = highSchool.Address,
            });

            return true;
        }
    }
}

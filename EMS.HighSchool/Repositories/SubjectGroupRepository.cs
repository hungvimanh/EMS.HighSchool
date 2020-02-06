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
    public interface ISubjectGroupRepository
    {
        Task<bool> Create(SubjectGroup subjectGroup);
        Task<SubjectGroup> Get(long Id);
        Task<int> Count(SubjectGroupFilter subjectGroupFilter);
        Task<List<SubjectGroup>> List(SubjectGroupFilter subjectGroupFilter);
        Task<bool> Update(SubjectGroup subjectGroup);
        Task<bool> Delete(long Id);
    }
    public class SubjectGroupRepository : ISubjectGroupRepository
    {
        private readonly EMSContext context;
        public SubjectGroupRepository(EMSContext _context)
        {
            context = _context;
        }

        private IQueryable<SubjectGroupDAO> DynamicFilter(IQueryable<SubjectGroupDAO> query, SubjectGroupFilter subjectGroupFilter)
        {
            if (subjectGroupFilter == null)
                return query.Where(q => 1 == 0);

            if (subjectGroupFilter.Id != null)
                query = query.Where(q => q.Id, subjectGroupFilter.Id);
            if (subjectGroupFilter.Name != null)
                query = query.Where(q => q.Name, subjectGroupFilter.Name);
            if (subjectGroupFilter.Code != null)
                query = query.Where(q => q.Code, subjectGroupFilter.Code);
            return query;
        }
        private IQueryable<SubjectGroupDAO> DynamicOrder(IQueryable<SubjectGroupDAO> query, SubjectGroupFilter subjectGroupFilter)
        {
            switch (subjectGroupFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (subjectGroupFilter.OrderBy)
                    {
                        case SubjectGroupOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SubjectGroupOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (subjectGroupFilter.OrderBy)
                    {
                        case SubjectGroupOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SubjectGroupOrder.Name:
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
            query = query.Skip(subjectGroupFilter.Skip).Take(subjectGroupFilter.Take);
            return query;
        }
        private async Task<List<SubjectGroup>> DynamicSelect(IQueryable<SubjectGroupDAO> query)
        {

            List<SubjectGroup> subjectGroups = await query.Select(q => new SubjectGroup()
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code
            }).ToListAsync();
            return subjectGroups;
        }

        public async Task<int> Count(SubjectGroupFilter subjectGroupFilter)
        {
            IQueryable<SubjectGroupDAO> subjectGroupDAOs = context.SubjectGroup;
            subjectGroupDAOs = DynamicFilter(subjectGroupDAOs, subjectGroupFilter);
            return await subjectGroupDAOs.CountAsync();
        }

        public async Task<List<SubjectGroup>> List(SubjectGroupFilter subjectGroupFilter)
        {
            if (subjectGroupFilter == null) return new List<SubjectGroup>();
            IQueryable<SubjectGroupDAO> subjectGroupDAOs = context.SubjectGroup;
            subjectGroupDAOs = DynamicFilter(subjectGroupDAOs, subjectGroupFilter);
            subjectGroupDAOs = DynamicOrder(subjectGroupDAOs, subjectGroupFilter);
            var subjectGroups = await DynamicSelect(subjectGroupDAOs);
            return subjectGroups;
        }

        public async Task<bool> Create(SubjectGroup subjectGroup)
        {
            SubjectGroupDAO subjectGroupDAO = new SubjectGroupDAO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name
            };

            context.SubjectGroup.Add(subjectGroupDAO);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(long Id)
        {
            await context.University_Majors_SubjectGroup.Where(t => t.SubjectGroupId == Id).DeleteFromQueryAsync();
            await context.SubjectGroup.Where(d => d.Id == Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<SubjectGroup> Get(long Id)
        {
            SubjectGroup subjectGroup = await context.SubjectGroup.Where(d => d.Id == Id).Select(d => new SubjectGroup
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name
            }).FirstOrDefaultAsync();

            return subjectGroup;
        }

        public async Task<bool> Update(SubjectGroup subjectGroup)
        {
            await context.SubjectGroup.Where(t => t.Id == subjectGroup.Id).UpdateFromQueryAsync(t => new SubjectGroupDAO
            {
                Code = subjectGroup.Code,
                Name = subjectGroup.Name
            });

            return true;
        }
    }
}

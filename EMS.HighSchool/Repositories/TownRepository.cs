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
    public interface ITownRepository
    {
        Task<bool> Create(Town town);
        Task<int> Count(TownFilter townFilter);
        Task<List<Town>> List(TownFilter townFilter);
        Task<Town> Get(long Id);
        Task<bool> Update(Town town);
        Task<bool> Delete(long Id);
    }
    public class TownRepository : ITownRepository
    {
        private readonly EMSContext context;
        public TownRepository(EMSContext _context)
        {
            context = _context;
        }

        private IQueryable<TownDAO> DynamicFilter(IQueryable<TownDAO> query, TownFilter townFilter)
        {
            if (townFilter == null)
                return query.Where(q => 1 == 0);
            query = query.Where(q => q.DistrictId == townFilter.DistrictId);

            if (townFilter.Id != null)
                query = query.Where(q => q.Id, townFilter.Id);
            if (townFilter.Name != null)
                query = query.Where(q => q.Name, townFilter.Name);
            if (townFilter.Code != null)
                query = query.Where(q => q.Code, townFilter.Code);
            return query;
        }
        private IQueryable<TownDAO> DynamicOrder(IQueryable<TownDAO> query, TownFilter townFilter)
        {
            switch (townFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (townFilter.OrderBy)
                    {
                        case TownOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case TownOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (townFilter.OrderBy)
                    {
                        case TownOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case TownOrder.Name:
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
            query = query.Skip(townFilter.Skip).Take(townFilter.Take);
            return query;
        }
        private async Task<List<Town>> DynamicSelect(IQueryable<TownDAO> query)
        {

            List<Town> towns = await query.Select(q => new Town()
            {
                Id = q.Id,
                DistrictId = q.DistrictId,
                DistrictCode = q.District.Code,
                DistrictName = q.District.Name,
                Name = q.Name,
                Code = q.Code
            }).ToListAsync();
            return towns;
        }

        public async Task<int> Count(TownFilter townFilter)
        {
            IQueryable<TownDAO> townDAOs = context.Town;
            townDAOs = DynamicFilter(townDAOs, townFilter);
            return await townDAOs.CountAsync();
        }

        public async Task<List<Town>> List(TownFilter townFilter)
        {
            if (townFilter == null) return new List<Town>();
            IQueryable<TownDAO> townDAOs = context.Town;
            townDAOs = DynamicFilter(townDAOs, townFilter);
            townDAOs = DynamicOrder(townDAOs, townFilter);
            var towns = await DynamicSelect(townDAOs);
            return towns;
        }

        public async Task<bool> Create(Town town)
        {
            TownDAO TownDAO = new TownDAO
            {
                Id = town.Id,
                Code = town.Code,
                Name = town.Name,
                DistrictId = town.DistrictId,
            };

            context.Town.Add(TownDAO);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(long Id)
        {
            await context.Student.Where(p => p.TownId == Id).DeleteFromQueryAsync();
            await context.Town.Where(t => t.Id == Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Town> Get(long Id)
        {
            Town Town = await context.Town.Where(t => t.Id == Id).Select(t => new Town
            {
                Id = t.Id,
                Code = t.Code,
                Name = t.Name,
                DistrictId = t.DistrictId,
                DistrictCode = t.District.Code,
                DistrictName = t.District.Name
            }).FirstOrDefaultAsync();

            return Town;
        }

        public async Task<bool> Update(Town town)
        {
            await context.Town.Where(t => t.Id == town.Id).UpdateFromQueryAsync(t => new TownDAO
            {
                Code = town.Code,
                Name = town.Name,
                DistrictId = town.DistrictId
            });

            return true;
        }
    }
}

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
    public interface IProvinceRepository
    {
        Task<bool> Create(Province province);
        Task<Province> Get(long Id);
        Task<int> Count(ProvinceFilter provinceFilter);
        Task<List<Province>> List(ProvinceFilter provinceFilter);
        Task<bool> Update(Province province);
        Task<bool> Delete(long Id);
    }
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly EMSContext context;
        public ProvinceRepository(EMSContext _context)
        {
            context = _context;
        }

        private IQueryable<ProvinceDAO> DynamicFilter(IQueryable<ProvinceDAO> query, ProvinceFilter provinceFilter)
        {
            if (provinceFilter == null)
                return query.Where(q => 1 == 0);

            if (provinceFilter.Id != null)
                query = query.Where(q => q.Id, provinceFilter.Id);
            if (provinceFilter.Name != null)
                query = query.Where(q => q.Name, provinceFilter.Name);
            if (provinceFilter.Code != null)
                query = query.Where(q => q.Code, provinceFilter.Code);
            return query;
        }
        private IQueryable<ProvinceDAO> DynamicOrder(IQueryable<ProvinceDAO> query, ProvinceFilter provinceFilter)
        {
            switch (provinceFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (provinceFilter.OrderBy)
                    {
                        case ProvinceOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ProvinceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (provinceFilter.OrderBy)
                    {
                        case ProvinceOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ProvinceOrder.Name:
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
            query = query.Skip(provinceFilter.Skip).Take(provinceFilter.Take);
            return query;
        }
        private async Task<List<Province>> DynamicSelect(IQueryable<ProvinceDAO> query)
        {

            List<Province> provinces = await query.Select(q => new Province()
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code,
            }).ToListAsync();
            return provinces;
        }

        public async Task<int> Count(ProvinceFilter provinceFilter)
        {
            IQueryable<ProvinceDAO> provinceDAOs = context.Province;
            provinceDAOs = DynamicFilter(provinceDAOs, provinceFilter);
            return await provinceDAOs.CountAsync();
        }

        public async Task<List<Province>> List(ProvinceFilter provinceFilter)
        {
            if (provinceFilter == null) return new List<Province>();
            IQueryable<ProvinceDAO> provinceDAOs = context.Province;
            provinceDAOs = DynamicFilter(provinceDAOs, provinceFilter);
            provinceDAOs = DynamicOrder(provinceDAOs, provinceFilter);
            var provinces = await DynamicSelect(provinceDAOs);
            return provinces;
        }

        public async Task<bool> Create(Province province)
        {
            ProvinceDAO provinceDAO = new ProvinceDAO
            {
                Id = province.Id,
                Code = province.Code,
                Name = province.Name
            };

            context.Province.Add(provinceDAO);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(long Id)
        {
            await context.Town.Where(t => t.District.ProvinceId == Id).DeleteFromQueryAsync();
            await context.District.Where(d => d.ProvinceId == Id).DeleteFromQueryAsync();
            await context.Province.Where(p => p.Id == Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Province> Get(long Id)
        {
            Province province = await context.Province.Where(p => p.Id == Id).Select(p => new Province
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
            }).FirstOrDefaultAsync();

            return province;
        }

        public async Task<bool> Update(Province province)
        {
            await context.Province.Where(p => p.Id == province.Id).UpdateFromQueryAsync(p => new ProvinceDAO
            {
                Code = province.Code,
                Name = province.Name,
            });

            return true;
        }
    }
}

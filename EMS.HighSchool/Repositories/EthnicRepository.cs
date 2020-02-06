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
    public interface IEthnicRepository
    {
        Task<Ethnic> Get(long Id);
        Task<List<Ethnic>> List(EthnicFilter ethnicFilter);
        Task<int> Count(EthnicFilter ethnicFilter);
    }
    public class EthnicRepository : IEthnicRepository
    {
        private readonly EMSContext context;
        public EthnicRepository(EMSContext _context)
        {
            context = _context;
        }

        private IQueryable<EthnicDAO> DynamicFilter(IQueryable<EthnicDAO> query, EthnicFilter ethnicFilter)
        {
            if (ethnicFilter == null)
                return query.Where(q => 1 == 0);
            
            if (ethnicFilter.Id != null)
                query = query.Where(q => q.Id, ethnicFilter.Id);
            if (ethnicFilter.Code != null)
                query = query.Where(q => q.Code, ethnicFilter.Code);
            if (ethnicFilter.Name != null)
                query = query.Where(q => q.Name, ethnicFilter.Name);
            
            return query;
        }
        private IQueryable<EthnicDAO> DynamicOrder(IQueryable<EthnicDAO> query, EthnicFilter ethnicFilter)
        {
            switch (ethnicFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (ethnicFilter.OrderBy)
                    {
                        case EthnicOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case EthnicOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (ethnicFilter.OrderBy)
                    {
                        case EthnicOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case EthnicOrder.Name:
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
            query = query.Skip(ethnicFilter.Skip).Take(ethnicFilter.Take);
            return query;
        }
        private async Task<List<Ethnic>> DynamicSelect(IQueryable<EthnicDAO> query)
        {

            List<Ethnic> ethnics = await query.Select(q => new Ethnic()
            {
                Id = q.Id,
                Code = q.Code,
                Name = q.Name,
            }).ToListAsync();
            return ethnics;
        }

        public async Task<int> Count(EthnicFilter ethnicFilter)
        {
            IQueryable<EthnicDAO> ethnicDAOs = context.Ethnic;
            ethnicDAOs = DynamicFilter(ethnicDAOs, ethnicFilter);
            return await ethnicDAOs.CountAsync();
        }

        public async Task<List<Ethnic>> List(EthnicFilter ethnicFilter)
        {
            if (ethnicFilter == null) return new List<Ethnic>();
            IQueryable<EthnicDAO> ethnicDAOs = context.Ethnic;
            ethnicDAOs = DynamicFilter(ethnicDAOs, ethnicFilter);
            ethnicDAOs = DynamicOrder(ethnicDAOs, ethnicFilter);
            var ethnics = await DynamicSelect(ethnicDAOs);
            return ethnics;
        }

        public async Task<Ethnic> Get(long Id)
        {
            Ethnic Ethnic = await context.Ethnic.Where(d => d.Id == Id).Select(d => new Ethnic
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name,
            }).FirstOrDefaultAsync();

            return Ethnic;
        }
    }
}

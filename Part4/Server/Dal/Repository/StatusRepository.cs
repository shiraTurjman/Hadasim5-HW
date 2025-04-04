using Dal.Entity;
using Dal.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Entities;

namespace Dal.Repository
{
    public class StatusRepository : IStatusRepository
    {

        private readonly IDbContextFactory<ServerDbContext> _factory;
        public StatusRepository(IDbContextFactory<ServerDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<StatusEntity>> GetAllStatusAsync()
        {
            using var context = _factory.CreateDbContext();
            var list = await context.Status.ToListAsync();
            if (list.Count > 0)
                return list;
            else
                throw new Exception("No Status exist");
        }

        public async Task AddStatusAsync(StatusEntity status)
        {
            using var context = _factory.CreateDbContext();
            await context.Status.AddAsync(status);
            await context.SaveChangesAsync();
            //throw new NotImplementedException();
        }
    }
}

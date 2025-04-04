using Dal.Entities;
using Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IStatusRepository
    {
        Task<List<StatusEntity>> GetAllStatusAsync();

        Task AddStatusAsync(StatusEntity status);
    }
}

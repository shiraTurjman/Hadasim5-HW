using Dal.Entities;
using Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IStatusService
    {
        Task<List<StatusEntity>> GetAllStatusAsync();

        Task AddStatusAsync(StatusEntity status);
    }
}

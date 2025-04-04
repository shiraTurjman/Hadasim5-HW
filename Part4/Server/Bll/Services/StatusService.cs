using Bll.Interfaces;
using Dal.Entities;
using Dal.Entity;
using Dal.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Services
{
    public class StatusService : IStatusService

    {
        private readonly IStatusRepository _statusRepository;
        public StatusService(IStatusRepository statusRepository) {
        _statusRepository = statusRepository;
        }

        public async Task<List<StatusEntity>> GetAllStatusAsync()
        {
           return await _statusRepository.GetAllStatusAsync();

        }

        public async Task AddStatusAsync(StatusEntity status)
        { 
            await _statusRepository.AddStatusAsync(status);
        }
    }
}

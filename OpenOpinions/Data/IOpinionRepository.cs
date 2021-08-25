using OpenOpinions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OpenOpinions.Data
{
    public interface IOpinionRepository
    {
        public Task<IEnumerable<Opinion>> GetAllOpinions();

        public Task<Opinion> GetOpinionById(int id);

        public Task CreateOpinion(Opinion newOpinion);

        public Task DeleteOpinion(Opinion deleteOpinion);
    }
}

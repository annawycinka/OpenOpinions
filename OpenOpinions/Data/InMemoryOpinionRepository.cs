using OpenOpinions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OpenOpinions.Data
{
    public class InMemoryOpinionRepository : IOpinionRepository
    {
        private readonly List<Opinion> _opinionList;
        

        public InMemoryOpinionRepository()
        {
            this._opinionList = new List<Opinion>();
        }
        
        public Task CreateOpinion(Opinion newOpinion)
        {
            int nextId;

            if (_opinionList.Count == 0)
            {
                nextId = 1;
            }
            else
            {
                nextId = _opinionList.Max(x => x.Id) + 1;
            }

            newOpinion.Id = nextId;
            if (newOpinion == null)
            {
                throw new ArgumentNullException(nameof(newOpinion));
            }
            _opinionList.Add(newOpinion);
           return Task.CompletedTask;
        }

        public Task DeleteOpinion(Opinion deleteOpinion)
        {
            _opinionList.Remove(deleteOpinion);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Opinion>> GetAllOpinions()
        {
            return Task.FromResult<IEnumerable<Opinion>>(_opinionList); 
        }

        public Task<Opinion> GetOpinionById(int id)
        {
            var opinion = _opinionList.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(opinion);
        }

        public Task Update(Opinion updateOpinion)
        {
            throw new NotImplementedException();
        }
    }
}

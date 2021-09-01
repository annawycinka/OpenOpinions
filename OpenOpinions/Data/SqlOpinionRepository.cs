using OpenOpinions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OpenOpinions.Data
{
    public class SqlOpinionRepository : IOpinionRepository
    {
        private readonly OpinionContext _context;

        public SqlOpinionRepository(OpinionContext context)
        {
            this._context = context;
        }

        public async Task CreateOpinion(Opinion newOpinion)
        {
            if (newOpinion == null)
            {
                throw new ArgumentNullException(nameof(newOpinion));
            }
            _context.Opinions.Add(newOpinion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOpinion(Opinion deleteOpinion)
        {
            if (deleteOpinion == null)
            {
                throw new ArgumentNullException();
            }

            _context.Opinions.Remove(deleteOpinion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Opinion>> GetAllOpinions()
        {
           return await _context.Opinions.ToListAsync();
        }

        public async Task<Opinion> GetOpinionById(int id)
        {
            return await _context.Opinions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

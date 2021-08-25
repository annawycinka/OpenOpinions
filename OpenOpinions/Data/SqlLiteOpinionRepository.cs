using OpenOpinions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOpinions.Data
{
    public class SqlLiteOpinionRepository : IOpinionRepository
    {
        private readonly OpinionContext _context;

        public SqlLiteOpinionRepository(OpinionContext context)
        {
            this._context = context;
        }

        public void CreateOpinion(Opinion newOpinion)
        {
            if (newOpinion == null)
            {
                throw new ArgumentNullException(nameof(newOpinion));
            }
            _context.Opinions.Add(newOpinion);
        }

        public IEnumerable<Opinion> GetAllOpinions()
        {
           return _context.Opinions.ToList();
        }

        public Opinion GetOpinionById(int id)
        {
            return _context.Opinions.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }
    }
}

using OpenOpinions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace OpenOpinions.Data
{
    public class DbLiteOpinionRepository : IOpinionRepository
    {
        public LiteDatabase Db { get; }
        public ILiteCollection<Opinion> OpinionsCollection { get; }
        public DbLiteOpinionRepository(DbLiteOpinionContext db)
        {
            this.Db = db.Db;
            OpinionsCollection = Db.GetCollection<Opinion>();
                
        }

        public Task CreateOpinion(Opinion newOpinion)
        {
            if (newOpinion == null)
            {
                throw new ArgumentNullException(nameof(newOpinion));
            }

            OpinionsCollection.Insert(newOpinion);
            return Task.CompletedTask;
            

        }

        public Task DeleteOpinion(Opinion deleteOpinion)
        {
            if (deleteOpinion == null)
            {
                throw new ArgumentNullException();
            }

            OpinionsCollection.Delete(new BsonValue(deleteOpinion.Id));

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Opinion>> GetAllOpinions()
        {
            var result = OpinionsCollection.FindAll();
            return Task.FromResult(result);
        }

        public Task<Opinion> GetOpinionById(int id)
        {
            var result = OpinionsCollection.FindById(new BsonValue(id));
            return Task.FromResult(result);
        }

        public Task Update(Opinion updateOpinion)
        {
            throw new NotImplementedException();
        }
        
    }
}

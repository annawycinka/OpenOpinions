using OpenOpinions.Models;
using System.Collections.Generic;

namespace OpenOpinions.Data
{
    public interface IOpinionRepository
    {
        public IEnumerable<Opinion> GetAllOpinions();

        public Opinion GetOpinionById(int id);

        public void CreateOpinion(Opinion newOpinion);

        public bool SaveChanges();


    }
}

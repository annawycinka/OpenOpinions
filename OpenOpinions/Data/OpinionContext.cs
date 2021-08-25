using Microsoft.EntityFrameworkCore;
using OpenOpinions.Models;

namespace OpenOpinions.Data
{
    public class OpinionContext :DbContext
    {
        public OpinionContext(DbContextOptions<OpinionContext>options):base(options){}


        public DbSet<Opinion> Opinions { get; set; }
    }
}

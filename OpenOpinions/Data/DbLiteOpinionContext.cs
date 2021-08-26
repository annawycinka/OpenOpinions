using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using OpenOpinions.Models;

namespace OpenOpinions.Data
{
    public class DbLiteOpinionContext
    {
        public readonly LiteDatabase Db = new LiteDatabase(@"C:\Users\annaw\source\repos\OpenOpinions\OpenOpinions\DbLiteOpinion.db");

        
    }
}

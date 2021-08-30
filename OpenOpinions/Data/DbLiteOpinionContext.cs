using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.Extensions.Hosting;
using OpenOpinions.Models;

namespace OpenOpinions.Data
{
    public class DbLiteOpinionContext
    {
        private readonly IHostEnvironment _hostEnvironment;

        public DbLiteOpinionContext(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment; 
        }

        public LiteDatabase Db => new LiteDatabase(@$"{_hostEnvironment.ContentRootPath}\DbLiteOpinion.db");
    }
}

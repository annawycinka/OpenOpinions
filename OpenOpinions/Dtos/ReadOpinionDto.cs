using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOpinions.Dtos
{
    public class ReadOpinionDto
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}

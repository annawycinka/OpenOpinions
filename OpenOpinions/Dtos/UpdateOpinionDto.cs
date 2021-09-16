using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenOpinions.Dtos
{
    public class UpdateOpinionDto
    {
        
            [Required]
            public string Text { get; set; }
        
    }
}

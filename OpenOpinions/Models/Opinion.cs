using System.ComponentModel.DataAnnotations;

namespace OpenOpinions.Models
{
    public class Opinion
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Text { get; set; }

       

    }
}

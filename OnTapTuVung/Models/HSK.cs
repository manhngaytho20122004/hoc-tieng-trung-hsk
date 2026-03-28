using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTapTuVung.Models
{
    [Table("HSKs")]
    public class HSK
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHSK { get; set; }

        [Required]
        public int LoaiHSK { get; set; } 

        // Quan hệ 1 HSK - nhiều Vocabulary
        public virtual ICollection<Vocabulary> Vocabularies { get; set; } = new List<Vocabulary>();
    }
}
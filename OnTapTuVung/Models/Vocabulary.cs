using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTapTuVung.Models
{
    [Table("Vocabularies")]
    public class Vocabulary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdHSK { get; set; }

        [ForeignKey("IdHSK")]
        public HSK? HSK { get; set; }

        [Required]
        public string TuVung { get; set; } = string.Empty;

        public string Pinyn { get; set; } = string.Empty;

        public string TuLoai { get; set; } = string.Empty;

        public string HanViet { get; set; } = string.Empty;

        [Required]
        public string Nghia { get; set; } = string.Empty;
    }
}
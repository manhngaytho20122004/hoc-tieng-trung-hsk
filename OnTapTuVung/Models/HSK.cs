using System.ComponentModel.DataAnnotations;

namespace OnTapTuVung.Models
{
    public class HSK
    {
        [Key]
        public int IdHSK { get; set; }
        public int LoaiHSK { get; set; }
        public List<Vocabulary> Vocabularies { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace OnTapTuVung.Models
{
    public class Vocabulary
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("HSK")]
        public int IdHSK { get; set; }
        public string TuVung { get; set; }
        public string Pinyn { get; set; }
        public string TuLoai { get; set; }
        public string HanViet { get; set; }
        public string Nghia { get; set; }

        public HSK HSK { get; set; }

    }
}

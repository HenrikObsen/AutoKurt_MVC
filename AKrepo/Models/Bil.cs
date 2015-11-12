using System.ComponentModel.DataAnnotations;

namespace AKrepo
{
    public class Bil
    {
        public int ID { get; set; }
        [Required]
        public int ProducentID { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Beskrivelse { get; set; }
        [Required]
        public decimal Pris { get; set; }
    }
}


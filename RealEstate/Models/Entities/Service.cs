using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace RealEstate.Models.Entities
{
    [Table("Services")]
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Název služby musí být vyplněn")]
        [MaxLength(512, ErrorMessage = "Název nesmí být delší než 512 znaků")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Služba musí mít titulní popisek")]
        [MaxLength(1024, ErrorMessage = "Titulní popisek nesmí být delší než 1024 znaků")]
        public string SmallDescription { get; set; }

        [Required(ErrorMessage = "Služba musí mít popis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Služba musí mít minimální cenu")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Služba musí mít titulní obrázek")]
        public string? Photo { get; set; }
        public int Requests { get; set; }

        [ForeignKey("ServiceId")]
        public List<Photo> Photos { get; set; }
    }
}

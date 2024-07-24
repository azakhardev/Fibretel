using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public string Path { get; set; }

        public string Text { get; set; }
    }
}

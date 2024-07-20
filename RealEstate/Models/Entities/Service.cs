using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace RealEstate.Models.Entities
{
    [Table("Services")]
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string? Photo { get; set; }
        public int Requests { get; set; }

        [ForeignKey("ServiceId")]
        public List<Photo> Photos { get; set; }
    }
}

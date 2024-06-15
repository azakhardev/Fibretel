using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.Entities
{
    [Table("Offers")]
    public class Offer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Rent { get; set; }

        public int Price { get; set; }        

        public string Address { get; set; }

        public float Area { get; set; }

        public string SmallDescription { get; set; }
        public string? Description { get; set; }

        public DateTime PostDate { get; set; } 

        public string? PropertyType { get; set; }        
        
        public string? AreaType { get; set; }                

        public string? PreviewPhoto { get; set; }
    }
}

using Fibretel.Models.Entities;

namespace Fibretel.Models.Dto
{
    public class ServiceDto
    {
        public Service Service { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}

using Fibretel.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Fibretel.Models.Dto
{
    public class ServiceDto
    {
        public Service Service { get; set; } = new Service();
        
        public IFormFile? Photo { get; set; }

        public List<PhotoDto> Photos { get; set; } = new List<PhotoDto>();
    }
}

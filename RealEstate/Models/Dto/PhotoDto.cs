using System.ComponentModel.DataAnnotations;

namespace Fibretel.Models.Dto
{
    public class PhotoDto
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; }

        public string? Text { get; set; }
    }
}

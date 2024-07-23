using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.Entities
{
    [Table("Requests")]
    public class Request
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public string Service { get; set; }   
        public DateTime SentAt { get; set; }
        public bool Answered { get; set; }
    }
}

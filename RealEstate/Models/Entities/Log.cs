using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.Entities
{
    [Table("Logs")]
    public class Log
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}

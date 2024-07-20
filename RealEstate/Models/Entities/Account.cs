using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.Entities
{
    [Table("Accounts")]
    public class Account
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool Superior { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
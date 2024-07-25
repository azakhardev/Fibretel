using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fibretel.Models.Entities
{
    [Table("Accounts")]
    public class Account
    {
        public int Id { get; set; }

        [Length(4, 256, ErrorMessage = "Username musí být 4-256 znaků dlouhý")]
        [Required(ErrorMessage = "Username nesmí být prázdný")]
        public string Username { get; set; }

        public string Password { get; set; }
        public bool Superior { get; set; }
        public bool Disabled { get; set; }

        [Required(ErrorMessage = "Email nesmí být prázdný")]
        [MaxLength(256, ErrorMessage = "Email nesmí mít přes 256 znaků")]
        public string? Email { get; set; }

        public string? Degree { get; set; }

        [Length(2, 256, ErrorMessage = "Jméno musí být 2-256 znaků dlouhé")]
        [Required(ErrorMessage = "Jméno nesmí být prázdné")]
        public string? Name { get; set; }

        [Length(2, 256, ErrorMessage = "Příjmení musí být 2-256 znaků dlouhé")]
        [Required(ErrorMessage = "Příjmení nesmí být prázdné")]
        public string? Surname { get; set; }
    }
}
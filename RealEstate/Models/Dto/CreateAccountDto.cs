using Fibretel.Models.Entities;

namespace Fibretel.Models.Dto
{
    public class CreateAccountDto
    {
        public Account Account { get; set; }
        public string Password2 { get; set; }
    }
}

using RealEstate.Models.Entities;

namespace RealEstate.Models.Dto
{
    public class CreateAccountDto
    {
        public Account Account { get; set; }
        public string Password2 { get; set; }
    }
}

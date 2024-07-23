using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace RealEstate.Models.Dto
{
    public class Answer
    {
        public int RequestId { get; set; }
        public string CustomerEmail { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}

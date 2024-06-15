using RealEstate.Models.Entities;

namespace RealEstate.Models.Dto
{
    public class OfferDto
    {
        public OfferDto Offer { get; set; }

        public List<Photo> OfferPhotos { get; set; }
    }
}
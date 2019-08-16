namespace ExploreCalifornia.DTOs
{
    public class TourSearchRequestDto
    {
        public decimal MinPrice { get; set; } = decimal.MinValue;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
    }
}
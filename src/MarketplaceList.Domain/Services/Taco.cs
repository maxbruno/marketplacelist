namespace MarketplaceList.Domain.Services
{
    public class Taco
    {
        public int id { get; set; }
        public string description { get; set; }
        public int base_qty { get; set; }
        public string base_unit { get; set; }
        public int category_id { get; set; }
    }
}
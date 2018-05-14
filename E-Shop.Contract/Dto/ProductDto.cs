namespace E_Shop.Contract.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public SpecificationDto Specification { get; set; }

        public ImageDto Image { get; set; }
        
    }
}

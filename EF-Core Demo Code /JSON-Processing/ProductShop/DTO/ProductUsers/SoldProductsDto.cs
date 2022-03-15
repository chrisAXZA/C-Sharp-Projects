namespace ProductShop.DTO.ProductUsers
{
    using System;

    public class SoldProductsDto
    {
        public SoldProductsDto()
        {
        }

        public int Count { get; set; }

        public ProductDetailsDto[] Products { get; set; }
    }
}

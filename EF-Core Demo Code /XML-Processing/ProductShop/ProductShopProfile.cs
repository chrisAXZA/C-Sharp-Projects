namespace ProductShop
{
    using System.Linq;
    
    using AutoMapper;

    using ProductShop.Models;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<ImportUserDto, User>();

            this.CreateMap<ImportProductDto, Product>();

            this.CreateMap<ImportCategoryDto, Category>();

            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();

            this.CreateMap<Product, ExportProductDto>();

            this.CreateMap<User, ExportUserDto>()
                .ForMember(p => p.SoldProducts,
                src => src.MapFrom(us => us.ProductsSold));

            this.CreateMap<Product, ExportSoldProductDto>();

            this.CreateMap<Category, ExportCategoryByProductDto>()
                .ForMember(p => p.Name,
                src => src.MapFrom(ca => ca.Name))
                .ForMember(p => p.Count,
                src => src.MapFrom(ca => ca.CategoryProducts.Count()))
                .ForMember(p => p.AveragePrice,
                src => src.MapFrom(ca => ca.CategoryProducts.Average(cp => cp.Product.Price)))
                .ForMember(p => p.TotalRevenue,
                src => src.MapFrom(ca => ca.CategoryProducts.Sum(cp => cp.Product.Price)));

            //.Select(c => new ExportCategoryByProductDto
            //{
            //    Name = c.Name,
            //    Count = c.CategoryProducts.Count(),
            //    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
            //    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
            //})

            this.CreateMap<User, ExportNestedUserDto>()
                .ForMember(p => p.SoldProducts,
                src => src.MapFrom(us => us.ProductsSold.Where(ps => ps.Buyer != null)));

            this.CreateMap<ExportNestedUserDto, ExportUserCountDto>()
                .ForMember(p => p.Users,
                src => src.MapFrom(nested => nested));

            this.CreateMap<Product, ExportNestedProductDetailDto>();

            this.CreateMap<User, ExportNestedProductsDto>()
                .ForMember(p => p.Count,
                src => src.MapFrom(us => us.ProductsSold.Where(ps => ps.Buyer != null).Count()))
                .ForMember(p => p.Products,
                src => src.MapFrom(us => us.ProductsSold.Where(ps => ps.Buyer != null)));
        }
    }
}

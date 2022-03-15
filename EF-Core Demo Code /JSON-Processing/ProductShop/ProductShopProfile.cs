namespace ProductShop
{
    using System.Linq;
    
    using AutoMapper;

    using ProductShop.Models;
    using ProductShop.DTO.User;
    using ProductShop.DTO.Product;
    using ProductShop.DTO.Category;
    using ProductShop.DTO.ProductUsers;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<Product, ListProductsInRangeDTO>()
                .ForMember(p => p.Name,
                src => src.MapFrom(pro => pro.Name))
                .ForMember(p => p.Price,
                src => src.MapFrom(pro => pro.Price))
                .ForMember(p => p.SellerName,
                src => src.MapFrom(pro => pro.Seller.FirstName + ' ' + pro.Seller.LastName));

            this.CreateMap<Product, UserSoldProductDTO>()
                .ForMember(p => p.Name,
                src => src.MapFrom(pro => pro.Name))
                .ForMember(p => p.Price,
                src => src.MapFrom(pro => pro.Price))
                .ForMember(p => p.BuyerFirstName,
                src => src.MapFrom(p => p.Seller.FirstName))
                .ForMember(p => p.BuyerLastName,
                src => src.MapFrom(pro => pro.Seller.LastName));

            this.CreateMap<User, UserWithSoldProductsDTO>()
                .ForMember(p => p.SoldProducts,
                //src => src.MapFrom(u => u.ProductsSold));
                src => src.MapFrom(u => u.ProductsSold.Where(x => x.Buyer != null)));

            this.CreateMap<Category, CategoriesByProductsDTO>()
                .ForMember(p => p.Category,
                src => src.MapFrom(p => p.Name))
                .ForMember(p => p.ProductsCount,
                src => src.MapFrom(p => p.CategoryProducts.Count))
                .ForMember(p => p.AveragePrice,
                src => src.MapFrom(p => p.CategoryProducts.Select(pr => pr.Product.Price).Average().ToString("F2")))
                .ForMember(p => p.TotalRevenue,
                src => src.MapFrom(p => p.CategoryProducts.Sum(pr => pr.Product.Price).ToString("F2")));

            // DTO mapper

            this.CreateMap<UserImportDTO, User>();

            // GetUsersAndProducts
            this.CreateMap<Product, ProductDetailsDto>();

            this.CreateMap<User, SoldProductsDto>()
                .ForMember(p => p.Count,
                src => src.MapFrom(u => u.ProductsSold.Count(ps => ps.Buyer != null)))
                .ForMember(p => p.Products,
                src => src.MapFrom(u => u.ProductsSold.Where(ps => ps.Buyer != null)));

            this.CreateMap<User, UserDetailsDto>()
                .ForMember(p => p.SoldProducts,
                src => src.MapFrom(u => u)); // uses above mapping <User, SoldProductsDto>

            this.CreateMap<UserDetailsDto[], UserInfoDto>()
                .ForMember(p => p.UsersCount,
                //src => src.MapFrom(uDto => uDto.Count()))
                src => src.MapFrom(uDto => uDto.Length))
                .ForMember(p => p.Users,
                src => src.MapFrom(uDto => uDto));
        }
    }
}

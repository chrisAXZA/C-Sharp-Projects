namespace ProductShop
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Globalization;
    using System.Collections.Generic;

    using AutoMapper;
    using Newtonsoft.Json;
    //using System.Text.Json;
    using Newtonsoft.Json.Serialization;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;

    using ProductShop.Data;
    using ProductShop.Models;
    using ProductShop.DTO.User;
    using ProductShop.DTO.Product;
    using ProductShop.DTO.Category;
    using ProductShop.DTO.ProductUsers;

    public class StartUp
    {
        private static string DirectoryResultsPath = "../../../Datasets/Results";

        //private static readonly IMapper mapper;

        public static void Main(string[] args)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            //mapper = new Mapper(cfg => new ConfigurationValidator())
            //Mapper mapper;
            //Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            using (ProductShopContext dbContext = new ProductShopContext())
            {

                //using ProductShopContext dbContext = new ProductShopContext();
                //ResetDatabase(dbContext);

                InitialzeMapper();

                string userJson = File.ReadAllText("../../../Datasets/users.json");
                string productJson = File.ReadAllText("../../../Datasets/products.json");
                string categoryJson = File.ReadAllText("../../../Datasets/categories.json");
                string catProJson = File.ReadAllText("../../../Datasets/categories-products.json");

                //var prod = new ListProductsInRangeDTO();

                //string result = ImportUsers(dbContext, userJson);
                //string result = ImportProducts(dbContext, productJson);
                //string result = ImportCategories(dbContext, categoryJson);
                //string result = ImportCategoryProducts(dbContext, catProJson);
                //string result = GetProductsInRange(dbContext);
                //string result = GetSoldProducts(dbContext);
                //string result = GetCategoriesByProductsCount(dbContext);
                //string result = GetUsersWithProducts(dbContext);

                //string result = ImportUsers2(dbContext, userJson);
                //string result = ImportProducts2(dbContext, productJson);
                //string result = ImportCategories2(dbContext, categoryJson);
                //string result = ImportCategoryProducts2(dbContext, catProJson);

                //string result = GetProductsInRange2(dbContext);
                string result = GetUsersWithProducts(dbContext);

                Console.WriteLine(result);
            }
        }

        public static string ImportUsers2(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);
            //    .Select(u => new User
            //{
            //        FirstName = u.FirstName,
            //        LastName = u.LastName,
            //        Age = u.Age,

            //});

            context.Users.AddRange(users);

            int count = context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts2(ProductShopContext context, string inputJson)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

            context.Products.AddRange(products);

            context.SaveChanges();


            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories2(ProductShopContext context, string inputJson)
        {
            Category[] categories2 = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(cat => cat.Name != null)
                .ToArray();


            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson);

            List<Category> validCategories = new List<Category>();

            foreach (var cat in categories)
            {
                if (cat.Name == null)
                {
                    continue;
                }

                validCategories.Add(cat);
            }

            context.Categories.AddRange(categories2);

            context.SaveChanges();

            return $"Successfully imported {categories2.Length}";
        }

        public static string ImportCategoryProducts2(ProductShopContext context, string inputJson)
        {
            var categoriesProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoriesProducts);

            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Length}";
        }

        public static string GetProductsInRange2(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = $"{p.Seller.FirstName} {p.Seller.LastName}",
                    //seller = p.Seller.FirstName + " " + p.Seller.LastName,
                })
                .ToArray();

            var json = JsonConvert.SerializeObject(products, Formatting.Indented);

            // Alternative
            //var resolver = new DefaultContractResolver
            //{
            //    NamingStrategy = new CamelCaseNamingStrategy(),
            //};

            //var jsonResolver = JsonConvert.SerializeObject(products, new JsonSerializerSettings
            //{
            //    ContractResolver = resolver,
            //    Formatting = Formatting.Indented,
            //});

            return json;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            //var finalUserDto = context.Users
            //    .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
            //    .ProjectTo<UserInfoDto>();

            var usersDto = context.Users
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                .ProjectTo<UserDetailsDto>()
                .OrderByDescending(u => u.SoldProducts.Count)
                .ToArray();

            var finalUserDto = Mapper.Map<UserInfoDto>(usersDto);

            var finalOutput = new UserInfoDto
            {
                UsersCount = usersDto.Length,
                Users = usersDto,
            };

            var jsonSettings2 = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy= new CamelCaseNamingStrategy(),
                },
            };

            var json2 = JsonConvert.SerializeObject(finalOutput, jsonSettings2);

            var usersProducts = context.Users
                .Include(u => u.ProductsSold)
                .ToList()
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                //.OrderByDescending(u => u.ProductsSold.Count(ps => ps.Buyer != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold.Count(ps => ps.Buyer != null),
                        //count = u.ProductsSold.Count(),
                        //products = u.ProductsSold
                        products = u.ProductsSold.Where(ps => ps.Buyer != null)
                        .Select(ps => new
                        {
                            name = ps.Name,
                            price = ps.Price,
                        }).ToArray(),
                    }
                })
                .OrderByDescending(u => u.soldProducts.products.Count())
                //.OrderByDescending(u => u.soldProducts.count)
                //.ToList();
                .ToArray();

            var finalUsers = new
            {
                usersCount = usersProducts.Count(),
                users = usersProducts,
            };

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };

            var json = JsonConvert.SerializeObject(finalUsers, jsonSettings);

            return json;

            //var jsonSettings = new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented,
            //    ContractResolver = contractResolver,
            //    Culture = CultureInfo.InvariantCulture,
            //    //Culture = CultureInfo.GetCultureInfo("bg-BG"),
            //    DateFormatString = "yyyy-MM-dd",
            //    NullValueHandling = NullValueHandling.Include, // Indicates parse behaviour for null values
            //    // Converter, activate custom logic for parsing of collection: JSON-array to Dictionary/HashSet
            //};

            //Console.WriteLine(JsonConvert.SerializeObject(forecast, jsonSettings));
        }

        // 02 Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            //var jsonSettings = new JsonSerializerSettings
            //{
            //    //Formatting = Formatting.Indented,
            //    //ContractResolver = contractResolver,
            //    Culture = CultureInfo.InvariantCulture,
            //    //Culture = CultureInfo.GetCultureInfo("bg-BG"),
            //    //DateFormatString = "yyyy-MM-dd",
            //    //NullValueHandling = NullValueHandling.Include, // Indicates parse behaviour for null values
            //    // Converter, activate custom logic for parsing of collection: JSON-array to Dictionary/HashSet
            //};

            //var options = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true,
            //};
            //var weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonString, options);

            // Deserialize with DTO to object, one additional step
            UserImportDTO[] usersDTO = JsonConvert.DeserializeObject<UserImportDTO[]>(inputJson);

            User[] usersDto = usersDTO.Select(udto => Mapper.Map<User>(udto)).ToArray();

            //var users= JsonSerializer.Deserialize<User>(inputJson);
            User[] users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);

            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        // 03 Import Products
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            // Array of products can be used, since JSON objects are contained in array
            // structure because no prop conflict when parsing to C# class
            // (Parsing to C# without cfg) 
            // JSON object can also be contained in object, then DTO mapping has to be employed
            // Example: {"entities" : [{}, {}, {}, ...]}
            // ProductImportDTO -> class prop Product[] Entities {get; set;}

            Product[] products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);

            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        // 04 Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            Category[] categories = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(cat => cat.Name != null)
                .ToArray();

            //foreach (var category in categories)
            //{
            //    if (category.Name == null)
            //    {
            //        continue;
            //    }

            //    context.Categories.Add(category);
            //}

            //JsonSerializerSettings settings = new JsonSerializerSettings()
            //{
            //    NullValueHandling = NullValueHandling.Ignore
            //    // only works when given prop of object is null, but not when whole
            //    // object is null
            //};

            //Category[] categories = JsonConvert.DeserializeObject<Category[]>(inputJson, settings);

            context.Categories.AddRange(categories);

            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
            //return $"Successfully imported {categories.Count(cat => cat.Name != null)}";
        }

        // 05 Import CategoryProducts
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            CategoryProduct[] categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);

            //context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }

        // 06 Export Products in Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            // prop name of anonymous object must the same as key of JSON object !!!

            var products = context.Products
                //.AsEnumerable() // not necessary
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                // Standard Variant
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    //price = p.Price.ToString("F2"), // Error, Price must be Decimal not string format
                    seller = p.Seller.FirstName + ' ' + p.Seller.LastName,
                    //seller = (p.Seller.FirstName + ' ') ?? "" + p.Seller.LastName ?? ""
                })
                // Variant 2 DTO
                //.Select(p => new ListProductsInRangeDTO
                //{
                //    Name = p.Name,
                //    Price = p.Price.ToString("F2"),
                //    SellerName = p.Seller.FirstName + ' ' + p.Seller.LastName
                //})
                // Variant 3 DTO + AutoMapper
                //.ProjectTo<ListProductsInRangeDTO>() // with AutoMapper
                .ToArray();

            //Console.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            var jsonFile = JsonConvert.SerializeObject(products, Formatting.Indented);
            // Formatting.None => Minified

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "/products-in-range-DTOMap.json", jsonFile);

            return jsonFile;
        }

        // 07 Export Successfully Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            //var users = context.Users
            //    .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
            //    .OrderByDescending(u => u.ProductsSold.Count)
            //    .Select(u => new
            //    {
            //        firstName = u.FirstName,
            //        lastName = u.LastName,
            //        age = u.Age,
            //        soldProducts = u.ProductsSold.Select(ps => new { name = ps.Name, price = ps.Price})
            //    })
            //    .ToList();

            var users = context.Users
                  .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                  .OrderBy(u => u.LastName)
                  .ThenBy(u => u.FirstName)
                  .Select(u => new
                  {
                      firstName = u.FirstName,
                      lastName = u.LastName,
                      soldProducts = u.ProductsSold
                      .Where(p => p.Buyer != null)
                      .Select(ps => new
                      {
                          name = ps.Name,
                          price = ps.Price,
                          buyerFirstName = ps.Buyer.FirstName,
                          buyerLastName = ps.Buyer.LastName,
                      })
                      .ToArray() // important JSON array of objects !!!
                  })
                  //.ProjectTo<UserWithSoldProductsDTO>()
                  .ToArray();

            // DTO + AutoMapper is best practise, returns concrete type UserWithSoldProductsDTO[]
            // instead of only var

            var jsonFile = JsonConvert.SerializeObject(users, Formatting.Indented);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "/users-and-products.json", jsonFile);

            return jsonFile;
        }

        // 08 Export Categories by Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
              .OrderByDescending(p => p.CategoryProducts.Count)
              .Select(p => new
              {
                  //category = p.CategoryProducts.Select(cp => cp.Product.Name),
                  // returns collection of all prodcut names for given category
                  //category = p.CategoryProducts.Select(cp => cp.Category.Name).First(),
                  category = p.Name,
                  productsCount = p.CategoryProducts.Count,
                  //Counts the number of occurences of given category
                  averagePrice = p.CategoryProducts.Average(cp => cp.Product.Price).ToString("F2"),
                  totalRevenue = p.CategoryProducts.Sum(cp => cp.Product.Price).ToString("F2"),
              })
              //.ProjectTo<CategoriesByProductsDTO>()
              .ToArray();

            var jsonFile = JsonConvert.SerializeObject(categories, Formatting.Indented);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "/categories-by-products.json", jsonFile);

            return jsonFile;
        }

        // 09 Export Users and Products
        //public static string GetUsersWithProducts(ProductShopContext context)
        //{
            //var userProducts2 = context.Users
            //    //.Where(p => p.ProductsSold.Count > 0)
            //    .Where(p => p.ProductsSold.Any(ps => ps.Buyer != null))
            //.OrderByDescending(p => p.ProductsSold.Count)
            //.Select(u => new
            //{
            //    usersCount = u.ProductsSold.Count > 0,
            //    users = new
            //    {
            //        firstName = u.FirstName,
            //        lastName = u.LastName,
            //        age = u.Age,
            //        soldProducts = new
            //        {
            //            count = u.ProductsSold.Count,
            //            products = u.ProductsSold.Select(sp => new { name = sp.Name, price = sp.Price }).ToArray()
            //        }
            //    }.ToArray()
            //});
            //.Select(u => new
            // {
            //     usersCount = u.ProductsSold.Count(ps => ps.Buyer != null),
            //     users = new
            //     {
            //         lastName = u.LastName,
            //         age = u.Age,
            //         soldProducts = new
            //         {
            //             count = u.ProductsSold.Count(p => p.Buyer != null),
            //             products = u.ProductsSold.Where(sp => sp.Buyer != null).Select(sp => new
            //             {
            //                 name = sp.Name,
            //                 price = sp.Price
            //             }).ToArray(),
            //         }
            //     }
            // })
            //.OrderByDescending(u => u.users.soldProducts.count);
            //.ToArray();

            //var userProducts = context.Users
            //   .Where(p => p.ProductsSold.Any(ps => ps.Buyer != null))
            //   .OrderByDescending(p => p.ProductsSold.Count)
            //   .Select(u => new
            //   {
            //       firstName = u.FirstName,
            //       lastName = u.LastName,
            //       age = u.Age,
            //       soldProducts = new
            //       {
            //           count = u.ProductsSold.Count(p => p.Buyer != null),
            //           products = u.ProductsSold.Where(sp => sp.Buyer != null)
            //               .Select(sp => new
            //               {
            //                   name = sp.Name,
            //                   price = sp.Price
            //               }).ToArray(),
            //       }
            //   })
            //   //.ToArray()
            //   //.OrderByDescending(p => p.soldProducts.count)
            //   .ToArray();

            //var result = new
        //    {
        //        usersCount = userProducts.Length,
        //        users = userProducts
        //    };

        //    var jsonSettings = new JsonSerializerSettings
        //    {
        //        Formatting = Formatting.Indented,
        //        NullValueHandling = NullValueHandling.Ignore
        //    };

        //    var jsonFile = JsonConvert.SerializeObject(result, jsonSettings);

        //    //EnsureDirectoryExists(DirectoryResultsPath);

        //    //File.WriteAllText(DirectoryResultsPath + "/users-and-products.json", jsonFile);

        //    return jsonFile;
        //}

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void InitialzeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());
        }

        private static void ResetDatabase(ProductShopContext dbContext)
        {
            dbContext.Database.EnsureDeleted();

            Console.WriteLine("Database was successfully deleted!");

            dbContext.Database.EnsureCreated();

            Console.WriteLine("Database was successfully created!");

            // will create new empty Database
        }
    }
}
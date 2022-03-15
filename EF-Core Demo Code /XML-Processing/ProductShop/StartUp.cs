namespace ProductShop
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;

    using ProductShop.Data;
    using ProductShop.Models;
    using ProductShop.XmlHelper;
    using ProductShop.Dtos.Import;
    using ProductShop.Dtos.Export;

    public class StartUp
    {
        private static string DirectoryResultsPath = "../../../Datasets/Results";

        public static void Main(string[] args)
        {
            string categoriesXML = File.ReadAllText("../../../Datasets/categories.xml");
            string categoriesProductsXML = File.ReadAllText("../../../Datasets/categories-products.xml");
            string productsXML = File.ReadAllText("../../../Datasets/products.xml");
            string usersXML = File.ReadAllText("../../../Datasets/users.xml");

            using var dbContext = new ProductShopContext();

            //ResetDatabase(dbContext);

            InitializeMapper();

            //string result = ImportUsers(dbContext, "users.xml");
            //string result = ImportProducts(dbContext, productsXML);
            //string result = ImportCategories(dbContext, categoriesXML);
            //string result = ImportCategoryProducts(dbContext, categoriesProductsXML);
            //string result = ImportUsers(dbContext, usersXML);
            //string result = GetProductsInRange(dbContext);
            //string result = GetSoldProducts(dbContext);
            //string result = GetCategoriesByProductsCount(dbContext);
            string result = GetUsersWithProducts(dbContext);

            Console.WriteLine(result);
        }

        // 01 Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportUserDto[]), new XmlRootAttribute("Users"));

            //var users = (ImportUserDto[])serializer.Deserialize(File.OpenRead($"../../../Datasets/{inputXml}"));

            //var users = (ImportUserDto[])serializer.Deserialize(File.OpenRead(inputXml));
            // Error, will not work as with StringReader !!!
            using StringReader reader = new StringReader(inputXml);

            var users2 = (ImportUserDto[])serializer.Deserialize(reader);
            //var users2 = (ImportUserDto[])serializer.Deserialize(new StringReader(inputXml));

            //context.Users.AddRange(users2);

            //context.SaveChanges();

            const string rootElement = "Users";

            var usersDto = XmlConverter.Deserializer<ImportUserDto>(inputXml, rootElement);

            //List<User> usersList = new List<User>();

            //foreach (var userDto in usersDto)
            //{
            //    User currentUser = new User
            //    {
            //        Age = userDto.Age,
            //        FirstName = userDto.FirstName,
            //        LastName = userDto.LastName,
            //    };

            //    //currentUser.Age = userDto.Age;
            //    //currentUser.FirstName = userDto.FirstName;
            //    //currentUser.LastName = userDto.LastName;

            //    usersList.Add(currentUser);

            //    // Or, for debugging purpose, to see which data causes exception
            //    context.Users.Add(currentUser);
            //    context.SaveChanges();
            //    // to see which user causes expection and adapt validation accordingly

            //}

            var users = usersDto
                .Select(u => new User
                {
                    Age = u.Age,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                })
                .ToArray();

            context.Users.AddRange(users);

            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        // 02 Import Products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            //XmlSerializer serializer = new XmlSerializer
            //    (typeof(ImportProductDto[]), new XmlRootAttribute("Products"));

            //var productsDto = (ImportProductDto[])serializer.Deserialize(new StreamReader(inputXml));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();
            // or
            //var mapper = new Mapper(config); // does not work, old way

            //Product[] products = productsDto.Select(pDto => mapper.Map<Product>(pDto)).ToArray();

            //Product[] products = productsDto.Select(pDto => Mapper.Map<Product>(pDto)).ToArray();
            //Product[] products = productsDto.ProjectTo<Product>().ToArray();
            // ProjectTo only works on Database Context

            const string rootElement = "Products";

            var productsDto = XmlConverter.Deserializer<ImportProductDto>(inputXml, rootElement);

            //var productsCheck = new List<Product>();

            //foreach (var pDto in productsDto)
            //{
            //    if (context.Users.Any(u => u.Id == pDto.BuyerId && u.Id == pDto.SellerId))
            //    {
            //        //var currentProduct = mapper.Map<Product>(pDto);

            //        var currentProduct = new Product()
            //        {
            //            Name = pDto.Name,
            //            Price = pDto.Price,
            //            BuyerId = pDto.BuyerId,
            //            SellerId = pDto.SellerId,
            //        };

            //        productsCheck.Add(currentProduct);
            //    }
            //}

            //var products = productsDto.Select(pDto => new Product
            //{
            //    Name = pDto.Name,
            //    Price = pDto.Price,
            //    BuyerId = pDto.BuyerId,
            //    SellerId = pDto.SellerId,
            //})
            //.ToArray();

            Product[] products = productsDto.Select(pDto => mapper.Map<Product>(pDto)).ToArray();

            context.Products.AddRange(products);
            //context.Products.AddRange(productsCheck);

            context.SaveChanges();

            return $"Successfully imported {products.Length}";
            //return $"Successfully imported {productsCheck.Count}";
        }

        // 03 Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            //XmlSerializer serializer = new XmlSerializer
            //    (typeof(ImportCategoryDto[]), new XmlRootAttribute("Categories"));

            //var categoriesDto = (ImportCategoryDto[])serializer.Deserialize(new StreamReader(inputXml));

            const string rootElement = "Categories";

            var categoriesDto = XmlConverter.Deserializer<ImportCategoryDto>(inputXml, rootElement);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>());

            var mapper = config.CreateMapper();

            var categories = categoriesDto
                .Where(c => c.Name != null)
                //.Select(cDto => mapper.Map<Category>(cDto))
                .Select(c => new Category
                {
                    Name = c.Name,
                })
                .ToArray();

            context.Categories.AddRange(categories);

            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        // 04 Import CategoryProduct
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            //XmlSerializer serializer = new XmlSerializer
            //    (typeof(ImportCategoryProductDto[]), new XmlRootAttribute("CategoryProducts"));

            //var categoriesProductsDto = (ImportCategoryProductDto[])serializer.Deserialize(new StreamReader(inputXml));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>());

            var mapper = config.CreateMapper();
            //var mapper = new Mapper(config);

            const string rootElement = "CategoryProducts";

            var categoriesProductsDto = XmlConverter.Deserializer<ImportCategoryProductDto>(inputXml, rootElement);

            var categoriesProducts = categoriesProductsDto
                .Where(cp => context.Categories.Any(c => c.Id == cp.CategoryId) && context.Products.Any(p => p.Id == cp.ProductId))
                //.Select(cp => mapper.Map<CategoryProduct>(cp))
                .Select(cp => new CategoryProduct
                {
                    CategoryId = cp.CategoryId,
                    ProductId = cp.ProductId,
                })
                .ToArray();

            // Alternative, Memory intensive but less queries on DB
            // best used with small size DB

            //var productIds = context.Products.Select(p => p.Id).ToArray();
            //var categoryIds = context.Categories.Select(c => c.Id).ToArray();

            //foreach (var cp in categoriesProductsDto)
            //{
            //    bool idsValid = categoryIds.Any(c => c == cp.CategoryId)
            //        && productIds.Any(p => p == cp.ProductId);

            //    if (idsValid)
            //    {
            //        var categoryProduct = new CategoryProduct
            //        {
            //            CategoryId = cp.CategoryId,
            //            ProductId = cp.ProductId,
            //        };

            //        context.CategoryProducts.Add(categoryProduct);
            //    }
            //}

            context.CategoryProducts.AddRange(categoriesProducts);

            int count = context.SaveChanges();

            return $"Successfully imported {count}";
            //return $"Successfully imported {categoriesProducts.Length}";
        }

        // 05 GetProductsInRange
        public static string GetProductsInRange(ProductShopContext context)
        {
            const string rootElement = "Products";

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>());

            var mapper = config.CreateMapper();

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Take(10)
                .Select(p => new ExportProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = p.Buyer.FirstName + ' ' + p.Buyer.LastName
                })
                //.Select(p => mapper.Map<ExportProductDto>(p))
                .ToArray();
            //.ToList();

            XmlSerializer serializer = new XmlSerializer
                (typeof(ExportProductDto[]), new XmlRootAttribute(rootElement));

            var result = XmlConverter.Serialize(products, rootElement);
            //StringBuilder sb = new StringBuilder();
            //serializer.Serialize(new StringWriter(sb), products);

            return result;

            // Test
            //using XmlTextWriter xmlTextWriter = new XmlTextWriter(DirectoryResultsPath + "/products-in-rangeDemo.xml", Encoding.UTF8);
            //xmlTextWriter.Formatting = Formatting.Indented;
            //XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            //XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            //xmlNamespaces.Add(string.Empty, string.Empty);
            //serializer.Serialize(xmlTextWriter, products);
            //serializer.Serialize(xmlTextWriter, products, xmlNamespaces);

            //var serializer = new XmlSerializer(typeof(ExportProductDto[]), new XmlRootAttribute("Products"));
            //var sb = new StringBuilder();
            //serializer.Serialize(new StringWriter(sb), products, new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") }));
            //return sb.ToString().TrimEnd();

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "/products-in-range.xml", result);

            //return sb.ToString();
        }

        // 06 GetSoldProducts
        public static string GetSoldProducts(ProductShopContext context)
        {
            const string rootElement = "Users";

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>());

            var mapper = config.CreateMapper();

            var users = context.Users
                .Where(u => u.ProductsSold.Count(ps => ps.Buyer != null) > 0)
                //.Where(u => u.ProductsSold.Any())
                // Any() also applicable
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                // will throw exception when given ThenBy(u => u.LastName)
                .Take(5)
                .Select(u => new ExportUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold.Select(ps => new ExportSoldProductDto
                    {
                        Name = ps.Name,
                        Price = ps.Price,
                    })
                    //.Take(5) Error not from products list
                    .ToArray(),
                    //.ToList(),
                })
                //.Select(u => mapper.Map<ExportUserDto>(u))
                //.Include(u => u.ProductsSold.Select(sp => mapper.Map<ExportSoldProductDto>(sp)))
                //.Include(u => u.SoldProducts.Select(sp => mapper.Map<ExportSoldProductDto>(sp)))
                //.Include(u => u.SoldProducts)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute(rootElement));
            //XmlSerializer serializer = new XmlSerializer(typeof(List<ExportUserDto>), rootElement);
            // Error, serializer requires new XmlRootAttribute(rootElement) !!!

            //using XmlTextWriter xmlTextWriter = new XmlTextWriter(DirectoryResultsPath + "/users-sold-productsDemo.xml", Encoding.UTF8);
            //xmlTextWriter.Formatting = Formatting.Indented;
            //XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            //serializer.Serialize(xmlTextWriter, users, xmlNamespaces);

            //var result = XmlConverter.Serialize(users, rootElement);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "/users-sold-products.xml", result);

            var result = XmlConverter.Serialize(users, rootElement);

            return result;
        }

        // 07 GetCategoriesByProductsCount
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            const string rootElement = "Categories";

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>());

            var mapper = config.CreateMapper();

            var categories = context.Categories
                .Select(c => new ExportCategoryByProductDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count(),
                    //AveragePrice = Math.Round(x.CategoryProducts.Average(y => y.Product.Price), 28),
                    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                    // Alternative
                    // TotalRevenue = c.CategoryProducts.Select(cp => cp.Product).Sum(cp => cp.Price)
                })
                //.Select(c => mapper.Map<ExportCategoryByProductDto>(c))
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            //XmlSerializer serializer = new XmlSerializer
            //    (typeof(ExportCategoryByProductDto[]), new XmlRootAttribute(rootElement));

            //using XmlTextWriter xmlWriter = new XmlTextWriter(DirectoryResultsPath + "/categories-by-productsDemo.xml", Encoding.UTF8);
            //xmlWriter.Formatting = Formatting.Indented;

            //XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            //serializer.Serialize(xmlWriter, categories, namespaces);

            var result = XmlConverter.Serialize(categories, rootElement);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "/categories-by-products.xml", result);

            return result;
        }

        // 08
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            const string rootElement = "Users";

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>());

            var mapper = config.CreateMapper();

            int userCount = context.Users.Count(u => u.ProductsSold.Count > 0);

            //var usersTest = context.Users
            //.Where(u => u.ProductsSold.Any())
            //.OrderByDescending(u => u.ProductsSold.Count)
            //.Take(10)
            ////.Where(u => u.ProductsSold.Any(p => p.Buyer != null))
            ////.Select(u => new ExportUserProductsDto
            //.Select(u => new ExportUserCollectionDto
            ////{
            ////    Count = userCount,
            ////    Users = new ExportUserCollectionDto
            //{
            //    FirstName = u.FirstName,
            //    LastName = u.LastName,
            //    Age = u.Age,
            //    SoldProducts = new ExportProductCollectionDto
            //    {
            //        Count = u.ProductsSold.Count,
            //        Products = u.ProductsSold.Select(ps => new ExportProductDetailsDto
            //        {
            //            Name = ps.Name,
            //            Price = ps.Price
            //        }).OrderByDescending(ps => ps.Price).ToArray()
            //    }
            //}
            //Users = new ExportUserCollectionDto
            //{
            //    FirstName = u.FirstName,
            //    LastName = u.LastName,
            //    Age = u.Age,
            //    SoldProducts = new ExportProductCollectionDto
            //    {
            //        Count = u.ProductsSold.Count,
            //        Products = u.ProductsSold.Select(ps => new ExportProductDetailsDto
            //        {
            //            Name = ps.Name,
            //            Price = ps.Price
            //        }).ToArray()
            //        //Products = new ExportProductDetailsDto
            //        //{
            //        //    //Name = u.ProductsSold.Select(ps => ps.CategoryProducts.First(cp => cp.Product.Name)),
            //        //    Name = u.ProductsSold.Select(ps => ps.CategoryProducts.Select(cp => cp.Product.Name)),
            //        //    Price =
            //        //}
            //    }
            //}
            //}).ToArray();

            var usersAndProducts = context.Users
                //.ToArray() // fix for Exception InMemory Query Internal, Test 000_001
                .Where(u => u.ProductsSold.Any())
                .OrderByDescending(u => u.ProductsSold.Count)
                .Take(10)
                .Select(u => new ExportNestedUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new ExportNestedProductsDto
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold.Select(ps => new ExportNestedProductDetailDto
                        {
                            Name = ps.Name,
                            Price = ps.Price,
                        }).OrderByDescending(ps => ps.Price).ToArray()
                    }
                })
                .ToArray();

            var result = new ExportUserCountDto
            {
                //Count = usersAndProducts.Length,
                // error requires all valid users not just top ten
                Count = context.Users.Count(u => u.ProductsSold.Any()),
                Users = usersAndProducts
            };

            //XmlSerializer serializer = new XmlSerializer
            //    (typeof(ExportUserCountDto), new XmlRootAttribute(rootElement));

            //using XmlTextWriter xmlWriter = new XmlTextWriter(DirectoryResultsPath + "/users-and-productsDemo.xml", Encoding.UTF8);
            //xmlWriter.Formatting = Formatting.Indented;

            //XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            //serializer.Serialize(xmlWriter, result, namespaces);

            var final = XmlConverter.Serialize(result, rootElement);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "/users-and-products.xml", final);

            return final;
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void ResetDatabase(ProductShopContext context)
        {
            context.Database.EnsureDeleted();

            Console.WriteLine("Database was successfully deleted!");

            context.Database.EnsureCreated();

            Console.WriteLine("Database was successfully created!");
        }

        private static void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());
        }
    }
}
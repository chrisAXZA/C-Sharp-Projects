namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Globalization;
    using System.Collections.Generic;

    using AutoMapper;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Microsoft.EntityFrameworkCore;

    using CarDealer.DTO;
    using CarDealer.Data;
    using CarDealer.Models;

    public class StartUp
    {
        private static string DirectoryResultsPath = "../../../Datasets/Results/";

        public static void Main(string[] args)
        {
            using (var dbContext = new CarDealerContext())
            {
                //ResetDatabase(dbContext);
                InitialzeMapper();

                string carsJson = File.ReadAllText("../../../Datasets/cars.json");
                string customersJson = File.ReadAllText("../../../Datasets/customers.json");
                string partsJson = File.ReadAllText("../../../Datasets/parts.json");
                string salesJson = File.ReadAllText("../../../Datasets/sales.json");
                string suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");

                //string result = ImportSuppliers(dbContext, suppliersJson);
                //string result = ImportParts(dbContext, partsJson);
                //string result = ImportCars(dbContext, carsJson);
                //string result = ImportCustomers(dbContext, customersJson);
                //string result = ImportSales(dbContext, salesJson);
                //string result = GetOrderedCustomers(dbContext);
                //string result = GetCarsFromMakeToyota(dbContext);
                //string result = GetLocalSuppliers(dbContext);
                //string result = GetCarsWithTheirListOfParts(dbContext);
                string result = GetTotalSalesByCustomer(dbContext);
                //string result = GetSalesWithAppliedDiscount(dbContext);

                //string result = ImportSuppliers2(dbContext, suppliersJson);
                //string result = ImportParts2(dbContext, partsJson);
                //string result = ImportCars2(dbContext, carsJson);
                //string result = ImportCustomers2(dbContext, customersJson);
                //string result = ImportSales2(dbContext, salesJson);
                //string result = GetOrderedCustomers2(dbContext);
                //string result = GetCarsFromMakeToyota2(dbContext);
                //string result = GetLocalSuppliers2(dbContext);
                //string result = GetCarsWithTheirListOfParts2(dbContext);
                //string result = GetTotalSalesByCustomer2(dbContext);
                //string result = GetSalesWithAppliedDiscount2(dbContext);

                Console.WriteLine(result);
            }

        }

        public static string ImportSuppliers2(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }

        public static string ImportParts2(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson);

            var supplierIds = context.Suppliers.Select(s => s.Id).ToArray();

            var validParts = new List<Part>();

            foreach (var part in parts)
            {
                if (supplierIds.Any(id => id == part.SupplierId))
                {
                    validParts.Add(part);
                }
            }

            Part[] validParts2 = JsonConvert.DeserializeObject<Part[]>(inputJson)
             .Where(p => supplierIds.Any(s => s == p.SupplierId))
             //.Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
             .ToArray();

            context.AddRange(validParts2);

            context.SaveChanges();

            return $"Successfully imported {validParts2.Length}.";
        }

        public static string ImportCars2(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<ImportCarDTO[]>(inputJson);

            var listOfCars = new List<Car>();

            var carPartList = new List<PartCar>();

            foreach (var car in cars)
            {
                var currentCar = new Car
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance,
                };

                //foreach (var partId in car.PartCars.Select(pc => pc.PartId).Distinct())
                foreach (var partId in car.PartsId.Distinct())
                {
                    currentCar.PartCars.Add(new PartCar
                    {
                        PartId = partId,
                    });

                    var currentPart = new PartCar
                    {
                        Car = currentCar,
                        PartId = partId,
                    };

                    carPartList.Add(currentPart);
                }

                listOfCars.Add(currentCar);
            }

            int count = carPartList.Count;

            context.Cars.AddRange(listOfCars);

            context.SaveChanges();

            return $"Successfully imported {listOfCars.Count}.";

            //var finalCars = new List<Car>();

            //var finalCarParts = new List<PartCar>();

            //var validPartIds = context.Parts.Select(p => p.Id).ToArray();

            //foreach (var car in cars)
            //{
            //    var newCar = new Car
            //    {
            //        Make = car.Make,
            //        Model = car.Model,
            //        TravelledDistance = car.TravelledDistance,
            //    };

            //    foreach (var part in car.PartCars.Distinct())
            //    {
            //        // Alternative
            //        //newCar.PartCars.Add(new PartCar { PartId = part.PartId });

            //        if (validPartIds.Any(p => p == part.PartId))
            //        {
            //            var newCarPart = new PartCar
            //            {
            //                Car = newCar,
            //                PartId = part.PartId,
            //            };

            //            finalCarParts.Add(newCarPart);
            //        }
            //    }

            //    finalCars.Add(newCar);
            //}

            //context.Cars.AddRange(finalCars);

            //context.PartCars.AddRange(finalCarParts);

            //context.SaveChanges();

            //return $"Successfully imported {finalCars.Count}.";
        }

        public static string ImportCustomers2(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);

            context.SaveChanges();

            return $"Successfully imported {customers.Length}.";
        }

        public static string ImportSales2(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Length}.";
        }


        public static string GetOrderedCustomers2(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver == true)
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    IsYoungDriver = c.IsYoungDriver,
                })
                .ToArray();

            var resolver = new DefaultContractResolver
            {
                NamingStrategy = new DefaultNamingStrategy(),
            };

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = resolver,
            };

            var json = JsonConvert.SerializeObject(customers, jsonSettings);

            return json;
        }

        public static string GetCarsFromMakeToyota2(CarDealerContext context)
        {
            var toyotaCars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                })
                .ToArray();

            //var resolver = new DefaultContractResolver
            //{
                //NamingStrategy = (Newtonsoft.Json.Serialization.NamingStrategy)(new PascalCaseNamingConvention()),
                //NamingStrategy = (new PascalCaseNamingConvention()),
                //NamingStrategy = new PascalCaseNamingConvention(),
                //NamingStrategy = new KebabCaseNamingStrategy(),
            //};

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                //ContractResolver = resolver,
            };

            var json = JsonConvert.SerializeObject(toyotaCars, jsonSettings);

            return json;
        }

        public static string GetLocalSuppliers2(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count(),
                });

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };

            var json = JsonConvert.SerializeObject(suppliers, jsonSettings);

            return json;
        }

        public static string GetCarsWithTheirListOfParts2(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance,
                    },
                    parts = c.PartCars.Select(pc => new
                    {
                        Name = pc.Part.Name,
                        Price = pc.Part.Price.ToString("F2"),
                    }).ToArray(),
                    //parts = new
                    //{
                    //    Name = c.PartCars.Select(pc => pc.Part.Name),
                    //    Price = c.PartCars.Select(pc => pc.Part.Price.ToString("F2")),
                    //}
                })
                .ToArray();

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(cars, jsonSettings);

            return json;
        }

        public static string GetTotalSalesByCustomer2(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count() > 0)
                .Include(c => c.Sales)
                .ThenInclude(s => s.Car)
                .ThenInclude(c => c.PartCars)
                .ThenInclude(pc => pc.Part)
                .ToArray()
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price)),
                    //SpentMoney = c.Sales.Sum(s => (decimal)s.Car.PartCars.Select(pc => pc.Part.Price))
                })
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToArray();

            var resolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            };

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = resolver,
            };

            var json = JsonConvert.SerializeObject(customers, jsonSettings);

            return json;
        }

        public static string GetSalesWithAppliedDiscount2(CarDealerContext context)
        {
            var sales = context.Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance,
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount.ToString("F2"),
                    price = s.Car.PartCars.Sum(pc => pc.Part.Price).ToString("F2"),
                    priceWithDiscount = (s.Car.PartCars.Sum(pc => pc.Part.Price) * (1 - (s.Discount / 100))).ToString("F2"),
                })
                .ToArray();

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(sales, jsonSettings);

            return json;
        }

        // 01 Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            //User[] users = JsonConvert.DeserializeObject<User[]>(inputJson);

            //context.Users.AddRange(users);

            //context.SaveChanges();

            //return $"Successfully imported {users.Length}";

            Supplier[] suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }

        // 02 Import Parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            Part[] parts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                .ToArray();

            //foreach (var part in parts)
            //{
            //    if (context.Suppliers.Any(s => s.Id == part.SupplierId))
            //    {
            //        context.Parts.Add(part);
            //    }
            //}

            context.Parts.AddRange(parts);

            context.SaveChanges();

            //int count = context.Parts.Count();

            return $"Successfully imported {parts.Length}.";
        }

        // 03 Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            //Car[] cars = JsonConvert.DeserializeObject<Car[]>(inputJson);
            ImportCarDTO[] carsDTO = JsonConvert.DeserializeObject<ImportCarDTO[]>(inputJson);

            //Car[] carsDb = carsDTO.Select(cdto => Mapper.Map<Car>(cdto)).ToArray();
            var carsDb = new List<Car>();

            var carParts = new List<PartCar>();

            foreach (var carDTO in carsDTO)
            {
                var car = new Car
                {
                    Make = carDTO.Make,
                    Model = carDTO.Model,
                    TravelledDistance = carDTO.TravelledDistance,
                };

                foreach (var partId in carDTO.PartsId.Distinct())
                {
                    //var carPart = new PartCar {PartId = part.PartId, CarId = car. }
                    //context.PartCars.Add()

                    var carPart = new PartCar { PartId = partId, Car = car };

                    carParts.Add(carPart);
                }

                carsDb.Add(car);
            }

            context.Cars.AddRange(carsDb);

            context.PartCars.AddRange(carParts);

            context.SaveChanges();

            return $"Successfully imported {carsDb.Count()}.";
        }

        // 04 ImportCustomers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            Customer[] customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);

            context.SaveChanges();

            return $"Successfully imported {customers.Length}.";
        }

        // 05 ImportSales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            Sale[] sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Length}.";
        }

        // 06 GetOrderedCustomers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            //var users = context.Users
            //     .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
            //     .OrderBy(u => u.LastName)
            //     .ThenBy(u => u.FirstName)
            //.Select(u => new
            //{
            //    firstName = u.FirstName,
            //    lastName = u.LastName,
            //    soldProducts = u.ProductsSold
            //    .Where(p => p.Buyer != null)
            //    .Select(ps => new
            //    {
            //        name = ps.Name,
            //        price = ps.Price,
            //        buyerFirstName = ps.Buyer.FirstName,
            //        buyerLastName = ps.Buyer.LastName,
            //    })
            //    .ToArray() // important JSON array of objects !!!
            //})
            //.ProjectTo<UserWithSoldProductsDTO>()
            //.ToArray();

            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver == true)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    c.IsYoungDriver
                })
                .ToArray();

            var jsonFile = JsonConvert.SerializeObject(customers, Formatting.Indented);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "ordered-customers.json", jsonFile);

            return jsonFile;
        }

        // 07 Get Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotas = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(t => new
                {
                    t.Id,
                    t.Make,
                    t.Model,
                    t.TravelledDistance,
                })
                .ToArray();

            var jsonFile = JsonConvert.SerializeObject(toyotas, Formatting.Indented);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "toyota-cars.json", jsonFile);

            return jsonFile;
        }

        // 08 GetLocalSuppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToArray();

            var jsonFile = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "local-suppliers.json", jsonFile);

            return jsonFile;
        }

        // 09 GetCarsWithTheirListOfParts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            //      var users = context.Users
            //.Where(u => u.ProductsSold.Any(p => p.Buyer != null))
            //.OrderBy(u => u.LastName)
            //.ThenBy(u => u.FirstName)
            ////.Select(u => new
            ////{
            ////    firstName = u.FirstName,
            ////    lastName = u.LastName,
            ////    soldProducts = u.ProductsSold
            ////    .Where(p => p.Buyer != null)
            ////    .Select(ps => new
            ////    {
            ////        name = ps.Name,
            ////        price = ps.Price,
            ////        buyerFirstName = ps.Buyer.FirstName,
            ////        buyerLastName = ps.Buyer.LastName,
            ////    })
            ////    .ToArray() // important JSON array of objects !!!
            ////})
            //.ProjectTo<UserWithSoldProductsDTO>()
            //.ToArray();

            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    },
                    parts = c.PartCars.Select(pc => new
                    {
                        Name = pc.Part.Name,
                        Price = $"{pc.Part.Price:F2}"
                    }).ToArray(),
                }).ToArray();

            var jsonFile = JsonConvert.SerializeObject(cars, Formatting.Indented);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "cars-and-parts.json", jsonFile);

            return jsonFile;
        }

        // 10 GetTotalSalesByCustomer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count() >= 1)
                //.Include(c => c.Sales)
                //.ThenInclude(s => s.Car)
                //.ThenInclude(c => c.PartCars)
                //.ThenInclude(ps => ps.Part)
                //.ToList()
                //.Where(c => c.Sales.Any())
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(),
                    spentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
                    //spentMoney = c.Sales.Sum(s => new { totalParts = s.Car.PartCars.Sum(pc => pc.Part.Price) })
                    //spentMoney = c.Sales.Sum(s => Convert.ToDecimal(s.Car.PartCars.Select(pc => pc.Part.Price)))
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToArray();

            var jsonFile = JsonConvert.SerializeObject(customers, Formatting.Indented);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "customers-total-sales.json", jsonFile);

            return jsonFile;
        }

        // 11 GetSalesWithAppliedDiscount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = $"{s.Discount:F2}",
                    price = $"{s.Car.PartCars.Sum(pc => pc.Part.Price):F2}",
                    priceWithDiscount = $"{s.Car.PartCars.Sum(pc => pc.Part.Price) * (1 - (s.Discount / 100)):F2}",
                })
                .ToArray();

            var jsonFile = JsonConvert.SerializeObject(sales, Formatting.Indented);

            //EnsureDirectoryExists(DirectoryResultsPath);

            //File.WriteAllText(DirectoryResultsPath + "customers-total-sales.json", jsonFile);

            return jsonFile;
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void ResetDatabase(CarDealerContext dbContext)
        {
            dbContext.Database.EnsureDeleted();

            Console.WriteLine("Database was successfully deleted!");

            dbContext.Database.EnsureCreated();

            Console.WriteLine("Database was successfully created!");
        }

        private static void InitialzeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());
        }
    }
}

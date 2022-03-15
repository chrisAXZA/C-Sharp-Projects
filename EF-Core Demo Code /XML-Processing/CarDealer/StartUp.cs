namespace CarDealer
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

    using CarDealer.Data;
    using CarDealer.Models;
    using CarDealer.Dtos.Import;
    using CarDealer.Dtos.Export;
    //using CarDealer.Dtos2.Import;
    //using CarDealer.Dtos2.Export;

    public class StartUp
    {
        private static string DirectoryResultsPath = "../../../Datasets/Results/";

        public static void Main(string[] args)
        {
            string carsXml = File.ReadAllText("../../../Datasets/cars.xml");
            string customersXml = File.ReadAllText("../../../Datasets/customers.xml");
            string partsXml = File.ReadAllText("../../../Datasets/parts.xml");
            string salesXml = File.ReadAllText("../../../Datasets/sales.xml");
            string suppliersXml = File.ReadAllText("../../../Datasets/suppliers.xml");

            using var dbContext = new CarDealerContext();

            //Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            //ResetDatabase(dbContext);

            //string result = ImportSuppliers(dbContext, suppliersXml);
            //string result = ImportParts(dbContext, partsXml);
            //string result = ImportCars(dbContext, carsXml);
            //string result = ImportCustomers(dbContext, customersXml);
            //string result = ImportSales(dbContext, salesXml);

            //string result = GetCarsWithDistance(dbContext);
            //string result = GetCarsFromMakeBmw(dbContext);
            string result = GetLocalSuppliers(dbContext);
            //string result = GetTotalSalesByCustomer(dbContext);
            //string result = GetCarsWithTheirListOfParts(dbContext);
            //string result = GetSalesWithAppliedDiscount(dbContext);


            //string result = ImportSuppliers2(dbContext, suppliersXml);
            //string result = ImportParts2(dbContext, partsXml);
            //string result = ImportCars2(dbContext, carsXml);
            //string result = ImportCustomers2(dbContext, customersXml);
            //string result = ImportSales2(dbContext, salesXml);

            //string result = GetLocalSuppliers2(dbContext);
            //string result = GetCarsWithTheirListOfParts2(dbContext);


            Console.WriteLine(result);

        }

        //public static string ImportSuppliers2(CarDealerContext context, string inputXml)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer
        //        (typeof(ImportSupplierDto2[]), new XmlRootAttribute("Suppliers"));

        //    using StringReader stringReader = new StringReader(inputXml);

        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());
        //    //var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Employee, AutoEmp>());

        //    //Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>()));

        //    IMapper mapper = config.CreateMapper();

        //    var suppliersDto = (ImportSupplierDto2[])xmlSerializer.Deserialize(stringReader);

        //    var suppliers = suppliersDto
        //        .Select(s => mapper.Map<Supplier>(s))
        //        .ToArray();

        //    // Alternative !!!
        //    var suppliers2 = mapper.Map<Supplier[]>(suppliersDto);

        //    context.Suppliers.AddRange(suppliers);

        //    context.SaveChanges();

        //    return $"Successfully imported {suppliers.Length}";
        //}

        //public static string ImportParts2(CarDealerContext context, string inputXml)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer
        //        (typeof(ImportPartDto2[]), new XmlRootAttribute("Parts"));

        //    using var stringReader = new StringReader(inputXml);

        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());

        //    IMapper mapper = config.CreateMapper();

        //    var partsDto = (ImportPartDto2[])xmlSerializer.Deserialize(stringReader);

        //    var supplierIds = context.Suppliers.Select(s => s.Id).ToArray();

        //    var parts = partsDto
        //        .Where(pDto => supplierIds.Any(s => s == pDto.SupplierId))
        //        .Select(pDto => mapper.Map<Part>(pDto))
        //        .ToArray();

        //    context.Parts.AddRange(parts);

        //    context.SaveChanges();

        //    return $"Successfully imported {parts.Length}";
        //}

        //public static string ImportCars2(CarDealerContext context, string inputXml)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer
        //        (typeof(ImportCarDto2[]), new XmlRootAttribute("Cars"));

        //    using var reader = new StringReader(inputXml);

        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());

        //    IMapper mapper = config.CreateMapper();

        //    var carsDto = (ImportCarDto2[])xmlSerializer.Deserialize(reader);

        //    var carList = new List<Car>();

        //    var carPartList = new List<PartCar>();

        //    var partIds = context.Parts.Select(p => p.Id).ToArray();

        //    foreach (var carDto in carsDto)
        //    {
        //        var newCar = new Car
        //        {
        //            Make = carDto.Make,
        //            Model = carDto.Model,
        //            TravelledDistance = carDto.TraveledDistance,
        //        };

        //        var allPartIds = carDto.Parts
        //            .Select(p => p.PartId)
        //            .Where(pdto => partIds.Any(p => p == pdto))
        //            .Distinct()
        //            .ToArray();

        //        //foreach (var part in carDto.Parts.Distinct())
        //        foreach (var partId in allPartIds)
        //        {
        //            var carPart = new PartCar
        //            {
        //                PartId = partId,
        //                Car = newCar,
        //            };

        //            //newCar.PartCars.Add(new PartCar { Part = newPart });

        //            carPartList.Add(carPart);
        //        }

        //        carList.Add(newCar);
        //    }

        //    context.Cars.AddRange(carList);

        //    context.PartCars.AddRange(carPartList);

        //    context.SaveChanges();

        //    return $"Successfully imported {carList.Count}";
        //}

        //public static string ImportCustomers2(CarDealerContext context, string inputXml)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer
        //        (typeof(ImportCustomerDto2[]), new XmlRootAttribute("Customers"));

        //    using var reader = new StringReader(inputXml);

        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());

        //    IMapper mapper = config.CreateMapper();

        //    var customersDto = (ImportCustomerDto2[])xmlSerializer.Deserialize(reader);

        //    var customers = customersDto
        //        .Select(cDto => mapper.Map<Customer>(cDto))
        //        .ToArray();

        //    context.Customers.AddRange(customers);

        //    context.SaveChanges();

        //    return $"Successfully imported {customers.Length}";
        //}

        //public static string ImportSales2(CarDealerContext context, string inputXml)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer
        //        (typeof(ImportSalesDto2[]), new XmlRootAttribute("Sales"));

        //    using var reader = new StringReader(inputXml);

        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());

        //    IMapper mapper = config.CreateMapper();

        //    var salesDto = (ImportSalesDto2[])xmlSerializer.Deserialize(reader);

        //    var carIds = context.Cars.Select(c => c.Id).ToArray();

        //    var sales = salesDto.Select(s => mapper.Map<Sale>(s))
        //        .Where(s => carIds.Any(ci => ci == s.CarId))
        //        .ToArray();

        //    //var carIds = context.Cars.Select(c => c.Id).ToArray();

        //    //var salesDto = ((ImportSalesDto[])serializer.Deserialize(reader))
        //    //    .Where(sDto => carIds.Any(cid => cid == sDto.CarId))
        //    //    //.Where(sDto => context.Cars.Any(c => c.Id == sDto.CarId))
        //    //    .ToArray();

        //    //var sales = Mapper.Map<Sale[]>(salesDto);

        //    context.Sales.AddRange(sales);

        //    context.SaveChanges();

        //    return $"Successfully imported {sales.Length}";
        //}

        //public static string GetLocalSuppliers2(CarDealerContext context)
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());

        //    IMapper mapper = config.CreateMapper();

        //    var suppliersDto = context.Suppliers
        //        .Where(s => s.IsImporter == false)
        //        .ProjectTo<ExportLocalSupplierDto2>()
        //        .ToArray();

        //    XmlSerializer xmlSerializer = new XmlSerializer
        //        (typeof(ExportLocalSupplierDto2[]), new XmlRootAttribute("suppliers"));

        //    StringBuilder sb = new StringBuilder();

        //    var nameSpaces = new XmlSerializerNamespaces();
        //    nameSpaces.Add(string.Empty, string.Empty);
        //    using var writer = new StringWriter(sb);
        //    xmlSerializer.Serialize(writer, suppliersDto, nameSpaces);
        //    xmlSerializer.Serialize
        //        (File.CreateText("../../../test.xml"), suppliersDto, nameSpaces);

        //    // UTF 16 variant for StringBuilder2
        //    //StringBuilder sb2 = new StringBuilder();
        //    //using var writer2 = new StringWriter(sb2);
        //    //using var xmlWriter = new XmlTextWriter(writer2);
        //    using var xmlWriter = new XmlTextWriter("../../../test2.xml", Encoding.UTF8);
        //    xmlWriter.Formatting = Formatting.Indented;
        //    var nameSpaces2 = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
        //    xmlSerializer.Serialize(xmlWriter, suppliersDto, nameSpaces2);

        //    return sb.ToString().TrimEnd();
        //    //return sb2.ToString().TrimEnd();
        //}

        //public static string GetCarsWithTheirListOfParts2(CarDealerContext context)
        //{
        //    var cars = context.Cars
        //        .OrderByDescending(c => c.TravelledDistance)
        //        .ThenBy(c => c.Model)
        //        .Take(5)
        //        .Include(c => c.PartCars)
        //        .ThenInclude(pc => pc.Part)
        //        .ToArray();

        //    var carsDto = cars.
        //        Select(c => new ExportCarWithPartsDto2
        //        {
        //            Make = c.Make,
        //            Model = c.Model,
        //            TravelledDistance = c.TravelledDistance,
        //            Parts = c.PartCars.Select(pc => new PartCarDetailDto2
        //            {
        //                Name = pc.Part.Name,
        //                Price = pc.Part.Price,
        //            }).OrderByDescending(p => p.Price).ToArray(),
        //        })
        //        .ToArray();

        //    //var cars2 = context.Cars
        //    //    .OrderByDescending(c => c.TravelledDistance)
        //    //    .ThenBy(c => c.Model)
        //    //    .Take(5)
        //    //    .ProjectTo<ExportCarWithPartsDto2>()
        //    //    .ToArray();

        //    //foreach (var car in cars2)
        //    //{
        //    //    car.Parts = car.Parts.OrderByDescending(p => p.Price).ToArray();
        //    //}

        //    XmlSerializer xmlSerializer = new XmlSerializer
        //        (typeof(ExportCarWithPartsDto2[]), new XmlRootAttribute("cars"));

        //    StringBuilder sb = new StringBuilder();

        //    var nameSpaces = new XmlSerializerNamespaces();

        //    nameSpaces.Add("", "");

        //    using var writer = new StringWriter(sb);

        //    xmlSerializer.Serialize(writer, carsDto, nameSpaces);

        //    return sb.ToString().TrimEnd();
        //}

        // 01 ImportSuppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer
                (typeof(ImportSupplierDto[]), new XmlRootAttribute("Suppliers"));

            using StringReader reader = new StringReader(inputXml);

            //var suppliersDto = (ImportSupplierDto[])serializer.Deserialize(new StringReader(inputXml));
            var suppliersDto = (ImportSupplierDto[])serializer.Deserialize(reader);

            var suppliers = suppliersDto
                .Select(s => new Supplier
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter,
                })
                //.Select(s => Mapper.Map<Supplier>(s))
                .ToArray();

            //var suppliers2 = Mapper.Map<Supplier[]>(suppliersDto);

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        // 02 ImportParts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer
                (typeof(ImportPartDto[]), new XmlRootAttribute("Parts"));

            using StringReader reader = new StringReader(inputXml);
            //using StringReader reader2 = new StringReader(inputXml);

            var partsDto = (ImportPartDto[])serializer.Deserialize(reader);
            //var partsDto = (ImportPartDto[])serializer.Deserialize(new StringReader(inputXml));

            //XmlSerializer serializer2 = new XmlSerializer
            //    (typeof(ImportPartDto[]), new XmlRootAttribute("Parts"));

            // better practise to already filter at DTO level
            //var partsDto2 = ((ImportPartDto[])serializer.Deserialize(reader2))
            //    .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
            //    .ToArray();

            //var parts2 = Mapper.Map<Part[]>(partsDto2);

            //var partsId = context.Parts.Select(p => p.Id).ToArray();
            var suppliersId = context.Suppliers.Select(p => p.Id).ToArray();

            var parts = partsDto
                .Where(p => suppliersId.Any(pi => pi == p.SupplierId))
                //.Where(cp => context.Categories.Any(c => c.Id == cp.CategoryId) && context.Products.Any(p => p.Id == cp.ProductId))
                .Select(p => new Part
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierId = p.SupplierId,
                })
                .ToArray();

            context.Parts.AddRange(parts);

            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }

        //03 ImportCars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer
                (typeof(ImportCarDto[]), new XmlRootAttribute("Cars"));

            using StringReader reader = new StringReader(inputXml);

            //var carsDto = (ImportCarDto[])serializer.Deserialize(new StringReader(inputXml));
            var carsDto = (ImportCarDto[])serializer.Deserialize(reader);

            var partsId = context.Parts.Select(p => p.Id).ToArray();

            List<Car> cars = new List<Car>();
            List<PartCar> carParts = new List<PartCar>();

            var allParts = context.Parts.Select(p => p.Id);

            //List<Car> alterCars = new List<Car>();

            foreach (var carDto in carsDto)
            {
                Car currentCar = new Car
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TraveledDistance,
                };

                var allPartIds = carDto.Parts
                    // collection of parts DTO
                    .Select(p => p.PartId)
                    .Where(pdto => allParts.Any(p => p == pdto))
                    // take only part IDs that are in DB
                    .Distinct()
                    // take only distinct, no double
                    .ToArray();

                // Alternative
                //var alterCar = new Car
                //{
                //    Make = carDto.Make,
                //    Model = carDto.Model,
                //    TravelledDistance = carDto.TraveledDistance,
                //    PartCars = allPartIds.Select(id => new PartCar
                //    {
                //        PartId = id,
                //    }).ToArray(),
                //};
                //alterCars.Add(alterCar);

                //foreach (var partIdDto in carDto.Parts.Select(p => p.PartId).Distinct())
                foreach (var partIdDto in allPartIds)
                {
                    PartCar part = new PartCar
                    {
                        PartId = partIdDto,
                        Car = currentCar
                    };

                    carParts.Add(part);
                }

                cars.Add(currentCar);
            }

            context.Cars.AddRange(cars);

            context.PartCars.AddRange(carParts);

            context.SaveChanges();

            //var cars = carsDto
            //    .Where(c => partsId.Any(pi => pi == c.Parts.Any(pa => pa.PartId))))
            //    //.Where(c => partsId.Any(pi => pi == c.Parts.Any(pa => pa.PartId)))
            //    .Select(c => new ImportCarDto
            //     {
            //         Make = c.Make,
            //         Model = c.Model,
            //         TraveledDistance = c.TraveledDistance
            //        Parts = c.Select(pa => new ImportPartDto
            //        {

            //        })
            //     })

            return $"Successfully imported {cars.Count}";
        }

        //04 ImportCustomers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            //XmlSerializer serializer = new XmlSerializer
            //    (typeof(Customer[]), new XmlRootAttribute("Customers"));
            //var customers = (Customer[])serializer.Deserialize(reader);

            XmlSerializer serializer = new XmlSerializer
                (typeof(ImportCustomerDto[]), new XmlRootAttribute("Customers"));

            using StringReader reader = new StringReader(inputXml);

            var customersDto = (ImportCustomerDto[])serializer.Deserialize(reader);

            //var customers = Mapper.Map<Customer[]>(customersDto);

            var customers = customersDto.Select(c => new Customer
            {
                Name = c.Name,
                BirthDate = DateTime.Parse(c.BirthDate),
                IsYoungDriver = c.IsYoungDriver,
            })
            .ToArray();

            context.Customers.AddRange(customers);

            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }

        // 05 ImportSales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer
                (typeof(ImportSalesDto[]), new XmlRootAttribute("Sales"));

            using StringReader reader = new StringReader(inputXml);
            //using StringReader reader2 = new StringReader(inputXml);

            var carIds = context.Cars.Select(c => c.Id).ToArray();

            var salesDto = ((ImportSalesDto[])serializer.Deserialize(reader))
                //.Where(sDto => carIds.Any(cid => cid == sDto.CarId))
                .Where(sDto => context.Cars.Any(c => c.Id == sDto.CarId))
                .ToArray();

            //var salesDto1 = (ImportSalesDto[])serializer.Deserialize(reader2);

            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            var sales = Mapper.Map<Sale[]>(salesDto);

            context.Sales.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Length}";
        }

        // 06 GetCarsWithDistance
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            var carsDto = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<ExportDistanceCarsDto>()
                .ToArray();

            XmlSerializer serializer = new XmlSerializer
                (typeof(ExportDistanceCarsDto[]), new XmlRootAttribute("cars"));


            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");

            //EnsureDirectoryExists();

            //serializer.Serialize
            //    (File.CreateText("../../../Datasets/Results/cars.xml"), carsDto, xmlNamespaces);

            StringBuilder sb = new StringBuilder();

            using var writer = new StringWriter(sb);

            serializer.Serialize(writer, carsDto, xmlNamespaces);

            var result = sb.ToString().TrimEnd();

            return result;
        }

        // 07 GetCarsFromMakeBmw
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            var bmwDto = context.Cars
                .Where(c => c.Make == "BMW")
                //.Select(c => Mapper.Map<ExportBmwDto>(c))
                .ProjectTo<ExportBmwDto>()
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer
                (typeof(ExportBmwDto[]), new XmlRootAttribute("cars"));

            //EnsureDirectoryExists();

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");

            //serializer.Serialize
            //    (File.CreateText("../../../Datasets/Results/bmw-cars.xml"), bmwDto, xmlNamespaces);

            StringBuilder sb = new StringBuilder();

            using var writer = new StringWriter(sb);

            serializer.Serialize(writer, bmwDto, xmlNamespaces);

            var result = sb.ToString().TrimEnd();

            return result;
        }

        // 08 GetLocalSuppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .ToArray();

            var suppliersDto = context.Suppliers
                .Where(s => s.IsImporter == false)
                .ProjectTo<ExportLocalSupplierDto>()
                .ToArray();

            //var suppliersDto = Mapper.Map<ExportLocalSupplierDto[]>(suppliers);

            XmlSerializer serializer = new XmlSerializer
                (typeof(ExportLocalSupplierDto[]), new XmlRootAttribute("suppliers"));

            //EnsureDirectoryExists();

            //XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");
            //xmlNamespaces.Add(String.Empty, String.Empty);
            //xmlNamespaces.Add("xmlns", "alabala");

            //serializer.Serialize(File.CreateText("../../../Datasets/Results/local-suppliers.xml"), suppliersDto, xmlNamespaces);

            //using XmlTextWriter xmlTextWriter = new XmlTextWriter(DirectoryResultsPath + "/users-sold-productsDemo.xml", Encoding.UTF8);
            //xmlTextWriter.Formatting = Formatting.Indented;
            //XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            //serializer.Serialize(xmlTextWriter, users, xmlNamespaces);
            //using var writer = new StringWriter(builder);
            //serializer.Serialize(writer, dataTransferObjects, GetXmlNamespaces());

            //return builder.ToString();

            StringBuilder sb = new StringBuilder();
            //XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            using var writer = new StringWriter(sb);

            serializer.Serialize(writer, suppliersDto, xmlNamespaces);

            var result = sb.ToString().TrimEnd();

            return result;
        }

        // 09 GetCarsWithTheirListOfParts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            // Manual Mapping DEMO
            //var carsDemo = context.Cars
            //    .OrderByDescending(c => c.TravelledDistance)
            //    .ThenBy(c => c.Model)
            //    .Take(5)
            //    .Include(c => c.PartCars)
            //    // Exception, Lambda expression inside Include() not allowed !!!
            //    //.Include(c => c.PartCars.Select(pc => pc.Part))
            //    .ThenInclude(pc => pc.Part)
            //    .ToArray();

            //List<ExportCarPartsDto> exportCars = new List<ExportCarPartsDto>();

            //foreach (var car in carsDemo)
            //{
            //    ExportCarPartsDto currentCar = new ExportCarPartsDto
            //    {
            //        Make = car.Make,
            //        Model = car.Model,
            //        TravelledDistance = car.TravelledDistance,
            //        Parts = car.PartCars
            //        .Select(pc => pc.Part)
            //        // Additional pulling from DB, if not done beforehand !!!
            //        //.Select(pc => context.Parts.Find(pc.PartId))
            //        .Select(p => new ExportCarPartsInfoDto
            //        {
            //            Name = p.Name,
            //            Price = p.Price,
            //        }).OrderByDescending(p => p.Price).ToArray(),
            //        //Parts = car.PartCars.Select(pc => new ExportCarPartsInfoDto
            //        //{
            //        //    Name = pc.Part.Name,
            //        //    Price = pc.Part.Price
            //        //}).OrderByDescending(p => p.Price).ToArray()
            //    };

            //    exportCars.Add(currentCar);
            //}

            //XmlSerializer manualSerializer = new XmlSerializer
            //    (typeof(List<ExportCarPartsDto>), new XmlRootAttribute("cars"));

            var carsDto = context.Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .Select(c => new ExportCarPartsDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars.Select(pc => pc.Part).Select(p => new ExportCarPartsInfoDto
                    {
                        Name = p.Name,
                        Price = p.Price,
                    }).OrderByDescending(p => p.Price).ToArray(),
                }).ToArray();

            var carsDtoMap = context.Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ProjectTo<ExportCarPartsDto>()
                .ToArray();

            // final sorting for nested collection, alternative in MapperProfile include sorting
            //foreach (var car in carsDtoMap)
            //{
            //    car.Parts = car.Parts.OrderByDescending(p => p.Price).ToArray();
            //}

            XmlSerializer serializer = new XmlSerializer
                (typeof(ExportCarPartsDto[]), new XmlRootAttribute("cars"));
            // serializer always takes most outer DTO !!!, Root usually represent
            // collection of DTOs

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");

            //EnsureDirectoryExists();

            //serializer.Serialize
            //    (File.CreateText("../../../Datasets/Results/cars-and-parts.xml"), carsDto, xmlNamespaces);

            StringBuilder sb = new StringBuilder();

            using var writer = new StringWriter(sb);

            serializer.Serialize(writer, carsDto, xmlNamespaces);

            return sb.ToString().TrimEnd();
        }

        // 10 GetTotalSalesByCustomer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customersDto = context.Customers
                .Where(c => c.Sales.Count() > 0)
                .Include(c => c.Sales)
                .ThenInclude(s => s.Car)
                .ThenInclude(ca => ca.PartCars)
                .ThenInclude(pc => pc.Part)
                .AsEnumerable()
                //.ToArray() // not working, not inlcuding nested models
                .Select(c => new ExportTotalSalesDto
                {
                    Name = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))

                    //c.Sales.Sum(s =>
                    //s.Car.Parts.Sum(p => p.Price)

                    //context.PartCars
                    //.Where(pc => pc.Car.Sales.Any(s =>s.CustomerId == c.CustomerId))
                    //.Sum(s => s.Part.Price)

                })
                .OrderByDescending(c => c.SpentMoney)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer
                (typeof(ExportTotalSalesDto[]), new XmlRootAttribute("customers"));

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");

            //EnsureDirectoryExists();

            //serializer.Serialize
            //    (File.CreateText("../../../Datasets/Results/customers-total-sales.xml"), customersDto, xmlNamespaces);

            StringBuilder sb = new StringBuilder();

            using var writer = new StringWriter(sb);

            serializer.Serialize(writer, customersDto, xmlNamespaces);

            return sb.ToString().TrimEnd();
        }

        // 11 GetSalesWithAppliedDiscount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var customersDto = context.Sales
                .Select(s => new ExportSalesDiscountDto
                {
                    Car = new ExportCarDiscountDto
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance,
                    },
                    Discount = s.Discount,
                    Name = s.Customer.Name,
                    Price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                    PriceWithDiscount = (s.Car.PartCars.Sum(pc => pc.Part.Price)) -
                         (s.Car.PartCars.Sum(pc => pc.Part.Price)) * (s.Discount / 100)
                    // will cut decimal to second number after decimal

                    //PriceWithDiscount = (s.Car.PartCars.Sum(pc => pc.Part.Price)) * (1 - (s.Discount / 100))
                    //PriceWithDiscount = string.Format($"{0:0.00}",(s.Car.PartCars.Sum(pc => pc.Part.Price)) * (1 - (s.Discount / 100)))
                    //PriceWithDiscount = Math.Round((s.Car.PartCars.Sum(pc => pc.Part.Price)) * (1 - (s.Discount / 100)), 2)
                    //string.Format("{0:0.00}", number)
                    // or price * discount / 100 
                })
                .ToArray();

            XmlSerializer serializer = new XmlSerializer
                (typeof(ExportSalesDiscountDto[]), new XmlRootAttribute("sales"));

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");

            //EnsureDirectoryExists();

            //serializer.Serialize
            //    (File.CreateText("../../../Datasets/Results/sales-discounts.xml"), customersDto, xmlNamespaces);

            StringBuilder sb = new StringBuilder();

            using var writer = new StringWriter(sb);

            serializer.Serialize(writer, customersDto, xmlNamespaces);

            return sb.ToString().TrimEnd();
        }

        private static void EnsureDirectoryExists()
        {
            if (!Directory.Exists("../../../Datasets/Results"))
            {
                Directory.CreateDirectory("../../../Datasets/Results");
            }
        }

        private static void ResetDatabase(CarDealerContext dbContext)
        {
            dbContext.Database.EnsureDeleted();

            Console.WriteLine("Database was successfully deleted!");

            dbContext.Database.EnsureCreated();

            Console.WriteLine("Database was successfully created!");
        }
    }
}
namespace CarDealer
{
    using System;
    using System.Linq;

    using AutoMapper;

    using CarDealer.Models;
    using CarDealer.Dtos.Import;
    using CarDealer.Dtos.Export;
    //using CarDealer.Dtos2.Import;
    //using CarDealer.Dtos2.Export;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            // 01
            this.CreateMap<ImportSupplierDto, Supplier>();
            //this.CreateMap<ImportSupplierDto2, Supplier>();

            // 02
            this.CreateMap<ImportPartDto, Part>();
            //this.CreateMap<ImportPartDto2, Part>();

            // 03
            //this.CreateMap<ImportCarDto2, Car>();

            // 04
            this.CreateMap<ImportCustomerDto, Customer>()
                .ForMember(p => p.BirthDate,
                src => src.MapFrom(cdto => DateTime.Parse(cdto.BirthDate)));

            //this.CreateMap<ImportCustomerDto2, Customer>()
            //    .ForMember(p => p.BirthDate,
            //    src => src.MapFrom(cDto => DateTime.Parse(cDto.BirthDate)));

            //this.CreateMap<ImportCustomerDto, Customer>();

            // 05
            this.CreateMap<ImportSalesDto, Sale>();
            //this.CreateMap<ImportSalesDto2, Sale>();

            // Local supplier
            this.CreateMap<Supplier, ExportLocalSupplierDto>()
                .ForMember(p => p.PartsCount,
                src => src.MapFrom(sup => sup.Parts.Count));
            // not required AutoMapper can recognise Parts + Count !!!
            //this.CreateMap<Supplier, ExportLocalSupplierDto2>()
            //    .ForMember(p => p.PartsCount,
            //    src => src.MapFrom(s => s.Parts.Count()));

            // Distance
            this.CreateMap<Car, ExportDistanceCarsDto>();

            // BMW
            this.CreateMap<Car, ExportBmwDto>();

            // CarListParts
            this.CreateMap<Part, ExportCarPartsInfoDto>();

            this.CreateMap<Car, ExportCarPartsDto>()
                .ForMember(p => p.Parts,
                src => src.MapFrom(ca => ca.PartCars.Select(pc => pc.Part)
                .OrderByDescending(pa => pa.Price)));
            // ForMember requires final object as the one given in the mapper
            // for ExportCarPartsInfoDto (in this case Part)

            //this.CreateMap<Part, PartCarDetailDto2>();

            //this.CreateMap<Car, ExportCarWithPartsDto2>()
            //    .ForMember(p => p.Parts,
            //    src => src.MapFrom(c => c.PartCars.Select(pc => pc.Part)));

            // SalesDiscount
        }
    }
}

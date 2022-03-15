namespace CarDealer
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    
    using AutoMapper;

    using CarDealer.DTO;
    using CarDealer.Models;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            // Import Cars
            this.CreateMap<ImportCarDTO, Car>();
        }
    }
}

using AutoMapper;
using SistemaPeaje.API.DTOs;
using SistemaPeaje.API.Models;

namespace SistemaPeaje.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Vehicle mappings
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<CreateVehicleDto, Vehicle>();
            CreateMap<UpdateVehicleDto, Vehicle>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
            // TollStation mappings
            CreateMap<TollStation, TollStationDto>();
            CreateMap<CreateTollStationDto, TollStation>();
            CreateMap<UpdateTollStationDto, TollStation>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
            // TollTransaction mappings
            CreateMap<TollTransaction, TollTransactionDto>();
            CreateMap<CreateTollTransactionDto, TollTransaction>();
            CreateMap<ProcessTollTransactionDto, TollTransaction>();
        }
    }
}

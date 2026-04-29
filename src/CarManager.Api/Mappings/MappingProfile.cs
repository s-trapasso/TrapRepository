using AutoMapper;
using CarManager.Api.DTOs;
using CarManager.Api.DTOs.Maintenance;
using CarManager.Api.DTOs.Owner;
using CarManager.Api.DTOs.Vehicle;
using CarManager.Core.Enums;
using CarManager.Core.Models;

namespace CarManager.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            // =========================
            // VEHICLE
            // =========================
            CreateMap<Vehicle, VehicleDTO>()
                .ForMember(dest => dest.Owner,
                    opt => opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.OwnerDisplay,
                    opt => opt.MapFrom(src => src.Owner != null ? src.Owner.FirstName + " " + src.Owner.LastName : null));

            CreateMap<CreateVehicleDTO, Vehicle>()
            .ForMember(dest => dest.CurrentTireType,
                opt => opt.MapFrom(_ => TireType.AllSeason))
            .ForMember(dest => dest.LastTireChangeDate,
                opt => opt.MapFrom(_ => (DateTime?)null));

            CreateMap<UpdateVehicleDTO, Vehicle>();


            // =========================
            // OWNER
            // =========================
            CreateMap<Owner, OwnerDTO>();

            CreateMap<CreateOwnerDTO, Owner>();
            CreateMap<UpdateOwnerDTO, Owner>();


            // =========================
            // MAINTENANCE
            // =========================
            CreateMap<Maintenance, MaintenanceDTO>()
                .ForMember(dest => dest.VehiclePlate,
                    opt => opt.MapFrom(src => src.Vehicle.Plate));

            CreateMap<CreateMaintenanceDTO, Maintenance>();
            CreateMap<UpdateMaintenanceDTO, Maintenance>();
            CreateMap<TireChangeDTO, Maintenance>()
                 .ForMember(dest => dest.MaintenanceType, opt => opt.MapFrom(_ => MaintenanceType.TireChange))
                 .ForMember(dest => dest.Description, opt => opt.Ignore());


        }
    }
}

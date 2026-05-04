using AutoMapper;
using VehicleManager.Shared.DTOs;
using VehicleManager.Shared.Entities;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ── Vehicle ───────────────────────────────────────────────────────────

        CreateMap<Vehicle, VehicleDto>()
            .ForMember(dest => dest.ProprietarioAttuale, opt => opt.MapFrom(src =>
                src.Proprietari
                    .Where(p => p.DataCessione == null)
                    .Select(p => p.Owner.NomeCompleto)
                    .FirstOrDefault()))
            .ForMember(dest => dest.NumeroManutenzioni, opt => opt.MapFrom(src =>
                src.Manutenzioni.Count))
            .ForMember(dest => dest.ScadenzeInScadenza, opt => opt.MapFrom(src =>
                src.Scadenze.Count(d => d.Stato == DeadlineStatus.InScadenza)))
            .ForMember(dest => dest.ScadenzeScadute, opt => opt.MapFrom(src =>
                src.Scadenze.Count(d => d.Stato == DeadlineStatus.Scaduta)));

        CreateMap<Vehicle, VehicleSummaryDto>()
            .ForMember(dest => dest.ProprietarioAttuale, opt => opt.MapFrom(src =>
                src.Proprietari
                    .Where(p => p.DataCessione == null)
                    .Select(p => p.Owner.NomeCompleto)
                    .FirstOrDefault()))
            .ForMember(dest => dest.ScadenzeScadute, opt => opt.MapFrom(src =>
                src.Scadenze.Count(d => d.Stato == DeadlineStatus.Scaduta)))
            .ForMember(dest => dest.ScadenzeInScadenza, opt => opt.MapFrom(src =>
                src.Scadenze.Count(d => d.Stato == DeadlineStatus.InScadenza)));

        CreateMap<CreateVehicleDto, Vehicle>();
        CreateMap<UpdateVehicleDto, Vehicle>();

        // ── Owner ─────────────────────────────────────────────────────────────

        CreateMap<Owner, OwnerDto>()
            .ForMember(dest => dest.NumeroVeicoli, opt => opt.MapFrom(src =>
                src.Veicoli.Count))
            .ForMember(dest => dest.StatoPatente, opt => opt.MapFrom(src =>
                src.StatoPatente))
            .ForMember(dest => dest.StatoVisitaMedica, opt => opt.MapFrom(src =>
                src.StatoVisitaMedica));

        CreateMap<CreateOwnerDto, Owner>();
        CreateMap<UpdateOwnerDto, Owner>();

        // ── VehicleOwnership ──────────────────────────────────────────────────

        CreateMap<VehicleOwnership, VehicleOwnershipDto>()
            .ForMember(dest => dest.NomeProprietario, opt => opt.MapFrom(src =>
                src.Owner.NomeCompleto))
            .ForMember(dest => dest.IsAttuale, opt => opt.MapFrom(src =>
                src.IsAttuale));

        CreateMap<CreateVehicleOwnershipDto, VehicleOwnership>();

        // ── Maintenance ───────────────────────────────────────────────────────

        CreateMap<Maintenance, MaintenanceDto>()
            .ForMember(dest => dest.VehicleLabel, opt => opt.MapFrom(src =>
                $"{src.Vehicle.Marca} {src.Vehicle.Modello} ({src.Vehicle.Targa})"));

        CreateMap<CreateMaintenanceDto, Maintenance>();
        CreateMap<UpdateMaintenanceDto, Maintenance>();

        // ── Deadline ──────────────────────────────────────────────────────────

        CreateMap<Deadline, DeadlineDto>()
            .ForMember(dest => dest.VehicleLabel, opt => opt.MapFrom(src =>
                $"{src.Vehicle.Marca} {src.Vehicle.Modello} ({src.Vehicle.Targa})"))
            .ForMember(dest => dest.GiorniAllaScadenza, opt => opt.MapFrom(src =>
                src.GiorniAllaScadenza))
            .ForMember(dest => dest.Stato, opt => opt.MapFrom(src =>
                src.Stato));

        CreateMap<CreateDeadlineDto, Deadline>();
        CreateMap<UpdateDeadlineDto, Deadline>();
    }
}
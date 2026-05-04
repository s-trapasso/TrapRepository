using VehicleManager.Shared.DTOs;

namespace VehicleManager.Api.Controllers;

/// <summary>
/// DTO aggregato per la dashboard: raccoglie tutti gli alert in una sola chiamata API.
/// </summary>
public class DashboardAlertDto
{
    public int ScaduteCount { get; set; }
    public int InScadenzaCount { get; set; }
    public int PatenteAlertCount { get; set; }
    public int VisitaAlertCount { get; set; }

    public IEnumerable<DeadlineDto> Scadute { get; set; } = [];
    public IEnumerable<DeadlineDto> InScadenza { get; set; } = [];
    public IEnumerable<OwnerDto> PatenteInScadenza { get; set; } = [];
    public IEnumerable<OwnerDto> VisitaInScadenza { get; set; } = [];
}
using VehicleManager.Shared.DTOs;

namespace VehicleManager.App;

/// <summary>
/// Rispecchia il DashboardAlertDto dell'API.
/// Definito nel progetto App per non creare dipendenza dal progetto Api.
/// </summary>
public class DashboardAlertData
{
    public int ScaduteCount { get; set; }
    public int InScadenzaCount { get; set; }
    public int PatenteAlertCount { get; set; }
    public int VisitaAlertCount { get; set; }

    public List<DeadlineDto> Scadute { get; set; } = [];
    public List<DeadlineDto> InScadenza { get; set; } = [];
    public List<OwnerDto> PatenteInScadenza { get; set; } = [];
    public List<OwnerDto> VisitaInScadenza { get; set; } = [];

    public int TotaleAlert => ScaduteCount + InScadenzaCount + PatenteAlertCount + VisitaAlertCount;
}
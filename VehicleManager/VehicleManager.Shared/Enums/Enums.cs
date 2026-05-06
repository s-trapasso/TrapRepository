using System.ComponentModel;

namespace VehicleManager.Shared.Enums;

public enum VehicleType
{
    Unknown = 0,
    [Description("Automobile")] Automobile = 1,
    [Description("Moto")] Moto = 2,
    [Description("Furgone")] Furgone = 3,
    [Description("Camion")] Camion = 4,
    [Description("Bicicletta")] Bicicletta = 5,
    [Description("Altro")] Altro = 99
}

public enum FuelType
{
    Unknown = 0,
    [Description("Benzina")] Benzina = 1,
    [Description("Diesel")] Diesel = 2,
    [Description("Ibrido")] Ibrido = 3,
    [Description("Elettrico")] Elettrico = 4,
    [Description("GPL")] GPL = 5,
    [Description("Metano")] Metano = 6,
    [Description("Altro")] Altro = 99
}

public enum MaintenanceType
{
    Unknown = 0,
    [Description("Tagliando")] Tagliando = 1,
    [Description("Cambio olio")] CambioOlio = 2,
    [Description("Cambio pneumatici")] CambioPneumatici = 3,
    [Description("Freni anteriori")] FreniAnteriori = 4,
    [Description("Freni posteriori")] FreniPosteriori = 5,
    [Description("Batteria")] Batteria = 6,
    [Description("Cinghia distribuzione")] CorreaDistribuzione = 7,
    [Description("Filtri")] Filtri = 8,
    [Description("Revisione generale")] RevisioneGenerale = 9,
    [Description("Carrozzeria")] Carrozzeria = 10,
    [Description("Elettronica")] Elettronica = 11,
    [Description("Altro")] Altro = 99
}

public enum DeadlineType
{
    Unknown = 0,
    [Description("Bollo")] Bollo = 1,
    [Description("Assicurazione")] Assicurazione = 2,
    [Description("Revisione")] Revisione = 3,
    [Description("Tagliando")] Tagliando = 4,
    [Description("Visita medica")] PatenteMedica = 5,
    [Description("Altro")] Altro = 99
}

public enum DeadlineStatus
{
    [Description("Valida")] Valida = 0,
    [Description("In scadenza")] InScadenza = 1,
    [Description("Scaduta")] Scaduta = 2
}

public enum OwnershipType
{
    [Description("Proprietario")] Proprietario = 0,
    [Description("Intestatario")] Intestatario = 1,
    [Description("Utilizzatore")] Utilizzatore = 2
}
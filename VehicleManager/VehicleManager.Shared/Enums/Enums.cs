namespace VehicleManager.Shared.Enums;

public enum VehicleType
{
    Automobile = 0,
    Moto = 1,
    Furgone = 2,
    Camion = 3,
    Bicicletta = 4,
    Altro = 99
}

public enum FuelType
{
    Benzina = 0,
    Diesel = 1,
    Ibrido = 2,
    Elettrico = 3,
    GPL = 4,
    Metano = 5,
    Altro = 99
}

public enum MaintenanceType
{
    Tagliando = 0,
    CambioOlio = 1,
    CambioPneumatici = 2,
    FreniAnteriori = 3,
    FreniPosteriori = 4,
    Batteria = 5,
    CorreaDistribuzione = 6,
    Filtri = 7,
    RevisioneGenerale = 8,
    Carrozzeria = 9,
    Elettronica = 10,
    Altro = 99
}

public enum DeadlineType
{
    Bollo = 0,
    Assicurazione = 1,
    Revisione = 2,
    Tagliando = 3,
    PatenteMedica = 4,
    Altro = 99
}

public enum DeadlineStatus
{
    Valida = 0,  // > 30 giorni alla scadenza
    InScadenza = 1,  // <= 30 giorni
    Scaduta = 2   // già scaduta
}

public enum OwnershipType
{
    Proprietario = 0,
    Intestatario = 1,
    Utilizzatore = 2  // es. auto aziendale in uso
}
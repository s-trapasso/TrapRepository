using System.ComponentModel.DataAnnotations;

namespace CarDesk.Data.Models.Enum
{
    public enum TipoInterventoEnum
    {
        [Display(Name = "Cambio Olio e Filtro")]
        CambioOlioFiltro,

        [Display(Name = "Sostituzione Filtro Aria")]
        SostituzioneFiltroAria,

        [Display(Name = "Sostituzione Filtro Abitacolo")]
        SostituzioneFiltroAbitacolo,

        [Display(Name = "Sostituzione Cinghie")]
        SostituzioneCinghie,

        [Display(Name = "Rabbocco Liquidi")]
        RabboccoLiquidi,

        [Display(Name = "Controllo Pneumatici")]
        ControlloPneumatici,

        [Display(Name = "Sostituzione Pastiglie Freno")]
        SostituzionePastiglieFreno,

        [Display(Name = "Sostituzione Dischi Freno")]
        SostituzioneDischiFreno,

        [Display(Name = "Sostituzione Ammortizzatori")]
        SostituzioneAmmortizzatori,

        [Display(Name = "Controllo Freni")]
        ControlloFreni,

        [Display(Name = "Sostituzione Candele")]
        SostituzioneCandele,

        [Display(Name = "Diagnostica Motore")]
        DiagnosticaMotore,

        [Display(Name = "Riparazione Scarico")]
        RiparazioneScarico,

        [Display(Name = "Sostituzione Batteria")]
        SostituzioneBatteria,

        [Display(Name = "Sostituzione Lampadine")]
        SostituzioneLampadine,

        [Display(Name = "Riparazione Carrozzeria")]
        RiparazioneCarrozzeria,

        [Display(Name = "Sostituzione Parabrezza")]
        SostituzioneParabrezza,

        [Display(Name = "Ricarica Climatizzatore")]
        RicaricaClimatizzatore,

        [Display(Name = "Luce Cruscotto")]
        LuceCruscotto,

        [Display(Name = "Cambio Gomme Invernali -> Estive")]
        CambioGommeInvernaliEstive,

        [Display(Name = "Cambio Gomme Estive -> Invernali")]
        CambioGommeEstiveInvernali,


        [Display(Name = "Altra Voce")]
        AltraVoce
    }
}

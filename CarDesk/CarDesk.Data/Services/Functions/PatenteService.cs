using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDesk.Data.Services.Functions
{
    public class PatenteService
    {
        public int CalcolaEta(DateTime nascita, DateTime riferimento)
        {
            int eta = riferimento.Year - nascita.Year;
            if (riferimento < nascita.AddYears(eta)) eta--;
            return eta;
        }

        public (DateTime dataScadenza, bool patenteValida) CalcolaScadenzaConVerifica(DateTime? dataNascita,DateTime? dataRilascio,DateTime? dataUltimoRinnovo = null)
        {
            if (!dataNascita.HasValue || !dataRilascio.HasValue)
                throw new ArgumentNullException("Dati insufficienti per il calcolo");

            // Se abbiamo un rinnovo, partiamo da lì, altrimenti dal rilascio
            DateTime dataPartenza = dataUltimoRinnovo ?? dataRilascio.Value;

            int eta = CalcolaEta(dataNascita.Value, dataPartenza);

            if (eta < 18)
                throw new ArgumentException("L'età del proprietario deve essere almeno 18 anni");

            // Regole anni di validità
            int anniValidita = eta < 50 ? 10 :
                               eta < 70 ? 5 :
                               eta < 80 ? 3 : 2;

            // La scadenza deve essere il giorno del compleanno
            DateTime scadenza = new DateTime(
                dataPartenza.Year + anniValidita,
                dataNascita.Value.Month,
                dataNascita.Value.Day
            );

            // Se la scadenza calcolata è prima della data di partenza, aggiungiamo 1 anno
            if( (scadenza.Month <= dataPartenza.Month) && (scadenza.Day <= dataPartenza.Day ))
                scadenza = scadenza.AddYears(1);

            bool valida = DateTime.Today <= scadenza;

            return (scadenza, valida);
        }
        public DateTime CalcolaScadenza(DateTime? dataNascita, DateTime? dataRilascio)
        {
            if (!dataRilascio.HasValue)
                throw new ArgumentNullException(nameof(dataRilascio), "La data di rilascio non può essere null.");
            if (!dataNascita.HasValue)
                throw new ArgumentNullException(nameof(dataNascita), "La data di nascita non può essere null.");

            int eta = CalcolaEta(dataNascita.Value, dataRilascio.Value);
            if (eta < 18)
                throw new ArgumentException("L'età del proprietario deve essere almeno 18 anni per il rilascio della patente.");


            // Anni di validità in base all'età
            int anniValidita = eta < 50 ? 10 :
                               eta < 70 ? 5 :
                               eta < 80 ? 3 : 2;

            // primaScadenza = rilascio + anni di validità (calcolati in base all'età)
            DateTime primaScadenza = dataRilascio.Value.AddYears(anniValidita);

            // Calcolo scadenza con giorno e mese di nascita
            DateTime scadenzaBase = dataRilascio.Value.AddYears(anniValidita);
            return new DateTime(scadenzaBase.Year, dataNascita.Value.Month, dataNascita.Value.Day);
        }

       
    }
}

using CarDesk.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDesk.Components.Pages.Commons
{
    public class ErrorDialog : IErrorHandlerService
    {
        private readonly IDialogService _dialogService;
        public ErrorDialog(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        public async Task HandleAsync(Exception ex, string? context = null)
        {
            
            // Messaggio predefinito
            string titolo = "Errore";
            string messaggio = "Si è verificato un errore imprevisto.";

            // Gestione di casi noti
            if (ex is DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message?.ToLower() ?? "";

                if (innerMessage.Contains("chiave duplicata") || innerMessage.Contains("indice univoco")
                    || innerMessage.Contains("duplicate") || innerMessage.Contains("unique constraint") || innerMessage.Contains("unique index"))
                {
                    messaggio = "Impossibile completare l'operazione: il dato esiste già.";
                }
                else if (innerMessage.Contains("foreign key"))
                {
                    messaggio = "Impossibile eliminare il dato perché è collegato ad altri dati.";
                }
            }

            // Mostra il dialog
            await _dialogService.ShowMessageBox(
                titolo,
                ex.Message,
                yesText: "Ok"
            );
        }
    }
    public interface IErrorHandlerService
    {
        Task HandleAsync(Exception ex, string? context = null);
    }
}

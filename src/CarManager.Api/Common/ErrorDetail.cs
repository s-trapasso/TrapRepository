using CarManager.Core.Enums;

namespace CarManager.Api.Common
{
    public class ErrorDetail
    {
        public ErrorCode Code { get; init; }
        public string Message { get; init; } = string.Empty;
        public int HttpStatus { get; init; }

        public static ErrorDetail From(ErrorCode code)
        {
            return code switch
            {
                ErrorCode.NotFound => new ErrorDetail
                {
                    Code = code,
                    Message = "Risorsa non trovata",
                    HttpStatus = 404
                },

                ErrorCode.DuplicatePlate => new ErrorDetail
                {
                    Code = code,
                    Message = "Targa già esistente",
                    HttpStatus = 400
                },

                ErrorCode.VehicleNotFound => new ErrorDetail
                {
                    Code = code,
                    Message = "Veicolo non trovato",
                    HttpStatus = 404
                },

                ErrorCode.ValidationError => new ErrorDetail
                {
                    Code = code,
                    Message = "Errore di validazione",
                    HttpStatus = 400
                },

                _ => new ErrorDetail
                {
                    Code = code,
                    Message = "Errore interno",
                    HttpStatus = 500
                }
            };
        }
    }
}

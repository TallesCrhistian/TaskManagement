using System.Net;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Exceptions
{
    public class CatchFunctions
    {
        public static ServiceResponseDTO<TEntity> ServiceResponse<TException, TEntity>(TException ex, HttpStatusCode statusCode) where TException : Exception
        {
            ServiceResponseDTO<TEntity> serviceResponseDTO = new ServiceResponseDTO<TEntity>();

            serviceResponseDTO.Sucess = false;
            serviceResponseDTO.Message = ex.Message;
            serviceResponseDTO.StatusCode = (int)statusCode;

            return serviceResponseDTO;
        }
    }
}

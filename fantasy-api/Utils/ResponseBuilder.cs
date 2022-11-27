using FantasyApi.Data.Base.Dtos;
using System.Collections.Generic;

namespace FantasyApi.Utils
{
    public static class ResponseBuilder
    {
        public static ErrorDto ErrorResponse(string errorCode)
        {
            return new ErrorDto()
            {
                Success = false,
                Code = errorCode,
            };
        }

        public static PaginatedListDto<T> PaginatedListResponse<T>(IEnumerable<T> list = null, int totalRows = 0, int page = 0, int totalPages = 0, int size = 0, bool success = true)
        {
            return new PaginatedListDto<T>()
            {
                Success = success,
                Paginate = new Paginate
                {
                    Total = totalRows,
                    Page = page,
                    Pages = totalPages,
                    PerPage = size,
                },
                Items = list,
            };
        }

        public static CreationDto<T> CreationResponse<T>(string message, T item, bool success = true)
        {
            return new CreationDto<T>
            {
                Success = success,
                Message = message,
                Item = item,
            };
        }

        public static DeletionDto GeneralResponse(string message, bool success = true)
        {
            return new DeletionDto()
            {
                Success = success,
                Message = message,
            };
        }
    }
}

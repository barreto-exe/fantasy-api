using FantasyApi.Data.Base.Dtos;
using System.Collections.Generic;

namespace FantasyApi.Utils
{
    public static class ResponsesBuilder
    {
        public static ErrorDto ErrorResponse(string errorCode)
        {
            return new ErrorDto()
            {
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

        public static CreationDto<T> CreationResponse<T>(string message, T item)
        {
            return new CreationDto<T>
            {
                Message = message,
                Item = item,
            };
        }

        public static DeletionDto DeletionResponse(string message, bool success = true)
        {
            return new DeletionDto()
            {
                Success = success,
                Message = message,
            };
        }
    }
}

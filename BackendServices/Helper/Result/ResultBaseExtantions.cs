namespace BackendServices.Helper.Result
{
    public static class ResultBaseExtantions
    {
        public static TResult SetError<TResult>(this TResult finalResult, string resultCode, string resultMessage)
            where TResult : ResultBase
        {
            finalResult.ResultCode = resultCode;
            finalResult.ResultMessage = resultMessage;
            return finalResult;
        }

        public static TResult SetError<TResult>(this TResult finalResult, ResultBase resultBase)
            where TResult : ResultBase
        {
            finalResult.ResultCode = resultBase.ResultCode;
            finalResult.ResultMessage = resultBase.ResultMessage;
            return finalResult;
        }

        public static TResult SetNotFound<TResult>(this TResult finalResult, string? resultMessage = null)
            where TResult : ResultBase
        {
            finalResult.ResultCode = ResultCodes.NotFound;
            finalResult.ResultMessage = string.IsNullOrWhiteSpace(resultMessage)
                ? "Data not found."
                : resultMessage;

            return finalResult;
        }

        public static TResult SetBadRequest<TResult>(this TResult finalResult, string resultMessage)
            where TResult : ResultBase
        {
            finalResult.ResultCode = ResultCodes.BadRequest;
            finalResult.ResultMessage = resultMessage;
            return finalResult;
        }

        public static TResult SetException<TResult>(this TResult finalResult, Exception ex)
            where TResult : ResultBase
        {
            var inner = ex.InnerException?.Message ?? string.Empty;
            finalResult.ResultCode = ResultCodes.Exception;
            finalResult.ResultMessage = (ex.Message ?? string.Empty) + " " + inner;
            return finalResult;
        }

        // =====================
        //  PAGINATION HELPER
        // =====================

        public static PaginatedResult<T>.PaginationMeta SetPagination<T>(
            this IQueryable<T> query,
            int page,
            int size)
        {
            var total = query.LongCount();

            return new PaginatedResult<T>.PaginationMeta
            {
                Page = page,
                Size = size,
                Total = total,
                TotalPage = (int)Math.Ceiling((double)total / size)
            };
        }

        public static PaginatedResult<T>.PaginationMeta SetPagination<T>(
            long total,
            int page,
            int size)
        {
            return new PaginatedResult<T>.PaginationMeta
            {
                Page = page,
                Size = size,
                Total = total,
                TotalPage = (int)Math.Ceiling((double)total / size)
            };
        }

        // Versi yang langsung mengisi ke objek result
        public static PaginatedResult<T> WithPagination<T>(
            this PaginatedResult<T> result,
            int page,
            int size,
            long total)
        {
            result.Pagination = new PaginatedResult<T>.PaginationMeta
            {
                Page = page,
                Size = size,
                Total = total,
                TotalPage = (int)Math.Ceiling((double)total / size)
            };

            result.ResultCountData = (int?)total;
            return result;
        }
    }
}

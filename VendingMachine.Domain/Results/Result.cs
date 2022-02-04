using System;

namespace VendingMachine.Domain.Results
{
    public class Result
    {
        public String? Error { get; }
        public Boolean IsSuccess => Error is null;

        public Result(String? error)
        {
            Error = error;
        }

        public static Result Fail(String error) => new Result(error);

        public static Result Success() => new Result(null);
    }
}

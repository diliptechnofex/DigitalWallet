using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DigitalWallet.Modules.Wallets.Application.Results
{
    public class Result
    {
        protected Result(bool isSuccess, IReadOnlyCollection<Error> errors)
        {
            if (isSuccess && errors.Count > 0)
            {
                throw new InvalidOperationException("Successful result cannot contain errors.");
            }

            if (!isSuccess && errors.Count == 0)
            {
                throw new InvalidOperationException("Failed result must contain at least one error.");
            }

            IsSuccess = isSuccess;
            Errors = errors;
        }
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public IReadOnlyCollection<Error> Errors { get; }
    }

    public sealed class Result<TValue> : Result, IResultFactory<Result<TValue>>
    {
        private readonly TValue? _value;

        private Result(TValue value) : base(true, Array.Empty<Error>())
        {
            ArgumentNullException.ThrowIfNull(value);
            _value = value;
        }

        private Result(IReadOnlyCollection<Error> errors) : base(false, errors)
        {
        }

        public TValue Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException(
                        "Cannot access the value of a failed result.");
                }

                return _value!;
            }
        }

        public static Result<TValue> Success(TValue value)
        {
            return new Result<TValue>(value);
        }

        public static Result<TValue> Failure(IReadOnlyCollection<Error> errors)
        {
            return new Result<TValue>(errors);
        }

        public static Result<TValue> Failure(params Error[] errors)
        {
            return new Result<TValue>(errors);
        }
    }
}

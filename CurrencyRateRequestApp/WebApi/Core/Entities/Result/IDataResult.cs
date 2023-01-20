﻿namespace WebApi.Core.Entities.Result
{
    public interface IDataResult<out T>:IResult
    {
        T Data { get; }
    }
}

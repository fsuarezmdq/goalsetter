﻿using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Goalsetter.AppServices.Utils
{
    public sealed class Messages : IMessages
    {
        private readonly IServiceProvider _provider;

        public Messages(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task<Result> Dispatch(ICommand command)
        {
            Type type = typeof(ICommandHandler<>);
            Type[] typeArgs = { command.GetType() };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _provider.GetService(handlerType);
            Task<Result> result = handler?.Handle((dynamic)command);

            return result;
        }

        public T Dispatch<T>(IQuery<T> query) where T : class
        {
            Type type = typeof(IQueryHandler<,>);
            Type[] typeArgs = { query.GetType(), typeof(T) };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _provider.GetService(handlerType);
            T result = handler?.Handle((dynamic)query);

            return result;
        }
    }
}

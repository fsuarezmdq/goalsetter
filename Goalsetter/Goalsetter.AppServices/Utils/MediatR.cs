using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Goalsetter.AppServices.Utils
{
    public class MediatR : IMessages
    {
        public Task<Result> Dispatch(ICommand command)
        {
            throw new NotImplementedException();
        }

        public T Dispatch<T>(IQuery<T> query) where T : class
        {
            throw new NotImplementedException();
        }
    }
}

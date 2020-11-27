using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Utils
{
    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}

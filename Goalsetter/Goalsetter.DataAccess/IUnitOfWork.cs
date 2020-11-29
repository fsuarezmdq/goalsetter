using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goalsetter.Domains;

namespace Goalsetter.DataAccess
{
    public interface IUnitOfWork
    {
        IQueryable<T> GetAsync<T>(Guid guid) where T : AggregateRoot;
        IQueryable<T> Query<T>() where T : AggregateRoot;
        void Update<T>(T entity);
        void Add<T>(T entity);
        Task Commit();
    }
}

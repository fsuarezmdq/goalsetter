using System;
using System.Threading.Tasks;

namespace Goalsetter.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        AppContext AppContext { get; }
        Task Commit();
    }
}

using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Goalsetter.AppServices.Utils
{
    public interface IMessages
    {
        Task<Result> Dispatch(ICommand command);
        T Dispatch<T>(IQuery<T> query) where T : class;
    }
}
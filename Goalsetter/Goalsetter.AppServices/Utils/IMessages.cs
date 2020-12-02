using CSharpFunctionalExtensions;
using Goalsetter.AppServices.Utils;
using System.Threading.Tasks;

namespace Goalsetter.AppServices
{
    public interface IMessages
    {
        Task<Result> Dispatch(ICommand command);
        T Dispatch<T>(IQuery<T> query) where T : class;
    }
}
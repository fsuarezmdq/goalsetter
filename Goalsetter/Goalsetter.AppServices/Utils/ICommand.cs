using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace Goalsetter.AppServices.Utils
{
    public interface ICommand
    {
    }

    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<Result> Handle(TCommand command);
    }
}

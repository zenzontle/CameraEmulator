using System.Windows.Input;
using Catel.MVVM;

namespace CameraEmulator.UI.ViewModels
{
    public interface ICommandViewModel<out TCommand> : IViewModel where TCommand : ICommand
    {
        TCommand SaveChanges { get; }
        TCommand ApplyChanges { get; }
        TCommand CancelChanges { get; }
    }

    public interface ITaskCommandViewModel : ICommandViewModel<TaskCommand>
    {
    }

    public interface ITaskCommandViewModel<TResult> : ICommandViewModel<TaskCommand<TResult>>
    {
    }
}

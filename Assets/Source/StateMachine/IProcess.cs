using System.Diagnostics;

namespace StateMachineLogic
{
    public interface IProcess : IState
    {
        ProcessStatus CurrentStatus { get; }
    }
}
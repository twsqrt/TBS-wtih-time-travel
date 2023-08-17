namespace StateMachineLogic
{
    public interface ITransition
    {
        bool TryTransition(out IState state);
    }
    
}
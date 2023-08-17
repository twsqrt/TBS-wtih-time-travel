namespace StateMachineLogic
{
    public interface IState 
    {
        public abstract void Enter();

        public abstract void Exit();
    }
}
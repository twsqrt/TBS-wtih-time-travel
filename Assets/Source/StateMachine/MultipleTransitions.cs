using System.Collections.Generic;

namespace StateMachineLogic
{
    public class MultipleTransitions : ITransition
    {
        private readonly IEnumerable<ITransition> _transitions;

        public MultipleTransitions(IEnumerable<ITransition> transitions)
        {
            _transitions = transitions;
        }

        public bool TryTransition(out IState state)
        {
            foreach(ITransition transition in _transitions)
            {
                if(transition.TryTransition(out state))
                    return true;
            }

            state = null;
            return false;
        }
    }
}
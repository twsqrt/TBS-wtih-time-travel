using System.Collections.Generic;
using System;

namespace StateMachineLogic
{
    public class Transition : ITransition
    {
        private readonly Func<bool> _condition;
        private readonly IState _next;

        public static Transition Always(IState next) => new Transition(next, () => true);

        public Transition(IState next, Func<bool> condition)
        {
            _condition = condition;
            _next = next;
        }

        public bool TryTransition(out IState next)
        {
            if(_condition() == false)
            {
                next = null;
                return false;
            }

            next = _next;
            return true;
        }
    }

}
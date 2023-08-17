using System;
using System.Linq;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.ShaderKeywordFilter;

namespace StateMachineLogic
{
    public class StateMachine : IState
    {
        private readonly Dictionary<IState, ITransition> _transitions;
        private readonly IState _start;
        private readonly IEnumerable<IState> _ends;

        private IState _current;

        public IState Current => _current;

        public StateMachine(IState start, IEnumerable<IState> ends, Dictionary<IState, ITransition> transitions)
        {
            _start = start;
            _ends = ends;
            _transitions = transitions;

            _current = null;
        }

        public void Enter()
        {
            _current = _start;
            _current.Enter();
        }

        public bool TryNext()
        {
            if(_transitions[_current].TryTransition(out IState next))
            {
                _current.Exit();
                _current = next;
                next.Enter();
                return true;
            }

            return false;
         }

        public void Exit()
        {
            _current?.Exit();
            _current = null;
        }
    }
}
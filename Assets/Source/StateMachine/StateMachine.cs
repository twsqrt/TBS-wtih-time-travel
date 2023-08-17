using System;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace StateMachineLogic
{
    public class StateMachine : IState
    {
        private readonly Dictionary<IState, ITransition> _transitions;
        private readonly IState _start;
        private readonly IEnumerable<IState> _ends;

        private IState _current;
        private bool _isComleted;

        public IState Current => _current;
        public bool IsCompleted => _isComleted;


        public event Action<IState> OnComplition;

        public StateMachine(IState start, IEnumerable<IState> ends, Dictionary<IState, ITransition> transitions)
        {
            _start = start;
            _ends = ends;
            _transitions = transitions;

            Reset();
        }

        private void Reset()
        {
            _current = null;
            _isComleted = false;
        }

        public void Enter()
        {
            _current = _start;
            _current.Enter();
        }

        public bool TryNext()
        {
            if(_isComleted)
                return false;

            if(_transitions[_current].TryTransition(out IState next))
            {
                _current.Exit();
                _current = next;
                next.Enter();

                if(_ends.Contains(next))
                {
                    OnComplition?.Invoke(next);
                    _isComleted = true;
                }

                return true;
            }

            return false;
         }

        public void Exit()
        {
            _current?.Exit();
            Reset();
        }
    }
}
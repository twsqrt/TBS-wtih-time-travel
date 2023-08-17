using System.Linq;
using System.Collections.Generic;
using System;

namespace StateMachineLogic.Builder
{
    public class LinearStateMachineBuilder<TState> : IStateMachineBuilder where TState : IState
    {
        private readonly Func<TState, bool> _previouseCondition;
        private readonly Func<TState, bool> _nextCondition;

        private List<TState> _states; 

        public LinearStateMachineBuilder(Func<TState, bool> previouseCondition, Func<TState, bool> nextCondition)
        {
            _previouseCondition = previouseCondition;
            _nextCondition = nextCondition;

            _states = new List<TState>();
        }

        private ITransition NextTransition(TState from, IState to)
            => new Transition(to, () => _nextCondition(from));

        private ITransition PreviouseTransition(TState from, IState to)
            => new Transition(to, () => _previouseCondition(from));

        public LinearStateMachineBuilder<TState> StartBuilding()
        {
            _states.Clear();

            return this;
        }

        public LinearStateMachineBuilder<TState> Register(TState state)
        {
            _states.Add(state);

            return this;
        }

        public LinearStateMachineBuilder<TState> Register(TState[] states)
        {
            _states.AddRange(states);

            return this;
        }


        public StateMachine Build()
        {
            if(_states.Count() < 2)
                throw new ArgumentException("At least two states must be registered!");

            TState[] statesArray = _states.ToArray();
            var transitions = new Dictionary<IState, ITransition>();

            TState start = statesArray[0];
            transitions.Add(start, NextTransition(start, statesArray[1]));

            for(int i = 1; i < statesArray.Length - 1; i++)
            {
                TState current = statesArray[i];

                ITransition previouse = PreviouseTransition(current, statesArray[i - 1]);
                ITransition next = NextTransition(current, statesArray[i + 1]);

                transitions.Add(current, new MultipleTransitions( new[]{previouse, next} ));
            }

            IState end = statesArray.Last();

            return new StateMachine(start, new[]{end}, transitions);
        }
    }
}
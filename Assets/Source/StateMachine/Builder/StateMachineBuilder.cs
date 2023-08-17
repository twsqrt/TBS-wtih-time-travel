using System;
using System.Linq;
using System.Collections.Generic;

namespace StateMachineLogic.Builder
{
    public class StateMachineBuilder : IStateMachineBuilder
    {
        private IState _start;
        private List<IState> _ends;
        private List<(IState, ITransition)> _registeredTransitions;
        private List<Action<Action<IState>>> _completedEvents;

        public StateMachineBuilder()
        {
            _registeredTransitions = new List<(IState, ITransition)>();
            _ends = new List<IState>();
            _completedEvents = new List<Action<Action<IState>>>();

            _start = null;
        }

        private ITransition MergeTransitions(IEnumerable<ITransition> transitions)
        {
            if(transitions.Count() > 1)
                return new MultipleTransitions(transitions);

            return transitions.First();
        }

        public StateMachineBuilder StartBuilding(IState start)
        {
            _start = start;
            _ends.Clear();
            _registeredTransitions.Clear();
            _completedEvents.Clear();

            return this;
        }

        public StateMachineBuilder Register(IState from, IState to)
        {
            _registeredTransitions.Add((from, Transition.Always(to)));

            return this;
        }

        public StateMachineBuilder Register(IState from, IState to, Func<bool> condition) 
        {
            var transition = new Transition(to, condition);
            _registeredTransitions.Add((from, transition));

            return this; 
        }

        public StateMachineBuilder Register<TState>(TState from, IState to, Func<TState, bool> condition) 
            where TState : IState
        {
            var transition = new Transition(to, () => condition(from));
            _registeredTransitions.Add((from, transition));

            return this; 
        }

        public StateMachineBuilder RegisterAfterStateMachineCompleted(StateMachine from, IState to)
        {
            _completedEvents.Add(handler => from.OnComplition += handler);

            return this;
        }

        public StateMachine Build()
        {
            Dictionary<IState, ITransition> transitions = _registeredTransitions
                .GroupBy(rt => rt.Item1, rt => rt.Item2)
                .ToDictionary(g => g.Key, MergeTransitions);

            return new StateMachine(_start, _ends, transitions);
        }
    }
}
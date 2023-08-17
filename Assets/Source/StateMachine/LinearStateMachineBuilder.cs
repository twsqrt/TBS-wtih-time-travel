using System.Linq;
using System.Collections.Generic;
using System;

namespace StateMachineLogic
{
    public class LinearStateMachineBuilder : IStateMachineBuilder
    {
        private IProcess[] _processes;

        public LinearStateMachineBuilder(IProcess[] processes)
        {
            if(processes.Length < 2)
                throw new ArgumentException("There must be at least two processes in the array!");

            _processes = processes;
        }

        private Transition TransitionByStatus(IProcess from, IState to, ProcessStatus status)
             => new Transition(to, () => from.CurrentStatus == status);

        public StateMachine Build()
        {
            var transitions = new Dictionary<IState, ITransition>();

            IProcess start = _processes.First();
            Transition startTransition = TransitionByStatus(start, _processes[1], ProcessStatus.READY_TO_EXIT);

            transitions.Add(start, startTransition);

            for(int i = 1; i < _processes.Length - 1; i++)
            {
                IProcess current = _processes[i];

                Transition previouse = TransitionByStatus(current, _processes[i - 1], ProcessStatus.CANCELED);
                Transition next = TransitionByStatus(current, _processes[i + 1], ProcessStatus.READY_TO_EXIT); 
                var transition = new MultipleTransitions( new[]{previouse, next} );

                transitions.Add(current, transition);
            }

            IState end = _processes.Last();

            return new StateMachine(start, new[]{end}, transitions);
        }
    }
}
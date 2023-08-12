using System;
using Model.GameStateLogic;
using StateMachine;

namespace Model.MoveLogic
{
    public class ArgumentSelector<T> : IState
    {
        private readonly Func<Func<T, bool>> _ruleFactory;
        private T _result;
        private bool _isResultSelected;

        public event Action<Func<T, bool>> OnStateEnter;
        public event Action<T> OnStateExit;

        public T Result 
        {
            get
            {
                if(_isResultSelected == false)
                    throw new InvalidOperationException("Argument is not selected!");

                return _result;
            }
        }

        public ArgumentSelector(Func<Func<T, bool>> ruleFactory)
        {
            _ruleFactory = ruleFactory;
            _isResultSelected = false;
        }

        public ArgumentSelector(Func<T, bool> rule)
        {
            _ruleFactory = () => rule;
            _isResultSelected = false;
        }

        public void Enter()
        {
            _isResultSelected = false;

            Func<T, bool> rule = _ruleFactory();
            OnStateEnter?.Invoke(rule);
        }

        public void Exit()
        {
            _isResultSelected = true;
            OnStateExit?.Invoke(_result);
        }
    }
}
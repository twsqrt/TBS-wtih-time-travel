using System;

namespace Model.GameStateLogic.MapLogic
{
    public class MapTile
    {
        private bool _state = true;
        public event Action<bool> OnStateChanged;
        public bool State
        {
            get => _state;
            set
            {
                _state = value;
                OnStateChanged?.Invoke(value);
            }
        }
    }
}

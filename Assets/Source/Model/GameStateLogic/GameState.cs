using Model.GameStateLogic.MapLogic;

namespace Model.GameStateLogic
{
    public class GameState
    {
        private readonly Map _map;

        public Map Map => _map;

        public GameState(Map map)
        {
            _map = map;
        }
    }
}
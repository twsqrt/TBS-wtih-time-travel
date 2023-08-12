using Model.GameStateLogic.MapLogic;

namespace Presenter.GameStatePresenter.MapPresenter
{
    public class MapPresenter
    {
        private Map _map;

        public MapPresenter(Map map)
        {
            _map = map;
        }

        public void ChangeState(int x, int y)
        {
            MapTile tile = _map[x, y];
            tile.State = ! tile.State;
        }
    }
}
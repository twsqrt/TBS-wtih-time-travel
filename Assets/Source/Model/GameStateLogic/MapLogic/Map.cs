using Config;

namespace Model.GameStateLogic.MapLogic
{
    public class Map
    {
        private readonly int _width;
        private readonly int _height;
        private readonly MapTile[] _tiles;

        public MapTile this[int x, int y]
        {
            get => _tiles[x + _width * y];
            private set => _tiles[x + _width * y] = value;
        }
        
        public int Width => _width;
        public int Height => _height;

        private MapTile Generator(int x, int y) => new();

        public Map(MapConfig config)
        {
            _width = config.Width;
            _height = config.Height;
            _tiles = new MapTile[_width * _height];

            for(int i =0; i < _width; i++)
            {
                for(int j =0; j < _height; j++)
                    this[i, j] = Generator(i, j);
            }
        }
    }
}

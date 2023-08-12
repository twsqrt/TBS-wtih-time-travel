using UnityEngine;
using Presenter.GameStatePresenter.MapPresenter;
using Model.GameStateLogic.MapLogic;

namespace View.GameStateView.MapView
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private MeshCollider _quadCollider;
        [SerializeField] private Transform _tilesContainer;
        [SerializeField] private float _spacing;
        [SerializeField] private MapTileView _prefab;

        private const int MAP_COLLIDER_LAYER = 1 << 6;

        private MapPresenter _presenter;
        private MapTileView[] _tiles;
        private int _width;
        private int _height;
        private Vector3 _positionOffset;

        public int Width => _width;
        public int Height => _height;

        private void Refresh(Map map)
        {
            Cleanup();
            Render(map);
        }

        private void Render(Map map)
        {
            for(int i =0; i < _width; i++)
            {
                for(int j =0; j < _height; j++)
                {
                    MapTileView view = Instantiate(_prefab, _tilesContainer);

                    view.name = $"{i}, {j}";
                    view.Init(map[i, j]);
                    view.transform.localPosition = new Vector3(i, 0f, j) * _spacing - _positionOffset;

                    this[i, j] = view;
                }
            }
        }

        private void Cleanup()
        {
            foreach(MapTileView tile in _tiles)
                Destroy(tile.gameObject);
        }

        private void Update()
        {
            if(Input.GetMouseButtonUp(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 50f, MAP_COLLIDER_LAYER))
                {
                    Vector3 quadPosition = _quadCollider.transform.position;
                    Vector3 pointOnQuad = hit.point - quadPosition + new Vector3(_width, 0f, _height) * _spacing * 0.5f;

                    int x = (int)(pointOnQuad.x / _spacing);
                    int y = (int)(pointOnQuad.z / _spacing);

                    _presenter.ChangeState(x, y);
                }
            }
        }

        public MapTileView this[int x, int y] 
        {
            get => _tiles[x + _width * y];
            private set
            {
                _tiles[x + _width * y] = value;
            }
        }

        public bool TryGetPosition(Ray ray, out Vector2Int position)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 50f, MAP_COLLIDER_LAYER))
            {
                Vector3 quadPosition = _quadCollider.transform.position;
                Vector3 pointOnQuad = hit.point - quadPosition + new Vector3(_width, 0f, _height) * _spacing * 0.5f;

                int x = (int)(pointOnQuad.x / _spacing);
                int y = (int)(pointOnQuad.z / _spacing);

                position = new Vector2Int(x, y);
                return true;
            }

            position = Vector2Int.zero;
            return false;
        }

        public void Init(MapPresenter presenter, Map map)
        {
            _presenter = presenter;

            _width = map.Width;
            _height = map.Height;
            _tiles = new MapTileView[_width * _height];

            _positionOffset = new Vector3(_width - 1f, 0f, _height - 1f) * _spacing * 0.5f;
            _quadCollider.transform.localScale = new Vector3(_width * _spacing, _height * _spacing, 1f);

            Render(map);
        }
    }
}

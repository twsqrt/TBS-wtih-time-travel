using UnityEngine;
using Model.GameStateLogic.MapLogic;
using View.AgrumentSelectorView;

namespace View.GameStateView.MapView
{
    public class MapTileView : MonoBehaviour
    {
        [SerializeField] private Color _trueStateColor;
        [SerializeField] private Color _falseStateColor;
        [SerializeField] private MeshRenderer _render;
        [SerializeField] private Highlighter _highlighter;

        public Highlighter Highlighter => _highlighter;

        public void Init(MapTile tile)
        {
            tile.OnStateChanged += StateChangeHandler;
        }

        private void StateChangeHandler(bool state)
        {
            Color newColor = state ? _trueStateColor : _falseStateColor;
            _render.material.SetColor("_Color", newColor);
        }
    }
}
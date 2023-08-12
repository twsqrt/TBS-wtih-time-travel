using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View.GameStateView.MapView;
using Presenter.AgrumentSelectorPresentor;
using Model.MoveLogic;

namespace View.AgrumentSelectorView
{
    public class PositionSelectorView : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _highlightersContainer;

        private PositionSelectorPresenter _presenter;
        private MapView _mapView;
        private IEnumerable<Highlighter> _activeHighlighters;
        private IEnumerable<Vector2Int> _allPosition;

        private void Update()
        {
            if(Input.GetMouseButtonUp(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if(_mapView.TryGetPosition(ray, out Vector2Int position))
                {

                }
            }
        }


        public void Init(PositionSelectorPresenter presenter, MapView mapView)
        {
            _presenter = presenter;
            _mapView = mapView;
            _activeHighlighters = Enumerable.Empty<Highlighter>();

            var widthRange = Enumerable.Range(0, _mapView.Width);
            var heightRange = Enumerable.Range(0, _mapView.Height);
            _allPosition = widthRange.SelectMany( _ => heightRange, (x, y) => new Vector2Int(x, y));

            //positionSelector.OnStateEnter += HighlightCorrectPositions;
            //positionSelector.OnStateExit += _ => HighlightDisable();
            
        }

        public void HighlightCorrectPositions(Func<Vector2Int, bool> rule)
        {
            _activeHighlighters = _allPosition.Where(rule).Select(v => _mapView[v.x, v.y].Highlighter);
            foreach(Highlighter highlighter in _activeHighlighters)
                highlighter.HighlightEnable();
        }

        public void HighlightDisable()
        {
            foreach(Highlighter highlighter in _activeHighlighters)
                highlighter.HighlightDisable();
        }
    }
}
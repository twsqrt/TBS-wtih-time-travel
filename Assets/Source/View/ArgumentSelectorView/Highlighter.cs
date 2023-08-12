using UnityEngine;

namespace View.AgrumentSelectorView
{
    public class Highlighter : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _render;
        [SerializeField] private Color _highlightColor;
        [Range(0f, 1f)] private float _additionCoeff;

        private Color _defaultColor;

        public void Init()
        {
            _defaultColor = _render.material.color;
        }

        public void HighlightEnable()
        {
            _defaultColor = _render.material.color;
            Color newColor = Color.Lerp(_defaultColor, _highlightColor, _additionCoeff);
            _render.material.SetColor("_Color", newColor);
        }

        public void HighlightDisable()
        {
            _render.material.SetColor("_Color", _defaultColor);
        }
    }
}
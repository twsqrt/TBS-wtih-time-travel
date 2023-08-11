using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Map Config", menuName = "Config/Map", order = 51)]
    public class MapConfig : ScriptableObject
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        public int Width => _width;
        public int Height => _height;
    }
}
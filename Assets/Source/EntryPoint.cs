using UnityEngine;
using Config;
using Model.GameStateLogic.MapLogic;
using View.GameStateView.MapView;
using Presenter.GameStatePresenter.MapPresenter;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private MapConfig _mapConfig;
    [SerializeField] private MapView _mapView;

    private void Awake()
    {
        var map = new Map(_mapConfig);
        var mapPresenter = new MapPresenter(map); 
        _mapView.Init(mapPresenter, map);
    }
}
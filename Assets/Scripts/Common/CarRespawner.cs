using Race;
using System;
using UnityEngine;

public class CarRespawner : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>, IDependency<CarInputControl>
{
    [SerializeField] private float _respawnHeight;

    private TrackPoint _respawnerPoint;

    private RaceStateTracker _raceStateTracker;
    public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;

    private Car _car;
    public void Construct(Car obj) => _car = obj;

    private CarInputControl _carInputControl;
    public void Construct(CarInputControl obj) => _carInputControl = obj;

    private void Start()
    {
        _raceStateTracker.eventTrackPointPassed += OnTrackPointPassed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) == true)
            Respawn();
    }

    private void OnDestroy()
    {
        _raceStateTracker.eventTrackPointPassed -= OnTrackPointPassed;
    }

    private void OnTrackPointPassed(TrackPoint point)
    {
        _respawnerPoint = point;
    }
    public void Respawn()
    { 
       if(_respawnerPoint == null) return;

        if (_raceStateTracker.State != RaceState.Race) return;

        _car.Respawn(_respawnerPoint.transform.position + _respawnerPoint.transform.up * _respawnHeight, _respawnerPoint.transform.rotation);

        _carInputControl.Reset();
    }
}

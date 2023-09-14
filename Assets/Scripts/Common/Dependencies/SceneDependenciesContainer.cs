using UnityEngine;

namespace Race
{
    public class SceneDependenciesContainer : Dependency
    {
        [SerializeField] private RaceStateTracker _stateTracker;
        [SerializeField] private CarInputControl _carInputControl;
        [SerializeField] private Car _car;
        [SerializeField] private TrackPointCircuit _trackPointCircuit;
        [SerializeField] private CarCameraController _carCameraController;
        [SerializeField] private RaceTimerTracker _raceTimerTracker;
        [SerializeField] private RaceResultTime _raceResultTime;

        protected override void BindAll(MonoBehaviour monoBehaviourScene)
        {
            Bind<RaceStateTracker>(_stateTracker, monoBehaviourScene);
            Bind<CarInputControl>(_carInputControl, monoBehaviourScene);
            Bind<Car>(_car, monoBehaviourScene);
            Bind<TrackPointCircuit>(_trackPointCircuit, monoBehaviourScene);
            Bind<CarCameraController>(_carCameraController, monoBehaviourScene);
            Bind<RaceTimerTracker>(_raceTimerTracker, monoBehaviourScene);
            Bind<RaceResultTime>(_raceResultTime, monoBehaviourScene);

        }

        private void Awake()
        {
            FindAllObjectToBind();
        }
    }
}


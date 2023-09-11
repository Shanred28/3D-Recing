using UnityEngine;

namespace Race
{
    public interface IDependency<T>
    {
        void Construct(T obj);
    }

    public class SceneDependenc : MonoBehaviour
    {
        [SerializeField] private RaceStateTracker _stateTracker;
        [SerializeField] private CarInputControl _carInputControl;
        [SerializeField] private Car _car;
        [SerializeField] private TrackPointCircuit _trackPointCircuit;
        [SerializeField] private CarCameraController _carCameraController;
        [SerializeField] private RaceTimerTracker _raceTimerTracker;
        [SerializeField] private RaceResultTime _raceResultTime;
        private void Bind(MonoBehaviour mono)
        {
            if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>).Construct(_stateTracker);
            if (mono is IDependency<CarInputControl>) (mono as IDependency<CarInputControl>).Construct(_carInputControl);
            if (mono is IDependency<Car>) (mono as IDependency<Car>).Construct(_car);
            if (mono is IDependency<TrackPointCircuit>) (mono as IDependency<TrackPointCircuit>).Construct(_trackPointCircuit);
            if (mono is IDependency<CarCameraController>) (mono as IDependency<CarCameraController>).Construct(_carCameraController);
            if (mono is IDependency<RaceTimerTracker>) (mono as IDependency<RaceTimerTracker>).Construct(_raceTimerTracker);
            if (mono is IDependency<RaceResultTime>) (mono as IDependency<RaceResultTime>).Construct(_raceResultTime);
        }

        private void Awake()
        {
            MonoBehaviour[] allMonoBehaviourInScene = FindObjectsOfType<MonoBehaviour>();

            for (int i = 0; i < allMonoBehaviourInScene.Length; i++)
            {
                Bind(allMonoBehaviourInScene[i]);

            }
        }
    }
}


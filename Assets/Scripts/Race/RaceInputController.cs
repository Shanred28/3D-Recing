using UnityEngine;

namespace Race
{
    public class RaceInputController : MonoBehaviour, IDependency<CarInputControl>, IDependency<RaceStateTracker>
    {
        private CarInputControl _carInputControl;
        public void Construct(CarInputControl obj) => _carInputControl = obj;

        private RaceStateTracker _raceStateTracker;
        public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;

        private void Start () 
        {
            _raceStateTracker.eventStarted += OnRaceStarted;
            _raceStateTracker.eventCompleted += OnRaceFinished;
            _carInputControl.enabled = false;
        }

        private void OnDestroy()
        {
            _raceStateTracker.eventStarted -= OnRaceStarted;
            _raceStateTracker.eventCompleted -= OnRaceFinished;
        }

        private void OnRaceStarted()
        {
             _carInputControl.enabled = true;
        }

        private void OnRaceFinished()
        {
            _carInputControl.enabled = false;
            _carInputControl.Stop();
        }
    }
}


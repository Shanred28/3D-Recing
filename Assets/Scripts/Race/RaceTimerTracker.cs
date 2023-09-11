using UnityEngine;

namespace Race
{
    public class RaceTimerTracker : MonoBehaviour, IDependency<RaceStateTracker>
    {
        private RaceStateTracker _raceStateTracker;
        public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;

        private float _currentTime;
        public float CurrentTime => _currentTime;

        private void Start()
        {
            _raceStateTracker.eventStarted += OnRaceStarted;
            _raceStateTracker.eventCompleted += OnRaceCompleted;

            enabled = false;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
        }

        private void OnDestroy()
        {
            _raceStateTracker.eventStarted -= OnRaceStarted;
            _raceStateTracker.eventCompleted -= OnRaceCompleted;
        }

        private void OnRaceStarted()
        {
            enabled = true;
            _currentTime = 0;
        }

        private void OnRaceCompleted()
        {
            enabled = false;
        }
    }
}


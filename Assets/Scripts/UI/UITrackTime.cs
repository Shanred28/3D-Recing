using TMPro;
using UnityEngine;

namespace Race
{
    public class UITrackTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimerTracker>
    {
        [SerializeField] private TMP_Text _textTimer;

        private RaceStateTracker _raceStateTracker;
        public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;

        private RaceTimerTracker _raceTimerTracker;
        public void Construct(RaceTimerTracker obj) => _raceTimerTracker = obj;

        private void Start()
        {
            _raceStateTracker.eventStarted += OnRaceStarted;
            _raceStateTracker.eventCompleted += OnRaceCompleted;

            _textTimer.enabled = false;
        }

        private void Update()
        {
            _textTimer.text = StringTime.SecondToTimeString(_raceTimerTracker.CurrentTime);
        }

        private void OnDestroy()
        {
            _raceStateTracker.eventStarted -= OnRaceStarted;
            _raceStateTracker.eventCompleted -= OnRaceCompleted;
        }

        private void OnRaceStarted()
        {
            _textTimer.enabled = true;
            enabled = true;
        }

        private void OnRaceCompleted()
        {
            _textTimer.enabled = false;
            enabled = false;
        }
    }
}


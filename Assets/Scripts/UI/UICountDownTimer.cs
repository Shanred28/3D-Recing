using TMPro;
using UnityEngine;

namespace Race
{
    public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private TMP_Text _textTimer;

        private RaceStateTracker _raceStateTracker;
        public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;

        private void Start()
        {
            _raceStateTracker.eventPreparationStarted += OnPreparationStarted;
            _raceStateTracker.eventStarted += OnRaceStarted;

            _textTimer.enabled = false;
        }

        private void Update() 
        { 
          _textTimer.text = _raceStateTracker.CountDownTimer.Value.ToString("F0");
            if (_textTimer.text == "0")
                _textTimer.text = "GO!";
        }

        private void OnPreparationStarted()
        {
            _textTimer.enabled = true;
            enabled = true;
        }
        private void OnRaceStarted()
        {
            _textTimer.enabled = false;
            enabled = false;
        }
    }
}


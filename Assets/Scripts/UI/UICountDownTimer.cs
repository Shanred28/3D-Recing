using TMPro;
using UnityEngine;

namespace Race
{
    public class UICountDownTimer : MonoBehaviour
    {
        [SerializeField] private RaceStateTracker _raceStateTracker;

        [SerializeField] private TMP_Text _textTimer;
        [SerializeField] private Timer _countDownTimer;

        private void Start()
        {
            _raceStateTracker.eventPreparationStarted += OnPreparationStarted;
            _raceStateTracker.eventStarter += OnRaceStarted;

            _textTimer.enabled = false;
        }

        private void Update() 
        { 
          _textTimer.text = _countDownTimer.Value.ToString("F0");
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


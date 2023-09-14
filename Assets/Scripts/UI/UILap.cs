using TMPro;
using UnityEngine;

namespace Race
{
    public class UILap : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<TrackPointCircuit>
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _textAmountLap;
        [SerializeField] private TMP_Text _textCurrentLap;
        private RaceStateTracker _raceStateTracker;
        public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;
        private TrackPointCircuit _trackPointCircuit;
        public void Construct(TrackPointCircuit obj) => _trackPointCircuit = obj;
        private int _lap = 1;
        private void Start () 
        {
            if (_trackPointCircuit.TrackType == TrackType.Sprint)
                _panel.SetActive(false);
            else
            {
                _panel.SetActive(true);
                _raceStateTracker.eventLapCompleted += LapCompleted;
                _textAmountLap.text = _raceStateTracker.LapsToComplete.ToString();
                _textCurrentLap.text = _lap.ToString();
            }
                
        }
        private void OnDestroy()
        {
            if (_trackPointCircuit.TrackType == TrackType.Circular)
                _raceStateTracker.eventLapCompleted -= LapCompleted;
        }

        private void LapCompleted(int lap)
        {
            _textCurrentLap.text = (lap + _lap).ToString();
        }
    }
}


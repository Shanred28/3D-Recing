using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Race
{
    public enum RaceState
    { 
        Preparation,
        CountDown,
        Race,
        Passed
    }
    public class RaceStateTracker : MonoBehaviour, IDependency<TrackPointCircuit>
    {
        public event UnityAction eventPreparationStarted;
        public event UnityAction eventStarted;
        public event UnityAction eventCompleted;
        public event UnityAction<TrackPoint> eventTrackPointPassed;
        public event UnityAction<int> eventLapCompleted;

        [SerializeField] private Timer _countDownTimer;
        public Timer CountDownTimer => _countDownTimer;
        [SerializeField] private int _lapsToComplete;
        public int LapsToComplete => _lapsToComplete;

        private RaceState _state;
        public RaceState State => _state;
        private TrackPointCircuit _trackPointCircuit;
        public void Construct(TrackPointCircuit obj) => _trackPointCircuit = obj;

        private void Start()
        {
            StartState(RaceState.Preparation);

            _countDownTimer.enabled = false;
            _countDownTimer.eventFinishedTimer += OnCountDownTimerFinished;

            _trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;
            _trackPointCircuit.LapCompleted += OnLapCompleted;
        }
      
        private void OnDestroy()
        {
            _countDownTimer.eventFinishedTimer -= OnCountDownTimerFinished;
            _trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
            _trackPointCircuit.LapCompleted -= OnLapCompleted;
        }

        private void StartState(RaceState state)
        { 
            _state = state;
        }

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            eventTrackPointPassed?.Invoke(trackPoint);
        }

        private void OnCountDownTimerFinished()
        {
            StartRace();
        }

        private void OnLapCompleted(int lapAmount)
        {
            if (_trackPointCircuit.TrackType == TrackType.Sprint)
            {
                CompleteRace();
                Save();
            }

            if (_trackPointCircuit.TrackType == TrackType.Circular)
            {
                if (lapAmount == _lapsToComplete)
                {
                    CompleteRace();
                    Save();
                }                  
                else
                    CompleteLap(lapAmount);
            }                
        }

        public void LaunchPreparationStart()
        {
            if (_state != RaceState.Preparation) return;

            StartState(RaceState.CountDown);

            _countDownTimer.enabled = true;
            eventPreparationStarted?.Invoke();
        }

        private void StartRace()
        {
            if (_state != RaceState.CountDown) return;

            StartState(RaceState.Race);

            eventStarted?.Invoke();
        }

        private void CompleteRace()
        {
            if (_state != RaceState.Race) return;

            StartState(RaceState.Passed);

            eventCompleted?.Invoke();
        }

        private void CompleteLap(int lapAmount)
        {
            eventLapCompleted?.Invoke(lapAmount);
        }

        private void Save()
        {
            var a = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetInt(a, 1);
            print(a);
        }
    }
}


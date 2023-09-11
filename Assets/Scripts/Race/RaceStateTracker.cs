using System;
using UnityEngine;
using UnityEngine.Events;

namespace Race
{
    public enum RaceState
    { 
        Preparation,
        CountDown,
        Race,
        Passed
    }
    public class RaceStateTracker : MonoBehaviour
    {
        public event UnityAction eventPreparationStarted;
        public event UnityAction eventStarter;
        public event UnityAction eventCompleted;
        public event UnityAction<TrackPoint> eventTrackPointPassed;
        public event UnityAction<int> eventLapCompleted;

        [SerializeField] private TrackPointCircuit _trackPointCircuit;
        [SerializeField] private Timer _countDownTimer;
        [SerializeField] private int _lapsToComplete;

        private RaceState _state;
        public RaceState State => _state;

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
            }

            if (_trackPointCircuit.TrackType == TrackType.Circular)
            {
                if (lapAmount == _lapsToComplete)
                    CompleteRace();
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

            eventStarter?.Invoke();
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
    }
}


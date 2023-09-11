using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Race
{
    public class RaceResultTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimerTracker>
    {
        public static string SaveMark = "_player_best_time";

        public event UnityAction UpdateRecords;

        [SerializeField] private float _goldTime;
        public float GoldTime => _goldTime;
        private float _playerRecordTime;
        public float PlayerRecordTime => _playerRecordTime;
        private float _currentTime;
        public float CurrentTime => _currentTime;
        public bool IsRecordWasSet => _playerRecordTime != 0;

        private RaceStateTracker _raceStateTracker;
        public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;

        private RaceTimerTracker _raceTimerTracker;
        public void Construct(RaceTimerTracker obj) => _raceTimerTracker = obj;

        private void Awake()
        {
            Load();
        }

        private void Start()
        {
            _raceStateTracker.eventCompleted += OnRaceCompleted;
        }

        private void OnDestroy()
        {
            _raceStateTracker.eventCompleted -= OnRaceCompleted;
        }

        private void OnRaceCompleted()
        {
            float absolutRecord = GetAbsolureeRecord();

            if (_raceTimerTracker.CurrentTime < absolutRecord || _playerRecordTime == 0)
            {
                _playerRecordTime = _raceTimerTracker.CurrentTime;
                Save();
            }
                
            _currentTime = _raceTimerTracker.CurrentTime;
            UpdateRecords?.Invoke();
        }

        public float GetAbsolureeRecord()
        {
            if (_playerRecordTime < _goldTime && _playerRecordTime != 0)
                return _playerRecordTime;
            else
                return _goldTime;
        }

        private void Load()
        {
            _playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, _playerRecordTime);
        }
    }
}


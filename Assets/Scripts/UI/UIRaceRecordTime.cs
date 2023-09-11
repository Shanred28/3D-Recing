using Race;
using System;
using TMPro;
using UnityEngine;

public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>
{
    [SerializeField] private GameObject _goldRecordObj;
    [SerializeField] private GameObject _playerRecordObj;
    [SerializeField] private TMP_Text _goldRecordTimeText;
    [SerializeField] private TMP_Text _playerRecordTimeText;

    [SerializeField] private TMP_Text _recordLable;


    private RaceStateTracker _raceStateTracker;
    public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;

    private RaceResultTime _raceResultTime;
    public void Construct(RaceResultTime obj) => _raceResultTime = obj;

    private void Start()
    {
        _raceStateTracker.eventStarted += OnRaceStart;
        _raceStateTracker.eventCompleted += OnRaceCompleted;

        _goldRecordObj.SetActive(false);
        _playerRecordObj.SetActive(false);
    }

    private void OnDestroy()
    {
        _raceStateTracker.eventStarted -= OnRaceStart;
        _raceStateTracker.eventCompleted -= OnRaceCompleted;
    }

    private void OnRaceStart()
    {
        if (_raceResultTime.PlayerRecordTime > _raceResultTime.GoldTime || _raceResultTime.IsRecordWasSet == false)
        {
            _goldRecordObj.SetActive(true);
            _goldRecordTimeText.text = StringTime.SecondToTimeString(_raceResultTime.GoldTime);
        }

        if (_raceResultTime.IsRecordWasSet == true)
        {
            _playerRecordObj.SetActive(true);
            _playerRecordTimeText.text = StringTime.SecondToTimeString(_raceResultTime.PlayerRecordTime);
        }
    }

    private void OnRaceCompleted()
    {
        _goldRecordObj.SetActive(false);
        _playerRecordObj.SetActive(false);
    }
}
